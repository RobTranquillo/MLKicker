// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class BouncyBallEffect : MonoBehaviour
// {
//     public GameObject Ball;
//     public ParticleSystem Collect;
//     public Renderer rend;
//     public Collider col;
//     Collider colBall;
//
//     public bool ItemHit;
//     public bool beingHandled;
//     
//     void Start()
//     {
//         rend = GetComponent<Renderer>();
//         col = GetComponent<Collider>();
//         colBall = Ball.GetComponent<Collider>();
//     }
//     
//     void Update()
//     {
//         if (ItemHit)
//         {
//             if (rend.enabled)
//                 StartCoroutine(HandleIt());
//         }
//     }
//
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.CompareTag("Ball"))
//         {
//             col.enabled = false;
//             rend.enabled = false;
//             if (Collect)
//                 Collect.Play();
//             ItemHit = true;  
//
//         }
//     }
//
//     private IEnumerator HandleIt()
//     {
//         beingHandled = true;
//
//         yield return new WaitForSeconds(1);
//
//         colBall.material.bounciness = 1;
//
//         yield return new WaitForSeconds(10);
//
//         colBall.material.bounciness = 0.5f;
//
//         yield return new WaitForSeconds(1);
//
//         rend.enabled = true; //Shalte den Renderer wieder ein
//         col.enabled = true; //Schalte den Collider wieder ein
//         ItemHit = false;
//
//         beingHandled = false;
//     }
// }

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BouncyBallEffect : MonoBehaviour
{
    public ItemEffectBouncyBall settings;
    private GameObject _ball;
    private float _bounciness;
    private Collider _thisCollider;
    private Renderer _thisRenderer;
    
    public void Start()
    {
        _ball = GameObject.FindWithTag("Ball");
        _bounciness = _ball.GetComponent<Collider>().material.bounciness;
        _thisCollider = GetComponent<Collider>();
        _thisRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _ball)
        {
            SwitchItem();
            
            if (settings.particleEffect)
            {
                var ps = Instantiate(settings.particleEffect, this.transform).GetComponent<ParticleSystem>();
                ps.Play();
            }
            
            StartCoroutine(ResetBounciness(_bounciness));
            _bounciness = settings.bounciness;
        }
    }

    private IEnumerator ResetBounciness(float val)
    {
        yield return new WaitForSeconds(settings.duration);
        _bounciness = val;
    }

    private void SwitchItem()
    {
        _thisCollider.enabled = ! _thisCollider.enabled;
        _thisRenderer.enabled = ! _thisRenderer.enabled;
        if (! _thisCollider.enabled)
            Invoke("SwitchItem", settings.duration);
    }
}