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

    public bool limited()
    {
        return Mathf.Abs(standL.position.z - stopperL.position.z) < 2f || 
               Mathf.Abs(standR.position.z - stopperR.position.z) < 2f;
    }
    
    

}
