using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BombBallEffect : MonoBehaviour
{
    public ItemEffectBombBall settings;
    private GameObject _ball;

    public void Start()
    {
        _ball = GameObject.FindWithTag("Ball");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _ball)
        {
            //DeactivateItem();
            if (settings.particleEffect)
            {
                var ps = Instantiate(settings.particleEffect, this.transform).GetComponent<ParticleSystem>();
                ps.Play();
            }
            //SpawnBalls();
        }
    }

  /* private void SpawnBalls()
    {
        for (int i = 0; i < settings.count; i++)
            Destroy(
                Instantiate(_ball, transform.position, Quaternion.identity), 
                settings.livingTime);
    }

    private void DeactivateItem()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(ActivateItem());
    }
    IEnumerator ActivateItem()
    {
        yield return new WaitForSeconds(settings.livingTime);
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;
    }*/
}
