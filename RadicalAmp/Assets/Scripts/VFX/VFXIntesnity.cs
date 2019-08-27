using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;


public class VFXIntesnity : MonoBehaviour
{
    [Header("VFX Prefab")]
    [SerializeField] VisualEffect vfx;
    [Header("VFX Multiplayer")]
    [SerializeField] float vfxMulti;
    [Header("VFX emission Name")]
    [SerializeField] string vfxName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vfx.SetFloat(vfxName, vfxMulti * juiceMeter.value);
    }
}
