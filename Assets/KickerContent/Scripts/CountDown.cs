using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text time;
    public ParticleSystem end;
 
    private GameController _game;
    private TorTrigger _goalRed;
    private TorTrigger _goalBlue;
    private float _timeLeft;

    private void Start()
    {
        _game = FindObjectOfType<GameController>();
        _goalBlue = GameObject.FindWithTag("BlueGoal").GetComponentInChildren<TorTrigger>();
        _goalRed = GameObject.FindWithTag("RedGoal").GetComponentInChildren<TorTrigger>();
        _timeLeft = _game.gameDuration;
    }

    void Update()
    {
        _timeLeft -= Time.deltaTime;
        time.text = "Time Left:" + Mathf.Round(_timeLeft);
        if (_timeLeft < 0)
        {
            if (end)
                end.Play();
            _game.ThrowBallIn();
            _timeLeft = (float) _game.gameDuration;
            _goalRed.ResetScore();
            _goalBlue.ResetScore();
        }
    }
}