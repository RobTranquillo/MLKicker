using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallEffect : MonoBehaviour
{
    public GameObject Ball;
    public ParticleSystem Collect;
    [Range(0, 20)]
    public int count = 4;
    [Range(0, 20)]
    public int livingTime = 4;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            DeactivateItem();
            if (Collect)
                Collect.Play();
            SpawnBalls();
        }
    }

    private void SpawnBalls()
    {
        for (int i = 0; i < count; i++)
            Destroy(
                Instantiate(Ball, transform.position, Quaternion.identity), 
                livingTime);
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
}
