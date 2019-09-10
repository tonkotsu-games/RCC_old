using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequenzCalculator : MonoBehaviour
{
    public static float[] spectrum = new float[4096];
    public static float[] frequBand = new float[11];
    public static float[] bandBuffer = new float[11];
    float[] bufferDecrease = new float[11];
    float[] frequBandHigh = new float[11];
    public static float[] audioband = new float[11];
    public static float[] audioBuffer = new float[11];
    public static float amblitude, amplitudebuffer;
    float amplitudehigh;

    public static bool beat;

    AudioSource audiosource;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        GetSpectrumData();
        FrequenzBand();
        //Bandbuffer();
        //AudioBand();
    }

    void GetSpectrumData()
    {
        audiosource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }

    void AudioBand()
    {
        for (int i = 0; i < 11; i++)
        {
            if (frequBand[i] > frequBandHigh[i])
            {
                frequBandHigh[i] = frequBand[i];
            }
            audioband[i] = frequBand[i] / frequBandHigh[i];
            audioBuffer[i] = bandBuffer[i] / frequBandHigh[i];

        }
    }

    void Bandbuffer()
    {
        for (int i = 0; i < 3; i++)
        {
            if (frequBand[i] > bandBuffer[i])
            {
                bandBuffer[i] = frequBand[i];
                bufferDecrease[i] = 0.005f;
            }
            if (frequBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }

        }
    }

    void FrequenzBand()
    {
        /*
         * 0 - 2
         * 1 - 4
         * 2 - 8
         * 3 - 16
         * 4 - 32
         * 5 - 64
         * 6 - 128
         * 7 - 256
         * 8 - 512
         * 9 - 1024
         * 10 - 2048
         * 11 - 4096
         */
        int count = 0;

        for (int i = 0; i < 11; i++)
        {
            float average = 0;
            int sammpleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 10)
            {
                sammpleCount += 2;
            }
            for (int j = 0; j < sammpleCount; j++)
            {
                average += spectrum[count] * (count + 1);
                count++;
            }
            average /= count;
            frequBand[i] = average * 2;
        }
    }
}