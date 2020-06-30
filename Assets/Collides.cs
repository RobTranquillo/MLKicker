using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collides : MonoBehaviour
{
    [HideInInspector]
    public bool touchedPlayer = false;
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
        touchedTarget = false;
        touchedSelfGoal = false;
        touchedHomeZone = false;
        touchedTargetZone = false;
    }

    void OnTriggerEnter(Collider other)
    {
        touchedHomeZone = other == homeZone;
        touchedTargetZone = other == targetZone;
    }
    void OnTriggerExit(Collider other)
    {
        touchedHomeZone = other == homeZone;
        touchedTargetZone = other == targetZone;
    }
    void OnCollisionEnter(Collision collision)
    {
        touchedTarget = collision.collider == target;
        touchedSelfGoal = collision.collider == selfGoal;
        touchedPlayer = (collision.collider == polePlayer1 || collision.collider == polePlayer2 || collision.collider == polePlayer3);
    }
    void OnCollisionExit(Collision collision)
    {
        touchedHomeZone = collision.collider == homeZone;
        touchedTargetZone = collision.collider == targetZone;
        touchedPlayer = (collision.collider == polePlayer1 || collision.collider == polePlayer2 || collision.collider == polePlayer3);
        touchedTarget = collision.collider == target;
        touchedSelfGoal = collision.collider == selfGoal;
    }
}
