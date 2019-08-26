using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseOnBeat : MonoBehaviour
{
    [SerializeField]
    float pulseAmount;
    Light light;
    float baseIntensity;

    private void Start()
    {
        light = gameObject.GetComponent<Light>();
        baseIntensity = light.intensity;
    }

    private void Update()
    {
        PulseMe();
    }

    void PulseMe()
    {

        if (BeatStrike.instance.IsOnBeat())
        {
            if (BeatStrike.pulseBeat)
            {
                light.intensity += pulseAmount;
            }
        }
        else
        {
            light.intensity = baseIntensity;
        }
    }

}
