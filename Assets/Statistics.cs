using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    private int episodes = 0;
    private int outs = 0;
    private int rests = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void AddOut()
    {
        outs++;
    }
    
    public void AddEpisode()
    {
        episodes++;
        if (episodes % 10 == 0)
        {
            Debug.Log("episodes: " + episodes + " outs: " + outs + " rests: " + rests);
        }
    }

    public void AddRest()
    {
        rests++;
    }
}
