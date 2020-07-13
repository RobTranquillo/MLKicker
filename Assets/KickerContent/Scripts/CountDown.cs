using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public float timeLeft = 15;
    public TorTrigger TorRed;
    public TorTrigger TorBlue;
    public GameObject ball;
    public ParticleSystem end;
    Vector3 startPosBall;

    public Text time;

    void Start()
    {
        startPosBall = ball.transform.position;
    }

    void Update()
    {

        timeLeft -= Time.deltaTime;
        time.text = "Time Left:" + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            if (end)
                end.Play();
            timeLeft = 15;
            TorRed.ResetScore();
            TorBlue.ResetScore();
            ball.transform.position = startPosBall;


        }
    }
}