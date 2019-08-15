using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;

public class VFXIntensity : MonoBehaviour
{
    private Slider Juicemeter;
    [SerializeField] VisualEffect Left;
    [SerializeField] VisualEffect Right;
    [SerializeField] ParticleSystem Trail;

    ParticleSystem.MainModule ps;


    // Start is called before the first frame update
    void Start()
    {
        Juicemeter = GameObject.FindGameObjectWithTag("JuiceMeter").GetComponent<Slider>();
        ps = Trail.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Juicemeter.value >= 0 && Juicemeter.value <= 25)
        {
            Left.SetFloat("Particle Base Size", 0.1f);
            Right.SetFloat("Particle Base Size", 0.1f);
            ps.startSizeMultiplier = 0.02f;

            Left.SetFloat("Particle Velocity", 0.1f);
            Right.SetFloat("Particle Velocity", 0.1f);
        }
        else if (Juicemeter.value >= 26 && Juicemeter.value <= 50)
        {
            Left.SetFloat("Particle Base Size", 0.3f);
            Right.SetFloat("Particle Base Size", 0.3f);
            ps.startSizeMultiplier = 0.04f;

            Left.SetFloat("Particle Velocity", 0.3f);
            Right.SetFloat("Particle Velocity", 0.3f);
        }

        else if (Juicemeter.value >= 51 && Juicemeter.value <= 75)
        {
            Left.SetFloat("Particle Base Size", 0.5f);
            Right.SetFloat("Particle Base Size", 0.5f);
            ps.startSizeMultiplier = 0.09f;

            Left.SetFloat("Particle Velocity", 0.5f);
            Right.SetFloat("Particle Velocity", 0.5f);
        }

        else if (Juicemeter.value >= 76 && Juicemeter.value <= 100)
        {
            Left.SetFloat("Particle Base Size", 1.0f);
            Right.SetFloat("Particle Base Size", 1.0f);
            ps.startSizeMultiplier = 1.5f;

            Left.SetFloat("Particle Velocity", 1.0f);
            Right.SetFloat("Particle Velocity", 1.0f);
        }
    }
}
