using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;

public class VFXIntensity : MonoBehaviour
{
    private Slider juiceMeter;
    //[SerializeField] VisualEffect Example;
    [SerializeField] ParticleSystem weaponTrail;
    [SerializeField] Material bodyMat;
    [SerializeField] Material capeMat;

    [SerializeField] float weaponTrailMulti;
    [SerializeField] float emissionMulti;
    //[SerializeField] float ExampleMulti;    

    ParticleSystem.MainModule weaponParticleModule;

    void Start()
    {
        juiceMeter = Locator.instance.GetJuiceMeter();
        weaponParticleModule = weaponTrail.main;
        //bodyMat.SetFloat("Vector1_244E07B5", 1f);
    }

    void Update()
    {
        weaponParticleModule.startSizeMultiplier = weaponTrailMulti * juiceMeter.value;
        bodyMat.SetFloat("_emissnIntensity", juiceMeter.value * emissionMulti + 1);
        //Example.SetFloat("Particle Base Size", ExampleMulti * juiceMeter.value);
    }
}