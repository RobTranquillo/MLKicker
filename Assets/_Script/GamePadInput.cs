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



public class GamePadInput : MonoBehaviour
{
    public GameObject[] poleBars = new GameObject[4];
    public float poleSwitchDelay = 0.9f; //Seconds delay between switching the poles
    public bool useSecondGamePad = false;
    
    private Hand _hand;
    private Gamepad _gamePad;
    
    float _nextSwitch;
    
    void Start()
    {
        _gamePad = Gamepad.current;

        if (useSecondGamePad)
        {
            var allGamePads = Gamepad.all;
            if (allGamePads.Count > 1)
            {
                _gamePad = allGamePads[1];
                Debug.Log("Second GamePad detected. Player Red playable.");
            }            
        }

        _nextSwitch = Time.time;
        
        _hand = Hand.Offense;
        SwitchHand(Hand.Defence);
    }

    void Update()
    {
        Switching();
        Rotating();
    }

    private void Rotating()
    {
        float amplify = 10f;

        if (_hand == Hand.Defence)
        {
            poleBars[0].transform.Rotate(_gamePad.leftStick.left.ReadValue() * amplify, 0,0); 
            poleBars[0].transform.Rotate(- _gamePad.leftStick.right.ReadValue() * amplify, 0,0);
            poleBars[1].transform.Rotate(_gamePad.rightStick.left.ReadValue() * amplify, 0,0); 
            poleBars[1].transform.Rotate(- _gamePad.rightStick.right.ReadValue() * amplify, 0,0);
        }
        else
        {
            poleBars[2].transform.Rotate(_gamePad.leftStick.left.ReadValue() * amplify, 0,0); 
            poleBars[2].transform.Rotate(- _gamePad.leftStick.right.ReadValue() * amplify, 0,0);
            poleBars[3].transform.Rotate(_gamePad.rightStick.left.ReadValue() * amplify, 0,0); 
            poleBars[3].transform.Rotate(- _gamePad.rightStick.right.ReadValue() * amplify, 0,0);
        }
    }

    private void Switching()
    {
        if (Time.time < _nextSwitch)
            return;
        
        var left = _gamePad.leftTrigger.ReadValue();
        var right = _gamePad.rightTrigger.ReadValue();
        if (left <= 0.2f && right <= 0.2f || left == 0 && right == 0)
            return;
            
        SwitchHandLeft(left);
        SwitchHandRight(right);
        _nextSwitch = Time.time + poleSwitchDelay;        
    }
    private void SwitchHandLeft(float val)
    {
        if (val >= 0.2f && _hand == Hand.Offense)
            SwitchHand(Hand.Defence);
    }
    private void SwitchHandRight(float val)
    {
        if (val >= 0.2f && _hand == Hand.Defence)
            SwitchHand(Hand.Offense);
    }
    private void SwitchHand(Hand newHand)
    {
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
