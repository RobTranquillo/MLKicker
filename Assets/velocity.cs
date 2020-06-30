using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocity : MonoBehaviour
{
    public Rigidbody ball;

    void Update()
    {
        Debug.Log(ball.velocity);
    }
}
