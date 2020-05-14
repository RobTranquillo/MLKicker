using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PlayerAgent : Agent
{
    public Rigidbody pole;
    private Transform tr_pole;
    private Vector3 pole_position;
    private Quaternion pole_rotation;

    public GameObject ball;
    private Rigidbody rb_ball;
    private Transform tr_ball;

    public Transform target;
    public Transform keepGoal;
    public float kickPower = 10;
    [Tooltip("Movement threshold below which the ball is at rest")]
    public float ballRestThreshold = 1f;
    [Tooltip("Reset the ball after period of time without moving")]
    public float ballRestTimeout = 3f;
    private float ballRestBegin = 0;
    public float shootPositionRange = 4;

    private void Start()
    {
        rb_ball = ball.GetComponent<Rigidbody>();
        tr_ball = ball.GetComponent<Transform>();
        tr_pole = pole.GetComponent<Transform>();
        pole_position = tr_pole.position;
        pole_rotation = tr_pole.rotation;
        KickInTheBall();
    }

    public override void OnEpisodeBegin()
    {
        tr_pole.position = pole_position;
        tr_pole.rotation = pole_rotation;
        KickInTheBall();
    }

    private void KickInTheBall()
    {
        rb_ball.angularVelocity = Vector3.zero;
        rb_ball.velocity = Vector3.zero;

        float xPos = Random.value * shootPositionRange;
        tr_ball.localPosition = new Vector3(xPos, 0.4f, -7.45f);

        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = -xPos * (kickPower / 4);
        controlSignal.z = kickPower;
        // Debug.Log("controlSignal " + controlSignal);
        rb_ball.AddForce(controlSignal);
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
        // Actions, size = 1 //weil nur Drehung der einen Achse, würde ich sagen.
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        pole.AddTorque(controlSignal);

        // Rewards

        if (BallOut())
        {
            Debug.Log("Out!");
            EndEpisode();
        }

        if (BallRest())
        {
            EndEpisode();
        }

        // Reached target
        float distanceToTarget = Vector3.Distance(tr_ball.localPosition, target.localPosition);
        if (distanceToTarget < 1.42f)
        {
            Debug.Log("Toooor");
            SetReward(1.0f);
            EndEpisode();
        }

        // Tor kassiert
        float distanceToKeepGoal = Vector3.Distance(tr_ball.localPosition, keepGoal.localPosition);
        if (distanceToKeepGoal < 1.42f)
        {
            Debug.Log("EIGENTOR");
            SetReward(-1.0f);
            EndEpisode();
        }
    }

    private bool BallOut()
    {
        if (tr_ball.position.y > 2 || tr_ball.position.y < 0)
        {
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
        //wenn keine Bewegung auftritt
        if (Mathf.Abs(rb_ball.velocity.x) > ballRestThreshold && Mathf.Abs(rb_ball.velocity.z) > ballRestThreshold )
        {
            ballRestBegin = Time.time;
            return false;
        }

        //wenn Ball längere Zeit geruht hat
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
