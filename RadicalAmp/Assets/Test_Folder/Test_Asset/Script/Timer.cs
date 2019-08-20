using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{

    public float timeCurrent = 0;
    public float timeMax = 0;

    public void Start(float max)
    {
        timeMax = max;
        timeCurrent = timeMax;
    }

    public void Tick()
    {
        timeCurrent -= Time.deltaTime;
    }
    
    public void ResetTimer()
    {
        timeCurrent = timeMax;
    }
}