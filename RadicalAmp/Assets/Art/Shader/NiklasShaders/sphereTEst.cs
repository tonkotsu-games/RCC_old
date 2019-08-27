using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereTEst : MonoBehaviour
{
    BeatAnalyse beatAnal;
    MeshRenderer renderer;
    bool started = false;
    float i = 0;

    private void Start()
    {
        beatAnal = GameObject.FindWithTag("MusicBox").GetComponent<BeatAnalyse>();
        renderer = gameObject.GetComponent<MeshRenderer>();
    }
    void Update()
    {
    
        if (beatAnal.IsOnBeat(5000))
        {
            started = true;
        }

        if (started)
        {
            if (i < 10)
            {
                i += 1.5f;
                renderer.materials[0].SetFloat("_Amount", i);
                renderer.materials[0].SetFloat("_Alpha", i);
            }
            else
            {
                started = false;
                i = 0.1f;
                renderer.materials[0].SetFloat("_Amount", i);
                renderer.materials[0].SetFloat("_Alpha", i);

            }
        }
    }
}
