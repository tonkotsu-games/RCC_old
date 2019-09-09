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
    Slider[] equalizerRight = new Slider[128];
    Slider[] equalizerLeft = new Slider[128];

    public float maxscale;

    private float currentJuiceValue = 0;

    void Start()
    {
        for (int i = 0; i < equalizerRight.Length; i++)
        {
            Slider instantSlider = Instantiate(equalizerRightPrefab);
            instantSlider.transform.SetParent(canvasParent.transform, false);
            instantSlider.name = "SampleCube: " + i;
            instantSlider.GetComponent<RectTransform>().localPosition = new Vector3(400,
                                                                                    -175 + (2.7f * i),
                                                                                    0);
            equalizerRight[i] = instantSlider;
            equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[0];
        }
        for (int i = 0; i < equalizerLeft.Length; i++)
        {
            Slider instantSlider = Instantiate(equalizerLeftPrefab);
            instantSlider.transform.SetParent(canvasParent.transform, false);
            instantSlider.name = "SampleCube: " + i;
            instantSlider.GetComponent<RectTransform>().localPosition = new Vector3(-400,
                                                                                    -175 + (2.7f * i),
                                                                                    0);
            equalizerLeft[i] = instantSlider;
            equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[0];
        }
    }
    void FixedUpdate()
    {
        for (int i = 0; i < equalizerLeft.Length; i++)
        {
            if (equalizerRight != null)
            {
                equalizerRight[127 - i].value = (FrequenzCalculator.spectrum[i] * maxscale) + 0.01f;
                equalizerLeft[127 - i].value = (FrequenzCalculator.spectrum[i] * maxscale) + 0.01f;
            }
        }

        if(juiceMeter.value != currentJuiceValue)
        {
            currentJuiceValue = juiceMeter.value;
            ChangeEqualizerColor();
        }
    }

    private void ChangeEqualizerColor()
    {
        for (int i = 0; i < equalizerLeft.Length; i++)
        {
            if (i <= juiceMeter.value * 1.28)
            {
                if (juiceMeter.value * 1.28 < 100)
                {
                    if (i <= 50)
                    {
                        equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[1];
                        equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[1];
                    }
                    else if (i > 50 && i <= 100)
                    {
                        equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[1];
                        equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[1];
                    }
                }
                else if (juiceMeter.value * 1.28 > 100)
                {

                    equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[3];
                    equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[3];
                }
            }
            if (i > juiceMeter.value * 1.28f)
            {
                equalizerRight[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[0];
                equalizerLeft[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = colorEqualizer[0];
            }
        }
    }
}