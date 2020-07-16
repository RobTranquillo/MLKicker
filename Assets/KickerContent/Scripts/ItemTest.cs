using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest : MonoBehaviour
{

    public GameObject Ball;
    public ParticleSystem Collect;
    public Renderer rend;
    public bool ItemHit;
    public Collider col;
    bool beingHandled = false;
    bool waitPlease = false;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>(); //hole mir den Renderer des Items
        col = GetComponent<Collider>(); //hole mir den Collider des Items
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemHit == true) //Item wurde getroffen (bool wird in OnTriggerEnter true gesetzt)
        {
            col.enabled = false; //Collider des items wird ausgeschaltet
            rend.enabled = false; //Renderer des Items wird ausgeschaltet
            

            if (rend.enabled == false) //Ist das Item unsichtbar?
            {
                StartCoroutine(HandleIt()); //Coroutine für das anschalten des Colliders und des Renderers
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball")) //Hat der Ball das Item getroffen?
        {
            ItemHit = true; //Dann schalte den bool dafür auf true 
            if (Collect)    //Spiel das Partikel System ab als Feedback
                Collect.Play();

            if (ItemHit == true) // Das Item wurde getroffen
            {
                StartCoroutine(WaitForIt()); //Coroutine für die Instanziierung und löschund der zusatz Bälle
            }


            
        }
    }
    private IEnumerator HandleIt()
    {
        beingHandled = true;
        // process pre-yield
        yield return new WaitForSeconds(15); //Warte bis alle Bälle Instanziiert wurden und wieder gelöscht sind
        // process post-yield
        rend.enabled = true; //Shalte den Renderer wieder ein
        col.enabled = true; //Schalte den Collider wieder ein
        beingHandled = false;
    }
    private IEnumerator WaitForIt()
    {
        waitPlease = true;
        // process pre-yield
        yield return new WaitForSeconds(3); //Warte 3 Sekunden bevor die Zusatz Bälle erscheinen (Damit der Collider ganz sicher ausgeschaltet ist)
        // process post-yield
        GameObject B1 = Instantiate(Ball, transform.position, Quaternion.identity); //Alle Bälle
        GameObject B2 = Instantiate(Ball, transform.position, Quaternion.identity);
        GameObject B3 = Instantiate(Ball, transform.position, Quaternion.identity);
        GameObject B4 = Instantiate(Ball, transform.position, Quaternion.identity);

        Destroy(B1, 8); //Lösche alle Bälle nach 8 Sekunden
        Destroy(B2, 8);
        Destroy(B3, 8);
        Destroy(B4, 8);
        ItemHit = false; //Das Item ist jetzt nicht mehr getroffen, sobald die andere Coroutine den Collider angeschaltet hat kann es wieder losgehen 
        waitPlease = false;
    }
}
