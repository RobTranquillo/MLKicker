using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitPoleMov : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Stopper1;
    public GameObject Stopper2;
    public GameObject Staender1;
    public GameObject Staender2;

    public float dis1;
    public float dis2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        dis1 = Vector3.Distance(Stopper1.transform.position, Staender1.transform.position);


        //dis1 = Mathf.Abs(Stopper1.transform.localPosition.x - Staender1.transform.localPosition.x);
        //dis2 = Mathf.Abs(Stopper2.transform.localPosition.x - Staender2.transform.localPosition.x);



        dis2 = Vector3.Distance(Stopper2.transform.position, Staender2.transform.position);

    }
}
