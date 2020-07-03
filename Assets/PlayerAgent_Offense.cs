using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

/// <summary>
///     Der Player soll erstmal nur den Ball los kicken Richtung Tor
/// </summary>
public class PlayerAgent_Offense : Agent
{
    private Colliders _ballColliders;
    private Vector3 ballDefaultPosition;
    private float ballRestBegin;
    private SphereCollider cl_ball;
    private float lastVelocity;
    private Vector3 pole_position;
    private Quaternion pole_rotation;
    private Rigidbody rb_ball;
    private Transform tr_ball;
    private Transform tr_pole;
    
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
        rb_ball = ball.GetComponent<Rigidbody>();
        tr_ball = ball.GetComponent<Transform>();
        tr_pole = pole.GetComponent<Transform>();
        _ballColliders = ball.GetComponent<Colliders>();
        pole_position = tr_pole.position;
        pole_rotation = tr_pole.rotation;
        shootPositionRange = shootPositionRange / 2;
        ballDefaultPosition = tr_ball.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        tr_pole.position = pole_position;
        tr_pole.rotation = pole_rotation;
        // KickInTheBall();
        SpawnTheBall();
    }

    /// <summary>
    ///     place the ball randomly
    /// </summary>
    private void SpawnTheBall()
    {
        var pos = ballDefaultPosition;
        pos.x = Random.Range(-4.4f, 4.4f);
        tr_ball.localPosition = pos;
        rb_ball.velocity = Vector3.zero;
        rb_ball.angularVelocity = Vector3.zero;
        tr_ball.localRotation = Quaternion.identity;
    }

    private void KickInTheBall()
    {
        rb_ball.angularVelocity = Vector3.zero;
        rb_ball.velocity = Vector3.zero;

        var xPos = Random.Range(-shootPositionRange, shootPositionRange);
        tr_ball.localPosition = new Vector3(xPos, 0.4f, -7.45f);

        var controlSignal = keepGoal.position - tr_ball.position;
        controlSignal.Normalize();
        rb_ball.AddForce(controlSignal * kickInPower);
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        //Grundlage für Einstellung in Vector Observation > Space Size
        //3x Vector3 + 1x float = 10

        // Target and Agent positions
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(keepGoal.localPosition);
        sensor.AddObservation(tr_ball.localPosition);

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

    private float maxAberration = 10f;
    private float accuracyReward = 0;
    private float aberration = 555;
    private float AimingAccuracy()
    {
        //just in the moment of touching
        if (_ballColliders.touchedPlayer || _ballColliders.touchedPole)
        {
            var goalDirection = target.position - tr_ball.position;
            aberration = Vector3.Angle(goalDirection, rb_ball.velocity);
            accuracyReward = 1 - (aberration / maxAberration);
            return accuracyReward > 0 ? accuracyReward : 0;
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
        var thisVelocity = Mathf.Abs(rb_ball.velocity.z);
        if (thisVelocity < lastVelocity)
        {
            // Debug.Log("SlowDownBall");
            lastVelocity = thisVelocity;
            return true;
        }

        lastVelocity = thisVelocity;
        return false;
    }

    private bool BallOut()
    {
        if (tr_ball.position.y > 2 || tr_ball.position.y < 0) return true;
        return false;
    }

    /// <summary>
    /// return true if the ball has not moved over a period of time
    /// </summary>
    /// <returns></returns>
    private bool BallRest()
    {
        // false, as long as playful actions are still possible
        if (Mathf.Abs(tr_ball.localPosition.z) < 2f)
            return false;
        
        //ball is moving constantly
        if (Mathf.Abs(rb_ball.velocity.x) > ballRestThreshold ||
            Mathf.Abs(rb_ball.velocity.y) > ballRestThreshold ||
            Mathf.Abs(rb_ball.velocity.z) > ballRestThreshold)
        {
            ballRestBegin = Time.time;
            return false;
        }

        //if the ball rests a period of time
        if (Time.time > ballRestBegin + ballRestTimeout)
        {
            ballRestBegin = Time.time;
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