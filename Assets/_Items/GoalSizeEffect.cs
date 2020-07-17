using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GoalSizeEffect : MonoBehaviour
{
    public ItemEffectGoalSize settings;
    private GameObject _ball;
    private Transform _redGoal;
    private Transform _blueGoal;

    public void Start()
    {
        _ball = GameObject.FindWithTag("Ball");
        _redGoal = GameObject.FindWithTag("RedGoal").transform;
        _blueGoal = GameObject.FindWithTag("BlueGoal").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _ball)
        {
            DeactivateItem();
            if (settings.particleEffect)
            {
                var ps = Instantiate(settings.particleEffect, this.transform).GetComponent<ParticleSystem>();
                ps.Play();
            }
            SizeTheGoals();
            StartCoroutine(ResizeTheGoals());
        }
    }

    private IEnumerator ResizeTheGoals()
    {
        yield return new WaitForSeconds(settings.duration);
        _blueGoal.transform.localScale = _blueGoal.transform.localScale * settings.sizingFactor;
        _redGoal.transform.localScale = _redGoal.transform.localScale * settings.sizingFactor;
    }

    private void SizeTheGoals()
    {
        _blueGoal.localScale = _blueGoal.localScale / settings.sizingFactor;
        _redGoal.localScale = _redGoal.localScale / settings.sizingFactor;
    }

    private void DeactivateItem()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(ActivateItem());
    }
    IEnumerator ActivateItem()
    {
        yield return new WaitForSeconds(settings.duration);
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;
    }
}
