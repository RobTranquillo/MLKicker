using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalShrink : MonoBehaviour
{
    public GameObject GoalRed;
    public GameObject GoalBlue;
    public GameObject Ball;
    public GameObject GoalPosRed;
    public GameObject GoalPosBlue;
    public GameObject ColliderRed;
    public GameObject ColliderBlue;

    public ParticleSystem Collect;

    public Renderer rend;

    public Collider col;
    Collider colBall;

    public bool ItemHit;
    public bool beingHandled;

    private Vector3 GoalBluePosOrigin;
    private Vector3 GoalRedPosOrigin;


    public int livingTime = 4;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>(); //hole mir den Renderer des Items
        col = GetComponent<Collider>(); //hole mir den Collider des Items
        colBall = Ball.GetComponent<Collider>();
        GoalBluePosOrigin = GoalBlue.transform.position;
        GoalRedPosOrigin = GoalRed.transform.position;
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
            DeactivateItem();
            if (Collect)    //Spiel das Partikel System ab als Feedback
                Collect.Play();
            ItemHit = true; //Dann schalte den bool dafür auf true 

        }
    }
    private void DeactivateItem()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(ActivateItem());
    }

    IEnumerator ActivateItem()
    {
        yield return new WaitForSeconds(livingTime);
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;
    }

    private IEnumerator HandleIt()
    {
        beingHandled = true;

        yield return new WaitForSeconds(1);

        GoalBlue.gameObject.transform.localScale = new Vector3(60, 60, 60);
        GoalRed.gameObject.transform.localScale = new Vector3(60, 60, 60);
        GoalBlue.transform.position = GoalPosBlue.transform.position;
        GoalRed.transform.position = GoalPosRed.transform.position;
        ColliderRed.SetActive(true);
        ColliderBlue.SetActive(true);


    yield return new WaitForSeconds(10);

        GoalBlue.gameObject.transform.localScale = new Vector3(100, 100, 100);
        GoalRed.gameObject.transform.localScale = new Vector3(100, 100, 100);
        GoalBlue.transform.position = GoalBluePosOrigin;
        GoalRed.transform.position = GoalRedPosOrigin;
        ColliderRed.SetActive(false);
        ColliderBlue.SetActive(false);

        yield return new WaitForSeconds(1);

        
        ItemHit = false;

        beingHandled = false;
    }

}
