using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleStartSize : MonoBehaviour
{
    [Header("Particle System Prefab")]
    [SerializeField] ParticleSystem particleSystem;
    [Header("Particle System Multiplayer")]
    [SerializeField] float particleMulti;
    ParticleSystem.MainModule particleModule;

    private Slider juiceMeter;

    void Start()
    {
        particleModule = particleSystem.main;
        juiceMeter = Locator.instance.GetJuiceMeter();
    }

    void Update()
    {
        particleModule.startSizeMultiplier = particleMulti * juiceMeter.value;

    }
}
