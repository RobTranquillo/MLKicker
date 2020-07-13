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
    public Material selectedMaterial;
    public Material unselectedMaterial;

    public float poleSwitchDelay = 0.9f; //Seconds delay between switching the poles
    
    private Hand _hand;
    private Gamepad _gamePad;
    
    float _nextSwitch;
    
    void Start()
    {
        _gamePad = Gamepad.current;
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
            poleBars[0].transform.Rotate(_gamePad.leftStick.up.ReadValue() * amplify, 0,0); 
            poleBars[0].transform.Rotate(- _gamePad.leftStick.down.ReadValue() * amplify, 0,0);
            poleBars[1].transform.Rotate(_gamePad.rightStick.up.ReadValue() * amplify, 0,0); 
            poleBars[1].transform.Rotate(- _gamePad.rightStick.down.ReadValue() * amplify, 0,0);
        }
        else
        {
            poleBars[2].transform.Rotate(_gamePad.leftStick.up.ReadValue() * amplify, 0,0); 
            poleBars[2].transform.Rotate(- _gamePad.leftStick.down.ReadValue() * amplify, 0,0);
            poleBars[3].transform.Rotate(_gamePad.rightStick.up.ReadValue() * amplify, 0,0); 
            poleBars[3].transform.Rotate(- _gamePad.rightStick.down.ReadValue() * amplify, 0,0);
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
        Debug.Log(("Handwechsel: " + newHand));
        _hand = newHand;

        if (_hand == Hand.Defence)
        {
            poleBars[0].GetComponent<Usage>().state = true;
            poleBars[1].GetComponent<Usage>().state = true;
            poleBars[2].GetComponent<Usage>().state = false;
            poleBars[3].GetComponent<Usage>().state = false;
        }
        else
        {
            poleBars[0].GetComponent<Usage>().state = false;
            poleBars[1].GetComponent<Usage>().state = false;
            poleBars[2].GetComponent<Usage>().state = true;
            poleBars[3].GetComponent<Usage>().state = true;
        }
    }
}
