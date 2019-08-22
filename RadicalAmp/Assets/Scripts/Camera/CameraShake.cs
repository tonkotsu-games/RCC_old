using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private Camera mainCam;

    [SerializeField] Slider juiceMeter;
    [SerializeField] BeatAnalyse beat;

    private bool screenShake = false;
    private bool endPoint = false;
    private bool bufferPoint = false;



    private float shakeStart;
    private float shakeEndCalculated;
    private float shakeBufferCalculated;
    private float shakeSpeed;


    private void Start()
    {
        mainCam = GetComponent<Camera>();
        shakeStart = mainCam.fieldOfView;
    }

    private void Update()
    {
        if (!screenShake && beat.IsOnBeat(0))
        {
            SpeedCalculation();
        }

        if (screenShake)
        {
            ScreenShake();
        }
    }

    public void ScreenShake()
    {
        if(!endPoint && !bufferPoint)
        {
            mainCam.fieldOfView -= shakeSpeed * Time.deltaTime;
            if(mainCam.fieldOfView <= shakeEndCalculated)
            {
                endPoint = true;
            }
        }
        else if(endPoint && !bufferPoint)
        {
            mainCam.fieldOfView += shakeSpeed * Time.deltaTime;
            if(mainCam.fieldOfView >= shakeBufferCalculated)
            {
                bufferPoint = true;
            }
        }
        else if(endPoint && bufferPoint)
        {
            mainCam.fieldOfView -= shakeSpeed * Time.deltaTime;
            if(mainCam.fieldOfView <= shakeStart)
            {
                mainCam.fieldOfView = shakeStart;
                endPoint = false;
                bufferPoint = false;
                screenShake = false;
            }
        }
    }

    public void SpeedCalculation()
    {
        if (!screenShake)
        {
            shakeEndCalculated = shakeStart + (-0.00005f * Mathf.Pow(juiceMeter.value, 2f));
            shakeBufferCalculated = shakeStart - (0.000025f * Mathf.Pow(juiceMeter.value, 2f));
            shakeSpeed = (0.00175f * Mathf.Pow(juiceMeter.value, 2f));
            screenShake = true;
        }
    }
}
