using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleStartSize : MonoBehaviour
{
    ParticleSystem particleSystem;
    [Header("Particle System Multiplier")]
    [SerializeField] float particleMulti;
    ParticleSystem.MainModule particleModule;
    ParticleSystem.EmissionModule particleEmission;

    private Slider juiceMeter;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleModule = particleSystem.main;
        particleEmission = particleSystem.emission;
        juiceMeter = Locator.instance.GetJuiceMeter();
    }

    void Update()
    {
        //particleModule.startSizeMultiplier = particleMulti * juiceMeter.value;
        particleEmission.rateOverTimeMultiplier = particleMulti * juiceMeter.value;
    }
}
