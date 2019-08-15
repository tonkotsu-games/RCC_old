using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReaction : MonoBehaviour
{
    [Header("Light Intensity")]
    [SerializeField] private float onBeat, offBeat;

    [SerializeField] private string[] lightName;
    private GameObject[][] lightSet;
    private Light[][] lightTest; 

    private BeatStrike beat;

    private bool counting = false;

    public int count;
    private int lightNumber;

    // Start is called before the first frame update
    void Start()
    {
        beat = GameObject.FindWithTag("Player").GetComponent<BeatStrike>();
        lightTest = new Light[lightName.Length][];
        lightSet = new GameObject[lightName.Length][];
        for (int i = 0; i < lightName.Length; i++)
        {
            lightSet[i] = GameObject.FindGameObjectsWithTag(lightName[i]);
            lightTest[i] = new Light[lightSet[i].Length];
            for(int j = 0;j < lightSet[i].Length;j++)
            {
                lightTest[i][j] = lightSet[i][j].GetComponent<Light>();
            }
        }

        lightNumber = lightName.Length;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(lightName.Length == 1)
        {
            LonleyLight();
        }
        else
        {
            if (beat.IsOnBeat())
            {
                LightReact();
            }
            Counting();
        }
    }
    private void LonleyLight()
    {
        if(beat.IsOnBeat())
        {
            for(int i = 0;i<lightTest[0].Length;i++)
            {
                lightTest[0][i].intensity = onBeat;
            }
        }
        else
        {
            for (int i = 0; i < lightTest[0].Length; i++)
            {
                lightTest[0][i].intensity = offBeat;
            }
        }
    }

    private void Counting()
    {
        if (beat.IsOnBeat() && !counting)
        {
            counting = true;
            if (count + 1 == lightName.Length)
            {
                count = 0;
            }
            else
            {
                count++;
            }
        }
        else if (!beat.IsOnBeat() && counting)
        {
            counting = false;
        }
    }
    private void LightReact()
    {
        for(int i = 0;i<lightName.Length;i++)
        {
            if(i == count)
            {
                for(int j = 0;j<lightTest[i].Length;j++)
                {
                    lightTest[i][j].intensity = onBeat;
                }
            }
            else if(i != count)
            {
                for(int j = 0;j<lightTest[i].Length;j++)
                {
                    lightTest[i][j].intensity = offBeat;
                }
            }
        }
    }
}
