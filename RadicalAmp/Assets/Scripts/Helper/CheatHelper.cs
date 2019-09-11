using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatHelper : MonoBehaviour
{
    [SerializeField] Slider juiceMeter;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            juiceMeter.value += 100;
            Debug.LogError("Cheat");
        }
    }
}
