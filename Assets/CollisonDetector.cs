using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonDetector : MonoBehaviour
{
    [HideInInspector]
    public bool isTouched = false;

    public void Reset()
    {
        isTouched = false;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        isTouched = true;
    }

    private void OnCollisionExit(Collision other)
    {
        isTouched = false;
    }
}
