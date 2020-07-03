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
    private Vector3 _polePosition;
    private Quaternion _poleRotation;
    private Rigidbody _rbBall;
    private Transform _trBall;
    private Transform _trPole;
    
    private float maxAberration = 10f;
    private float _accuracyReward = 0;
    private float _aberration = 555;
    private float _lastGoalDistance = 0;

    [Header("Training settings")]
    public GameObject ball;

    [Tooltip("Velocity threshold below which the ball is defined at rest")]
    public float ballRestThreshold = 1f;

    [Tooltip("Reset the ball after period of time without moving")]
    public float ballRestTimeout = 3f;
    
    public Transform target;
    public Transform keepGoal;
    public float kickInPower = 10;
    public Rigidbody pole;
    public float shootPositionRange = 4;


    [Header("Debug Output")] 
    public TMP_Text cumulativeReward;
    public TMP_Text lineTwo;    
    
    private void Start()
    {
        _rbBall = ball.GetComponent<Rigidbody>();
        _trBall = ball.GetComponent<Transform>();
        _trPole = pole.GetComponent<Transform>();
        _ballColliders = ball.GetComponent<Colliders>();
        _polePosition = _trPole.position;
        _poleRotation = _trPole.rotation;
        shootPositionRange = shootPositionRange / 2;
        _ballDefaultPosition = _trBall.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        _trPole.position = _polePosition;
        _trPole.rotation = _poleRotation;
        SpawnTheBall();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Grundlage für Einstellung in Vector Observation > Space Size
        //3x Vector3 + 1x float = 10

        // Target and Agent positions
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(keepGoal.localPosition);
        sensor.AddObservation(_trBall.localPosition);

        // Agent velocity
        sensor.AddObservation(pole.rotation.eulerAngles.x);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        var kickForceAmplification = 20;
        var poleDragAmplification = 10;
        var controlSignal = Vector3.zero;
        
        // Actions, size = 2 //weil Drehung um die Achse und schieben der Stange
        controlSignal.x = vectorAction[0] * kickForceAmplification;
        pole.AddTorque(controlSignal);
        pole.AddForce(vectorAction[1] * poleDragAmplification, 0, 0);

        // sudden deaths
        if (BallOut())
        {
            Debug.Log("Out!");
            EndEpisode();
        }
        
        //for precise kicks towards the goal
        AddReward(AimingAccuracy());
        
        //AddReward(TargetApproximation());

        if (BallRest())
        {
            // Debug.Log("REST!");
            SetReward(-0.3f);
            EndEpisode();
        }

        // Rewards
        if (_ballColliders.touchedHomeZone)
        {
            // Debug.Log("Strafraum");
            AddReward(-0.3f);
            _ballColliders.touchedHomeZone = false;
        }

        // if (SlowDownTheBall()) 
        //     AddReward(0.3f);

        // Reached target
        if (_ballColliders.touchedTarget)
        {
            // Debug.Log("Toooor");
            AddReward(1.0f);
            _ballColliders.ResetValues();
            EndEpisode();
        }

        // Tor kassiert
        if (_ballColliders.touchedSelfGoal)
        {
            // Debug.Log("EIGENTOR");
            AddReward(-1.0f);
            _ballColliders.ResetValues();
            EndEpisode();
        }

        cumulativeReward.text = GetCumulativeReward().ToString("R");
        // lineTwo.text = accuracyReward.ToString("F") +" | "+ aberration.ToString("F");
    }

    /// <summary>
    /// reward for moving the ball closer to the goal   
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private float TargetApproximation()
    {
        float goalDistance = Vector3.Distance(_trBall.position, target.position);
        if (_lastGoalDistance >= goalDistance)
            return 0;
        return 0.1f;
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
        if (_trBall.position.y > 2 || _trBall.position.y < 0) return true;
        return false;
    }

    /// <summary>
    /// return true if the ball has not moved over a period of time
    /// </summary>
    /// <returns></returns>
    private bool BallRest()
    {
        // false, as long as playful actions are still possible
        if (Mathf.Abs(_trBall.localPosition.z) < 2f)
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