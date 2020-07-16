using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitDragging : MonoBehaviour
{
    public Transform stopperL;
    public Transform standL;
    public Transform stopperR;
    public Transform standR;

    public bool NoLimitedAt(float offset)
    {
        return ! (Mathf.Abs(standL.position.z - stopperL.position.z) + offset < 1f ||
                  Mathf.Abs(standR.position.z - stopperR.position.z) - offset < 1f);
    }
}
