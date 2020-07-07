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
    public Transform target;
    public Transform keepGoal;
    public Transform player1Foot;
    public Transform player2Foot;
    public Transform player3Foot;
    public CollisonDetector scoopNet;

    [Header("Training settings")]
    public GameObject ball;

    [Tooltip("Velocity threshold below which the ball is defined at rest")]
    public float ballRestThreshold = 1f;

    [Tooltip("Reset the ball after period of time without moving")]
    public float ballRestTimeout = 3f;
    
    public float kickInPower = 10;
    public Rigidbody pole;
    public float shootPositionRange = 4;
    
    [Header("Debug Output")] 
    public TMP_Text cumulativeReward;
    public TMP_Text lineTwo;
    public Statistics statistics;
    
    private void Start()
    {
        _rbBall = ball.GetComponent<Rigidbody>();
        _trBall = ball.GetComponent<Transform>();
        _trPole = pole.GetComponent<Transform>();
        _ballColliders = ball.GetComponent<Colliders>();
        _poleDefaultPosition = _trPole.position;
        _poleDefaultRotation = _trPole.rotation;
        shootPositionRange = shootPositionRange / 2;
        _ballDefaultPosition = _trBall.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        statistics.AddEpisode();
        _trPole.position = _poleDefaultPosition;
        _trPole.rotation = _poleDefaultRotation;
        SpawnTheBall();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Grundlage für Einstellung in Vector Observation > Space Size
        // 1x pos Ball                   = 3
        sensor.AddObservation(_trBall.localPosition);        
        // 1x velocity Ball                   = 3
        sensor.AddObservation(_rbBall.velocity);
        // 3x pos of the kicker foot     = 3x3
        sensor.AddObservation(player1Foot.localPosition);
        sensor.AddObservation(player2Foot.localPosition);
        sensor.AddObservation(player3Foot.localPosition);
        // 3x rot of the kicker foot     = 3x4
        sensor.AddObservation(player1Foot.localRotation);
        sensor.AddObservation(player2Foot.localRotation);
        sensor.AddObservation(player3Foot.localRotation);
        // 1x rot angle of the pole      = 1
        sensor.AddObservation(pole.rotation.eulerAngles.x);
        //                     sum:     = 28
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        ApplyActions(vectorAction);
        YieldRewards();
        
        //Debug Output
        cumulativeReward.text = GetCumulativeReward().ToString("R");
        lineTwo.text = vectorAction[1].ToString("F");
    }


    private void ApplyActions(float[] vectorAction)
    {
        // Actions, size = 2
        // [0] axis rotation
        // [1] pole bar dragging 
        var kickForceAmplification = 40;
        var poleDragAmplification = 30;
        var controlSignal = Vector3.zero;
        
        controlSignal.x = vectorAction[0] * kickForceAmplification;

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
            EndEpisode();
        }
        
        if (BallRest())
        {
            // Debug.Log("Ball rests");
            AddReward(-0.2f);
            statistics.AddRest();
            EndEpisode();
        }

        // Rewards

        //for precise kicks towards the goal
        AddReward(AimingAccuracy());
        AddReward(TargetApproximation());
        
        
        if (_ballColliders.touchedHomeZone)
        {
            // Debug.Log("penalty area");
            AddReward(-0.3f);
            _ballColliders.touchedHomeZone = false;
        }

        // if (SlowDownTheBall()) 
        //     AddReward(0.3f);

        // Toooor
        if (_ballColliders.touchedTarget)
        {
            AddReward(1.0f);
            _ballColliders.ResetValues();
            EndEpisode();
        }

        // doh, own goal
        if (_ballColliders.touchedSelfGoal)
        {
            AddReward(-1.0f);
            _ballColliders.ResetValues();
            EndEpisode();
        }
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
    /// reward for moving the ball closer to the goal
    /// if just check _lastGoalDistance > goalDistance, model optimizes for most slowly hit the goal 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private float TargetApproximation()
    {
        float goalDistance = Vector3.Distance(_trBall.position, target.position);
        if (_lastGoalDistance - goalDistance <= 0.05f)
        {
            _lastGoalDistance = goalDistance;
            return 0;
        }

        _lastGoalDistance = goalDistance;
        return 0.01f;
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

    private void KickInTheBall()
    {
        _rbBall.angularVelocity = Vector3.zero;
        _rbBall.velocity = Vector3.zero;

        var xPos = Random.Range(-shootPositionRange, shootPositionRange);
        _trBall.localPosition = new Vector3(xPos, 0.4f, -7.45f);

        var controlSignal = keepGoal.position - _trBall.position;
        controlSignal.Normalize();
        _rbBall.AddForce(controlSignal * kickInPower);
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
    
    /// <summary>
    /// defense tactic of slowing down the ball
    /// </summary>
    /// <returns></returns>
    private bool SlowDownTheBall()
    {
        if (!_ballColliders.touchedPlayer) return false;
        var thisVelocity = Mathf.Abs(_rbBall.velocity.z);
        if (thisVelocity < _lastVelocity)
        {
            // Debug.Log("SlowDownBall");
            _lastVelocity = thisVelocity;
            return true;
        }

        _lastVelocity = thisVelocity;
        return false;
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