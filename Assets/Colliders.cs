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

    public BoxCollider pole;
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
        touchedPole = collision.collider == pole;
    }
    void OnCollisionExit()
    {
        ResetValues();
        // würde sagen, das wird nicht benötigt. einfach beim verlassen alle states zurücksetzen
        // das ist dann gefärlich bis falsch, wenn mehrere Objekte zeitüberlagernd collidieren können. zB. Player & HomeZone 
        // touchedHomeZone = collision.collider == homeZone;
        // touchedTargetZone = collision.collider == targetZone;
        // touchedPlayer = (collision.collider == polePlayer1 || collision.collider == polePlayer2 || collision.collider == polePlayer3);
        // touchedPole = collision.collider == pole;
        // touchedTarget = collision.collider == target;
        // touchedSelfGoal = collision.collider == selfGoal;
    }
}
