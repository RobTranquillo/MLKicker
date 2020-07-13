using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Change the appearance depending on selected state 
/// </summary>
public class Usage : MonoBehaviour
{
    public GameObject[] objects = new GameObject[2];
    public Material selectedMaterial;
    public Material unselectedMaterial;
    private bool _state = false;
    
    public void SetState(bool state)
    {
        if (state == _state)
            return;
        
        _state = state;
        Material thisMat = _state == true ? selectedMaterial : unselectedMaterial;
        foreach (var go in objects)
            go.GetComponent<Renderer>().material = thisMat;
    }
}
