using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

/// <summary>
///  Class for an offensive playing mind 
/// </summary>
public class PlayerAgent_Offense : Agent
{
    private Colliders _ballColliders;
    private Vector3 _ballDefaultPosition;
    private float _ballRestBegin;
    private float _lastVelocity;
    private Vector3 _poleDefaultPosition;
    private Quaternion _poleDefaultRotation;
    private Rigidbody _rbBall;
    private Transform _trBall;
    private Transform _trPole;

    private float maxAberration = 10f;
    private float _accuracyReward = 0;
    private float _aberration = 555;
    private float _lastGoalDistance = 0;

    [Header("Physicals")] 
    public Transform kicker;
    public Transform target;
    public Transform keepGoal;
    public Transform player1Foot;
    public Transform player2Foot;
    public Transform player3Foot;
    public CollisonDetector scoopNet;

    [Header("Training settings")]
    public GameObject ball;

    [Tooltip("Velocity threshold below which the ball is defined at rest")]
    [Range(0f , 1f)]
    public float ballRestThreshold = 1f;

    [Tooltip("Reset the ball after period of time without moving")]
    public float ballRestTimeout = 3f;
    
    public float kickInPower = 10;
    public float poleDragAmplification = 30;
    public Rigidbody pole;
    
    [Tooltip("The threshold of the ball speed above where reward is given. When exceeded and approaching the goal.")]
    [Range(1,200)]
    public float rewardedBallSpeed = 5;
    
    [Header("Debug Output")] 
    public TMP_Text cumulativeReward;
    public TMP_Text lineTwo;
    public Statistics statistics;
    
    
    // rewards and penalties
    private const float Goal = 1.0f;
    private const float SelfGoal = -1f;
    private const float ForwardPlay = 0.01f;
    private const float BallContactPenalty = -0.001f;
    private const float HeavyRotationPenalty = -0.01f;
    private const float BallRestPenalty = -0.2f;
    private const float LameDefensePenalty = -0.3f;
    
    
    private void Start()
    {
        _rbBall = ball.GetComponent<Rigidbody>();
        _trBall = ball.GetComponent<Transform>();
        _trPole = pole.GetComponent<Transform>();
        _ballColliders = ball.GetComponent<Colliders>();
        _poleDefaultPosition = _trPole.position;
        _poleDefaultRotation = _trPole.rotation;
        _ballDefaultPosition = _trBall.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        statistics.AddEpisode();
        ResetPole();
        SpawnTheBall();
    }
    
    public override void OnActionReceived(float[] vectorAction)
    {
        ApplyActions(vectorAction);
        YieldRewards();
        
        //Debug Output
        cumulativeReward.text = GetCumulativeReward().ToString("R");
        lineTwo.text = (_rbBall.velocity / MaxVelocity).ToString("F");
        // (_trBall.localPosition.x / GameFieldWidth).ToString("F") 
        //            + " / " ;
    }

    //dimensions for normalisation
    private const float GameFieldSize = 18f;
    private const float GameFieldWidth = 5f;
    private const float GameFieldHeight = 5f;
    private const float MaxVelocity = 100f; //how many units moved per second (// )can be high on strong kicks) 
    
    public override void CollectObservations(VectorSensor sensor)
    {
        // normalizedValue = (currentValue - minValue)/(maxValue - minValue)
        CheckNormalization(_trBall.localPosition.z / GameFieldSize);
        CheckNormalization(_trBall.localPosition.x / GameFieldWidth);
        CheckNormalization(_rbBall.velocity / MaxVelocity);
        CheckNormalization(player1Foot.localPosition.x / GameFieldSize);
        CheckNormalization(player1Foot.localPosition.y / GameFieldHeight);
        CheckNormalization(player1Foot.localPosition.z / GameFieldWidth);        
          
        //Grundlage für Einstellung in Vector Observation > Space Size
        // 1x pos Ball                   = 3
        sensor.AddObservation(_trBall.localPosition.x / GameFieldWidth);
        sensor.AddObservation(_trBall.localPosition.y);
        sensor.AddObservation(_trBall.localPosition.z / GameFieldSize);
        // 1x velocity Ball              = 3
        sensor.AddObservation(_rbBall.velocity / MaxVelocity);
        
        // 3x pos of the kicker foot     = 3x3
        sensor.AddObservation(player1Foot.localPosition.x / GameFieldSize);
        sensor.AddObservation(player1Foot.localPosition.y / GameFieldHeight);
        sensor.AddObservation(player1Foot.localPosition.z / GameFieldWidth);
        sensor.AddObservation(player2Foot.localPosition.x / GameFieldSize);
        sensor.AddObservation(player2Foot.localPosition.y / GameFieldHeight);
        sensor.AddObservation(player2Foot.localPosition.z / GameFieldWidth);
        sensor.AddObservation(player3Foot.localPosition.x / GameFieldSize);
        sensor.AddObservation(player3Foot.localPosition.y / GameFieldHeight);
        sensor.AddObservation(player3Foot.localPosition.z / GameFieldWidth);

        // 3x rot of the kicker foot     = 3x4
        sensor.AddObservation(player1Foot.localRotation);
        sensor.AddObservation(player2Foot.localRotation);
        sensor.AddObservation(player3Foot.localRotation);
        
        // 1x rot angle of the pole      = 1
        sensor.AddObservation(pole.rotation.eulerAngles.x / 360.0f);
        //                     sum:     = 28
    }

    private void CheckNormalization(Vector3 val)
    {
        CheckNormalization(val.x);
        CheckNormalization(val.y);
        CheckNormalization(val.z);
    }
    private void CheckNormalization(float val)
    {
        if (val > 1f || val < -1f)
        {
            Debug.Log("Value ist nicht nomalisiert!");
        }
    }

