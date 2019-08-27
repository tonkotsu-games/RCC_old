using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEmissive : MonoBehaviour
{

    Material material;
    Color emissionColor;
    public float pulseSpeed = 1;

    float baseIntensity;
    float maxIntensity;
    float maxOnHitIntensity;
    public float intensityIncrease = 3;
    public float intensityIncreaseOnHit = 8;
    public bool lerping = false;

    [SerializeField]
    BeatAnalyse musicBox;


    // Start is called before the first frame update
    void Start()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
        musicBox = Locator.instance.GetBeat();
        baseIntensity = material.GetFloat("_Intensity");
        maxIntensity = baseIntensity + intensityIncrease;
        maxOnHitIntensity = baseIntensity + intensityIncreaseOnHit;
        
    }

    // Update is called once per frame
    void Update()
    {
        PulseConstantly();
    }

    void PulseConstantly()
    {
        if (musicBox.IsOnBeat(1000))
        {
            material.SetFloat("_Intensity", maxIntensity);
        }
        else
        {
            if (lerping)
            {
                material.SetFloat("_Intensity", Mathf.Lerp(maxIntensity, baseIntensity, pulseSpeed));
            }
            else
            {
                material.SetFloat("_Intensity", baseIntensity);
            }
        }
    }


    void PulseOnHit()
    {

        if (BeatStrike.instance.IsOnBeat())
        {
            if (BeatStrike.pulseBeat)
            {
                material.SetFloat("_Intensity", maxOnHitIntensity);
            }
        }
        else
        {
            material.SetFloat("_Intensity", baseIntensity);
        }
    }
}
