using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStartSize : MonoBehaviour
{
    [Header("Particle System Prefab")]
    [SerializeField] ParticleSystem particleSystem;
    [Header("Particle System Multiplayer")]
    [SerializeField] float particleMulti;
    ParticleSystem.MainModule particleModule;


    void Start()
    {
        particleModule = particleSystem.main;
    }

    void Update()
    {
        particleModule.startSizeMultiplier = particleMulti * juiceMeter.value;

    }
}
