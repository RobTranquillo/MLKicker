using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

[Flags]
public enum Hand
{
    None = 0b_0000_0000,    // 0
    Defence = 0b_0000_0001, // 1
    Offense = 0b_0000_0010  // 2
}    



[RequireComponent(typeof(PlayerInput))]
public class GamePadInput : MonoBehaviour
{
    public GameObject[] poleBars = new GameObject[4];
    public float poleSwitchDelay = 0.9f; //Seconds delay between switching the poles
    public float amplifyRotation = 10f;

    private Hand _hand;
    private float _nextSwitch;
    private Vector3[] _poleBarDefaultPos = new Vector3[4];
    private LimitDragging[] _barLimit = new LimitDragging[4];

    // actions via the action map and PlayerInput script 
    // hint: PlayerInputManager 	Handles setups that allow for several players including scenarios such as player lobbies and split-screen gameplay.
    private float _controllerALeftRotate;
    private float _controllerALeftDrag;
    private float _controllerARightRotate;
    private float _controllerARightDrag;
    private bool _controllerADefense;
    private bool _controllerAOffense;
    public void OnRotatePoleLeftHand(InputValue context)
    {
        _controllerALeftRotate = context.Get<float>();
    }
    public void OnRotatePoleRightHand(InputValue context)
    {
        _controllerARightRotate = context.Get<float>();
    }
    public void OnDragPoleLeftHand(InputValue context)
    {
        _controllerALeftDrag = context.Get<float>();
    }
    public void OnDragPoleRightHand(InputValue context)
    {
        _controllerARightDrag = context.Get<float>();
    }
    public void OnOffense()
    {
        SwitchHand(Hand.Offense);
    }
    public void OnDefense()
    {
        SwitchHand(Hand.Defence);
    }
    
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            _poleBarDefaultPos[i] = poleBars[i].transform.localPosition;
            _barLimit[i] = poleBars[i].GetComponent<LimitDragging>();
        }

        _nextSwitch = Time.time;
        _hand = Hand.Offense;
        SwitchHand(Hand.Defence);
    }

    void Update()
    {
        Rotating();
        Dragging();
    }

    private void Dragging()
    {
        short[] selectedBars = new short[]{0,1};
        if (_hand == Hand.Offense)
            selectedBars = new short[]{2,3};

        if (_barLimit[selectedBars[0]].limited())
            Debug.Log("Ich bin am Ende! Bitte nicht weiter draggen");

        if (_barLimit[selectedBars[1]].limited())
            Debug.Log("Ich bin am Ende! Bitte nicht weiter draggen");


        Vector3 pos;


        pos = poleBars[selectedBars[0]].transform.localPosition;
        pos.x = poleBars[selectedBars[0]].transform.localPosition.x - _controllerALeftDrag / 10;

        
        if (!_barLimit[selectedBars[0]].limited()) 
        { 
            poleBars[selectedBars[0]].transform.localPosition = pos;
        }

        if (_barLimit[selectedBars[0]].limited())
        {
            pos = poleBars[selectedBars[0]].transform.localPosition;
            pos.x = poleBars[selectedBars[0]].transform.localPosition.x - _controllerARightDrag / 10;
        }
        

        if (!_barLimit[selectedBars[1]].limited()) 
        { 
            poleBars[selectedBars[1]].transform.localPosition = pos;
        }

        if (_barLimit[selectedBars[1]].limited())
        {
            pos = poleBars[selectedBars[1]].transform.localPosition;
            pos.x = poleBars[selectedBars[1]].transform.localPosition.x - _controllerARightDrag / 10;
        }
    }

    private void Rotating()
    {
        if (_hand == Hand.Defence)
        {
            poleBars[0].transform.Rotate(_controllerALeftRotate * amplifyRotation, 0, 0);
            poleBars[1].transform.Rotate(_controllerARightRotate * amplifyRotation, 0, 0);
        }
        else
        {
            poleBars[2].transform.Rotate(_controllerALeftRotate * amplifyRotation, 0, 0);
            poleBars[3].transform.Rotate(_controllerARightRotate * amplifyRotation, 0, 0);
        }
    }
    
    private void SwitchHand(Hand newHand)
    {
        if (Time.time < _nextSwitch || _hand == newHand)
            return;        
        _nextSwitch = Time.time + poleSwitchDelay;   
        
        _hand = newHand;
        if (_hand == Hand.Defence)
        {
            poleBars[0].GetComponent<Usage>().SetState(true);
            poleBars[1].GetComponent<Usage>().SetState(true);
            poleBars[2].GetComponent<Usage>().SetState(false);
            poleBars[3].GetComponent<Usage>().SetState(false);
        }
        else
        {
            poleBars[0].GetComponent<Usage>().SetState(false);
            poleBars[1].GetComponent<Usage>().SetState(false);
            poleBars[2].GetComponent<Usage>().SetState(true);
            poleBars[3].GetComponent<Usage>().SetState(true);
        }
    }
}
