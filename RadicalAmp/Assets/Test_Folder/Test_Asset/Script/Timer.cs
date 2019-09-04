using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{

    public float timeCurrent = 0;
    public float timeMax = 0;
    public bool paused = false;

    public void Start(float max)
    {
        timeMax = max;
        timeCurrent = timeMax;
    }

    public void Tick()
    {
        if(!paused)
        {
            timeCurrent -= Time.deltaTime;
        }
    }
    
    public void ResetTimer()
    {
        timeCurrent = timeMax;
    }

    public void TogglePause()
    {
        paused = !paused;
    }
}