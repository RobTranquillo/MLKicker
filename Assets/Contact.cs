using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contact : MonoBehaviour
{
    public Team isTeam;

    private Collider _ballCollider; 
    private GameController _game;

    void Start()
    {
        _game = FindObjectOfType<GameController>();
        _ballCollider = GameObject.FindWithTag("Ball").GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider == _ballCollider)
            _game.BallPossession = isTeam;
    }
    
}
