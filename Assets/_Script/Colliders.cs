using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliders : MonoBehaviour
{
    [HideInInspector]
    public bool touchedPlayer = false;
    [HideInInspector]
    public bool touchedPole = false;
    [HideInInspector]
    public bool touchedTarget = false;
    [HideInInspector]
    public bool touchedSelfGoal = false;
    [HideInInspector]
    public bool touchedHomeZone = false;
    [HideInInspector]
    public bool touchedTargetZone = false;

    public BoxCollider polePlayer1;
    public BoxCollider polePlayer2;
    public BoxCollider polePlayer3;
    public BoxCollider target;
    public BoxCollider selfGoal;
    public BoxCollider homeZone;
    public BoxCollider targetZone;

    public void ResetValues()
    {
        touchedPlayer = false;
        touchedPole = false;
        touchedTarget = false;
        touchedSelfGoal = false;
        touchedHomeZone = false;
        touchedTargetZone = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == homeZone)
            touchedHomeZone = true;
        if (other == targetZone)
            touchedTargetZone = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other == homeZone)
            touchedHomeZone = false;
        if (other == targetZone)
            touchedTargetZone = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == target)
            touchedTarget = true;
        if (collision.collider == selfGoal)
            touchedSelfGoal = true;
        if (collision.collider == polePlayer1 || collision.collider == polePlayer2 || collision.collider == polePlayer3)
            touchedPlayer = true;
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider == target)
            touchedTarget = false;
        if (collision.collider == selfGoal)
            touchedSelfGoal = false;
        if (collision.collider == polePlayer1 || collision.collider == polePlayer2 || collision.collider == polePlayer3)
            touchedPlayer = false;
    }
}