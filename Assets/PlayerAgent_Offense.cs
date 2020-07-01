using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

/// <summary>
///     Der Player soll erstmal nur den Ball los kicken Richtung Tor
/// </summary>
public class PlayerAgent_Offense : Agent
{
    public GameObject ball;
    private Collides ballCollides;
    private Vector3 ballDefaultPosition;
    private float ballRestBegin;

    [Tooltip("Velocity threshold below which the ball is defined at rest")]
    public float ballRestThreshold = 1f;

    [Tooltip("Reset the ball after period of time without moving")]
    public float ballRestTimeout = 3f;

    private SphereCollider cl_ball;

    [Header("Debug Output")] public TMP_Text cumulativeReward;

    public Transform keepGoal;
    public float kickInPower = 10;
    private float lastVelocity;
    public Rigidbody pole;
    private Vector3 pole_position;
    private Quaternion pole_rotation;
    private Rigidbody rb_ball;
    public float shootPositionRange = 4;

    public Transform target;
    private Transform tr_ball;
    private Transform tr_pole;

    private void Start()
    {
        rb_ball = ball.GetComponent<Rigidbody>();
        tr_ball = ball.GetComponent<Transform>();
        tr_pole = pole.GetComponent<Transform>();
        ballCollides = ball.GetComponent<Collides>();
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
        var kickForceAmplification = 200;
        // Actions, size = 1 //weil nur Drehung der einen Achse, würde ich sagen.
        var controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0] * kickForceAmplification;
        pole.AddTorque(controlSignal);

        // sudden deaths
        if (BallOut())
        {
            Debug.Log("Out!");
            EndEpisode();
        }

        if (BallRest())
        {
            Debug.Log("REST!");
            EndEpisode();
        }

        // Rewards
        if (ballCollides.touchedHomeZone)
        {
            // Debug.Log("Strafraum");
            SetReward(-0.3f);
            ballCollides.touchedHomeZone = false;
        }

        if (SlowDownTheBall()) SetReward(0.3f);

        // Reached target
        if (ballCollides.touchedTarget)
        {
            // Debug.Log("Toooor");
            SetReward(1.0f);
            ballCollides.ResetValues();
            EndEpisode();
        }

        // Tor kassiert
        if (ballCollides.touchedSelfGoal)
        {
            // Debug.Log("EIGENTOR");
            SetReward(-1.0f);
            ballCollides.ResetValues();
            EndEpisode();
        }

        cumulativeReward.text = vectorAction[0].ToString("R") + " // " + GetCumulativeReward().ToString("R");
    }

    //wenn jetzt die Beschleunigung viel kleiner ist als beim letzten Mal gibts reward
    private bool SlowDownTheBall()
    {
        if (!ballCollides.touchedPlayer) return false;
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
    ///     return true if the ball has not moved over a period of time
    /// </summary>
    /// <returns></returns>
    private bool BallRest()
    {
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
    }
}