using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usage : MonoBehaviour
{
    public GameObject[] objects = new GameObject[2];
    public Material selectedMaterial;
    public Material unselectedMaterial;
    public bool state;
    
    
    // Update is called once per frame
    void Update()
    {
        Material thisMat = state == true ? selectedMaterial : unselectedMaterial;
        foreach (var go in objects)
            go.GetComponent<Renderer>().material = thisMat;
    }
}
