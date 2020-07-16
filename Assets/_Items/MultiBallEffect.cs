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
            GetComponent<Collider>().enabled = false;
            StartCoroutine(ActivateCollider());
            
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

    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(livingTime);
        GetComponent<Collider>().enabled = true;
    }
}
