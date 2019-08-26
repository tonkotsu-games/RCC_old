using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;

public class VFXIntensity : MonoBehaviour
{
    private Slider juiceMeter;
    [SerializeField] VisualEffect leftTrail;
    [SerializeField] VisualEffect rightTrail;
    [SerializeField] ParticleSystem weaponTrail;

    [SerializeField] float weaponTrailMulti;
    [SerializeField] float dashTrailMulti;    

    ParticleSystem.MainModule weaponParticleModule;

    void Start()
    {
        juiceMeter = GameObject.FindGameObjectWithTag("JuiceMeter").GetComponent<Slider>();
        weaponParticleModule = weaponTrail.main;
    }

    void Update()
    {
        weaponParticleModule.startSizeMultiplier = weaponTrailMulti * juiceMeter.value;


        if (juiceMeter.value >= 0 && juiceMeter.value <= 25)
        {
            leftTrail.SetFloat("Particle Base Size", 0.1f);
            rightTrail.SetFloat("Particle Base Size", 0.1f);

            leftTrail.SetFloat("Particle Velocity", 0.1f);
            rightTrail.SetFloat("Particle Velocity", 0.1f);
        }
        else if (juiceMeter.value >= 26 && juiceMeter.value <= 50)
        {
            leftTrail.SetFloat("Particle Base Size", 0.3f);
            rightTrail.SetFloat("Particle Base Size", 0.3f);

            leftTrail.SetFloat("Particle Velocity", 0.3f);
            rightTrail.SetFloat("Particle Velocity", 0.3f);
        }

        else if (juiceMeter.value >= 51 && juiceMeter.value <= 75)
        {
            leftTrail.SetFloat("Particle Base Size", 0.5f);
            rightTrail.SetFloat("Particle Base Size", 0.5f);

            leftTrail.SetFloat("Particle Velocity", 0.5f);
            rightTrail.SetFloat("Particle Velocity", 0.5f);
        }

        else if (juiceMeter.value >= 76 && juiceMeter.value <= 100)
        {
            leftTrail.SetFloat("Particle Base Size", 1.0f);
            rightTrail.SetFloat("Particle Base Size", 1.0f);

            leftTrail.SetFloat("Particle Velocity", 1.0f);
            rightTrail.SetFloat("Particle Velocity", 1.0f);
        }
    }
}