    private void ApplyActions(float[] vectorAction)
    {
        // Actions, size = 2
        // [0] axis rotation
        // [1] pole bar dragging 
        var controlSignal = Vector3.zero;
        
        controlSignal.x = vectorAction[0] * kickInPower;

        //just add physical forces 
        pole.AddTorque(controlSignal);
        pole.AddForce(vectorAction[1] * poleDragAmplification, 0, 0);
    }

    private void YieldRewards()
    {
        // sudden deaths
        if (BallOut() || PoleOut())
        {
            statistics.AddOut();
            ResetPole();
            // EndEpisode();
        }

        if (BallRest())
        {
            // Debug.Log("Ball rests");
            AddReward(BallRestPenalty);
            statistics.AddRest();
            SpawnTheBall();
            // EndEpisode();
        }

        // Rewards
        AddReward(BallContact());
        
        //for precise kicks towards the goal
        AddReward(AimingAccuracy());
        AddReward(TargetApproximation());
        
        //try to penalty heavy rotation
        AddReward(HeavyRotation());
        
        
        if (_ballColliders.touchedHomeZone)
        {
            // Debug.Log("penalty area");
            AddReward(LameDefensePenalty);
            _ballColliders.touchedHomeZone = false;
        }

        // if (SlowDownTheBall()) 
        //     AddReward(0.3f);

        // Toooor
        if (_ballColliders.touchedTarget)
        {
            AddReward(Goal);
            _ballColliders.ResetValues();
            EndEpisode();
        }

        // doh, own goal
        if (_ballColliders.touchedSelfGoal)
        {
            AddReward(SelfGoal);
            _ballColliders.ResetValues();
            EndEpisode();
        }
    }

    private void ResetPole()
    {
        _trPole.position = _poleDefaultPosition;
        _trPole.rotation = _poleDefaultRotation;        
    }
    

    private float HeavyRotation()
    {
        if (_trPole.rotation.eulerAngles.x > 179f && _trPole.rotation.eulerAngles.x < 181f)
            return HeavyRotationPenalty;
        return 0;
    }

    private float BallContact()
    {
        if (Mathf.Abs(_trBall.position.z - _trPole.position.z) > 1f)
            return 0;
        if (_ballColliders.touchedPlayer)
        {
            _ballColliders.touchedPlayer = false;
            return 1f;
        }
        //if ball stays untouched
        return BallContactPenalty;
    }

    /// <summary>
    /// If the pole bar is cracked out
    /// </summary>
    /// <returns></returns>
    private bool PoleOut()
    {
        if (scoopNet.isTouched)
        {
            // Debug.Log("Scoop Out!");
            scoopNet.Reset();
            return true;
        }

        if (Mathf.Abs(_trPole.position.y - _poleDefaultPosition.y) > 3f ||
            Mathf.Abs(_trPole.position.z - _poleDefaultPosition.z) > 3f ||
            _trPole.localPosition.y < 0f)
        {
            // Debug.Log("Positon Out!");
            return true;
        }
        return false;
    }

    /// <summary>
    /// reward for moving the ball closer to the goal fast
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private float TargetApproximation()
    {
        float goalDistance = Vector3.Distance(_trBall.position, target.position);
        if (_lastGoalDistance - goalDistance <= 1 / rewardedBallSpeed)
        {
            _lastGoalDistance = goalDistance;
            return 0;
        }

        _lastGoalDistance = goalDistance;
        return ForwardPlay;
    

    }

    /// <summary>
    ///     place the ball randomly
    /// </summary>
    private void SpawnTheBall()
    {
        var pos = _ballDefaultPosition;
        pos.x = Random.Range(-4.4f, 4.4f);
        _trBall.localPosition = pos;
        _rbBall.velocity = Vector3.zero;
        _rbBall.angularVelocity = Vector3.zero;
        _trBall.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// reward for kicking the ball towards the goal
    /// </summary>
    /// <returns></returns>
    private float AimingAccuracy()
    {
        //just in the moment of touching
        if (_ballColliders.touchedPlayer || _ballColliders.touchedPole)
        {
            var goalDirection = target.position - _trBall.position;
            _aberration = Vector3.Angle(goalDirection, _rbBall.velocity);
            _accuracyReward = 1 - (_aberration / maxAberration);
            return _accuracyReward > 0 ? _accuracyReward : 0;
        }
        return 0;
    }

    private bool BallOut()
    {
        if (Mathf.Abs(_trBall.localPosition.y - _ballDefaultPosition.y) > 3f)
        {
            // Debug.Log("Ball Out!");
            return true;
        }
        return false;
    }

    /// <summary>
    /// return true if the ball has not moved over a period of time
    /// </summary>
    /// <returns></returns>
    private bool BallRest()
    {
        // false, as long as playful actions are still possible
        if (Mathf.Abs(_trPole.localPosition.z - _trBall.localPosition.z) < 2f)
            return false;
        
        //ball is moving constantly
        if (Mathf.Abs(_rbBall.velocity.x) > ballRestThreshold ||
            Mathf.Abs(_rbBall.velocity.y) > ballRestThreshold ||
            Mathf.Abs(_rbBall.velocity.z) > ballRestThreshold)
        {
            _ballRestBegin = Time.time;
            return false;
        }

        //if the ball rests a period of time
        if (Time.time > _ballRestBegin + ballRestTimeout)
        {
            _ballRestBegin = Time.time;
            return true;
        }
        return false;
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Vertical");
        actionsOut[1] = Input.GetAxis("Horizontal");
    }
}