using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;


public enum Team
{
    None,
    Red,
    Blue
}

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Holds parameters to control the game
    /// </summary>
    
    [Range(1,100)]
    [Tooltip("Dauer einer Runde")]
    public int gameDuration = 15;
    [Range(0.1f,10f)]
    [Tooltip("Zeit die der Ball wartet beim Einwurf, wenn das Spiel beginnt oder nach Tor.")]
    public float startGameDelay = 1.3f;
    public GameObject ballPrefab;
    
    public Team BallPossession { get; set; }
    
    [Tooltip("Drop an Object to use this position as start position")]
    public Transform ballStartPosition;
    
    private GameObject _ball;
    private Vector3 _startPos;

    public void Start()
    {
        _ball = GameObject.FindWithTag("Ball");
        _startPos = ballStartPosition.position;
        ThrowBallIn();
    }

    public void ThrowBallIn()
    {
        _ball.transform.position = _startPos;
        _ball.GetComponent<Rigidbody>().isKinematic = true;
        Invoke("GameStartDelayed", startGameDelay);
    }

    private void GameStartDelayed() {
        _ball.GetComponent<Rigidbody>().isKinematic = false;
    }
}
