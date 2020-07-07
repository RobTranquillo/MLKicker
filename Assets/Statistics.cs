using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    private int _episodes = 0;
    private int _outs = 0;
    private int _rests = 0;

    public void AddOut()
    {
        _outs++;
    }
    
    public void AddEpisode()
    {
        _episodes++;
        if (_episodes % 10 == 0)
        {
            Debug.Log("episodes: " + _episodes + " outs: " + _outs + " rests: " + _rests);
        }
    }

    public void AddRest()
    {
        _rests++;
    }
}
