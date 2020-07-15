using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleBegrenzung : MonoBehaviour
{
    public bool isColliding = false;
    public GameObject KickerStand;
    Collider KickerStandCol;

    // Start is called before the first frame update
    void Start()
    {
        KickerStandCol = KickerStand.GetComponent<Collider>();
        isColliding = false;
    }

    public void OnTriggerEnter(Collider collision)
    {
        //isColliding = collision.gameObject.tag == "KickerStand";


        //isColliding = KickerStandCol;

        //isColliding = collision == KickerStandCol;

        if (collision == KickerStandCol)
        {
            isColliding = true;
        }

    }
    public void OnTriggerExit(Collider collision)
    {
        isColliding = false;
    }
}
