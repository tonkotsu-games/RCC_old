using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseOnBeat : MonoBehaviour
{
    [SerializeField]
    float pulseAmount;
    Light light;
    float baseIntensity;

    float baseTemp;

    private void Start()
    {
        light = gameObject.GetComponent<Light>();
        baseIntensity = light.intensity;
        baseTemp = light.colorTemperature;

    }

    private void Update()
    {
        BasePulsing();
        PulseMe();
    }

    void PulseMe()
    {

        if (BeatStrike.instance.IsOnBeat())
        {
            if (BeatStrike.pulseBeat)
            {
                if(light.intensity > 1000)
                {
                    light.colorTemperature = 1000;
                    pulseAmount = 200;
                }
                light.intensity += pulseAmount;
            }
        }
        else
        {
            light.intensity = baseIntensity;
            light.colorTemperature = baseTemp;

        }
    }

    void BasePulsing()
    {
        if (BeatStrike.instance.IsOnBeat())
        {
            light.intensity += 2f;
        }
        else
        {
            light.intensity = baseIntensity;
        }
    }

}
