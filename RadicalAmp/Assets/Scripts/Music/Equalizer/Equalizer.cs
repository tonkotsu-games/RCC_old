using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equalizer : MonoBehaviour
{
    [SerializeField] Slider equalizerRightPrefab;
    [SerializeField] Slider equalizerLeftPrefab;
    [SerializeField] Slider juiceMeter;
    [SerializeField] GameObject canvasParent;
    [SerializeField] Color[] colorEqualizer;
    Slider[] equalizerRight = new Slider[16];
    Slider[] equalizerLeft = new Slider[16];

    public float maxscale;

    void Start()
    {
        for (int i = 0; i < 16; i++)
        {
            Slider instantSlider = Instantiate(equalizerRightPrefab);
            instantSlider.transform.SetParent(canvasParent.transform, false);
            instantSlider.name = "SampleCube: " + i;
            instantSlider.GetComponent<RectTransform>().localPosition = new Vector3(400,
                                                                                    -175 + (25f * i),
                                                                                    0);
            equalizerRight[i] = instantSlider;
        }
        for (int i = 0; i < 16; i++)
        {
            Slider instantSlider = Instantiate(equalizerLeftPrefab);
            instantSlider.transform.SetParent(canvasParent.transform, false);
            instantSlider.name = "SampleCube: " + i;
            instantSlider.GetComponent<RectTransform>().localPosition = new Vector3(-400,
                                                                                    -175 + (25f * i),
                                                                                    0);
            equalizerLeft[i] = instantSlider;
        }
        for (int i = 0; i < 16; i++)
        {
            if (i <= 5)
            {
                equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[1];
                equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[1];
            }
            if(i > 5 && i <= 11)
            {
                equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[2];
                equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[2];
            }
            if(i > 11)
            {
                equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[3];
                equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[3];
            }
        }
    }
        void Update()
    {
        for (int i = 0; i < 16; i++)
        {
            if (equalizerRight != null)
            {
                equalizerRight[i].value = (FrequenzCalculator.spectrum[i] * maxscale) + 0.01f;
                equalizerLeft[i].value = (FrequenzCalculator.spectrum[i] * maxscale) + 0.01f;
            }
        }
        if(juiceMeter.value <= 0)
        {
            for (int i = 0; i < 16; i++)
            {
                equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[0];
                equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[0];
            }
        }
        else if (juiceMeter.value > 0 && juiceMeter.value <= 25)
        {
            for (int i = 0; i < 16; i++)
            {
                if (i <= 5)
                {
                    equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[1];
                    equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[1];
                }
                if (i > 5)
                {
                    equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[0];
                    equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[0];
                }
            }
        }
        else if (juiceMeter.value >= 26 && juiceMeter.value <= 50)
        {
            for (int i = 0; i < 16; i++)
            {
                if (i > 5 && i <= 11)
                {
                    equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[2];
                    equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[2];
                }
                if (i > 11)
                {
                    equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[0];
                    equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[0];
                }
            }
        }
        else if (juiceMeter.value >= 51 && juiceMeter.value <= 100)
        {
            for (int i = 0; i < 16; i++)
            {
                if (i > 11 && i <= 16)
                {
                    equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[3];
                    equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[3];
                }
            }
        }

    }
}