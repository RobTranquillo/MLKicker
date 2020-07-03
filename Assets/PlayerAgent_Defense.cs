using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System;
using UnityEngine.UI;

public class PlayerAgent_Defense: Agent
{
    public Rigidbody pole;
    private Transform tr_pole;
    private Vector3 pole_position;
    private Quaternion pole_rotation;

    public GameObject ball;
    private Rigidbody rb_ball;
    private Transform tr_ball;
    private SphereCollider cl_ball;

    public Transform target;
    public Transform keepGoal;
    public float kickPower = 10;
    [Tooltip("Movement threshold below which the ball is at rest")]
    public float ballRestThreshold = 1f;
    [Tooltip("Reset the ball after period of time without moving")]
    public float ballRestTimeout = 3f;
    private float ballRestBegin = 0;
    public float shootPositionRange = 4;
    private float lastVelocity = 0f;
    private Colliders _ballColliders;
    
    [Header("Debug Output")]
    public TMPro.TMP_Text cumulativeReward;    

    private void Start()
    {
        rb_ball = ball.GetComponent<Rigidbody>();
        tr_ball = ball.GetComponent<Transform>();
        tr_pole = pole.GetComponent<Transform>();
        _ballColliders = ball.GetComponent<Colliders>();
        pole_position = tr_pole.position;
        pole_rotation = tr_pole.rotation;
        shootPositionRange = shootPositionRange/2;
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

        float xPos = UnityEngine.Random.Range(-shootPositionRange, shootPositionRange);
        tr_ball.localPosition = new Vector3(xPos, 0.4f, -7.45f);

        Vector3 controlSignal = keepGoal.position - tr_ball.position;
        controlSignal.Normalize();
        rb_ball.AddForce(controlSignal * kickPower);
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
        controlSignal.x = vectorAction[0] * 200;
        pole.AddTorque(controlSignal);

        // sudden deaths
        if (BallOut())
        {
            Debug.Log("Out!");
            EndEpisode();
        }

        if (BallRest())
        {
            EndEpisode();
        }

        // Rewards
        if (_ballColliders.touchedHomeZone)
        {
            // Debug.Log("Strafraum");
            SetReward(-0.3f);
            _ballColliders.touchedHomeZone = false;
        }

        if (SlowDownTheBall())
        {
            SetReward(0.3f);
        }

        // Reached target
        if (_ballColliders.touchedTarget)
        {
            // Debug.Log("Toooor");
            SetReward(1.0f);
            _ballColliders.ResetValues();
            EndEpisode();
        }

        // Tor kassiert
        if (_ballColliders.touchedSelfGoal)
        {
            // Debug.Log("EIGENTOR");
            SetReward(-1.0f);
            _ballColliders.ResetValues();
            EndEpisode();
        }

        cumulativeReward.text = GetCumulativeReward().ToString("R");
    }

    //wenn jetzt die Beschleunigung viel kleiner ist als beim letzten Mal gibts reward
    private bool SlowDownTheBall()
    {
        if( ! _ballColliders.touchedPlayer )
        {
            return false;
        }
        float thisVelocity = Mathf.Abs(rb_ball.velocity.z);
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
