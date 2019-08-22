using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private Camera mainCam;

    [SerializeField] Slider juiceMeter;

    private bool shakeScreen = false;
    private bool endPoint = false;
    private bool bufferPoint = false;

    private float shakeStart;
    private float shakeSpeed;
    private float shakeEndCalculated;
    private float shakeBufferCalculated;

    [SerializeField] float shakeEnd;
    [SerializeField] float shakeBuffer;


    private void Start()
    {
        mainCam = GetComponent<Camera>();
        shakeStart = mainCam.fieldOfView;
        shakeEndCalculated = shakeStart - shakeEnd;
        shakeBufferCalculated = shakeStart + shakeBuffer;
    }

    private void Update()
    {

        if(shakeScreen)
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
                shakeScreen = false;
                endPoint = false;
                bufferPoint = false;
            }
        }
    }

    public void SpeedCalculation()
    {
        if (!shakeScreen)
        {
            shakeSpeed = juiceMeter.value;
            if(shakeSpeed > 0)
            {
                shakeScreen = true;
            }
        }
    }
}
