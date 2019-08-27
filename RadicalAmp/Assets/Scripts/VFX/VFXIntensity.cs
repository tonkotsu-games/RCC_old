using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;

public class VFXIntensity : MonoBehaviour
{
    private Slider juiceMeter;
    //[SerializeField] VisualEffect Example;
    [SerializeField] ParticleSystem weaponTrail;

    [SerializeField] float weaponTrailMulti;
    //[SerializeField] float ExampleMulti;    

    ParticleSystem.MainModule weaponParticleModule;

    void Start()
    {
        juiceMeter = Locator.instance.GetJuiceMeter();
        weaponParticleModule = weaponTrail.main;
    }

    void Update()
    {
        weaponParticleModule.startSizeMultiplier = weaponTrailMulti * juiceMeter.value;
        //Example.SetFloat("Particle Base Size", ExampleMulti * juiceMeter.value);
    }
}