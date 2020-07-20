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