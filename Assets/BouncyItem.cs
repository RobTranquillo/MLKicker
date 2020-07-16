using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyItem : MonoBehaviour
{
    public GameObject Ball;

    public ParticleSystem Collect;

    public Renderer rend;

    public Collider col;
    Collider colBall;

    public bool ItemHit;
    public bool beingHandled;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>(); //hole mir den Renderer des Items
        col = GetComponent<Collider>(); //hole mir den Collider des Items
        colBall = Ball.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemHit == true)
        {

            if (rend.enabled == false) //Ist das Item unsichtbar?
            {
                StartCoroutine(HandleIt());
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball")) //Hat der Ball das Item getroffen?
        {
            col.enabled = false; //Collider des items wird ausgeschaltet
            rend.enabled = false; //Renderer des Items wird ausgeschaltet
            if (Collect)    //Spiel das Partikel System ab als Feedback
                Collect.Play();
            ItemHit = true; //Dann schalte den bool dafür auf true 

        }
    }

    private IEnumerator HandleIt()
    {
        beingHandled = true;

        yield return new WaitForSeconds(1);

        colBall.material.bounciness = 1;

        yield return new WaitForSeconds(10);

        colBall.material.bounciness = 0.5f;

        yield return new WaitForSeconds(1);

        rend.enabled = true; //Shalte den Renderer wieder ein
        col.enabled = true; //Schalte den Collider wieder ein
        ItemHit = false;

        beingHandled = false;
    }
}
