using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerToggleCamera : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject trailerCamera;
    [SerializeField] float slowMotionAmount = 0.2f;
    private bool camEnabled = true;
    private bool inSlowMotion = false;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            ToggleCamera();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleSlowMotion();
        }
    }

    private void ToggleCamera()
    {
        if(camEnabled)
        {
            mainCamera.SetActive(false);
            trailerCamera.SetActive(true);
            camEnabled = false;
        }
        else
        {
            mainCamera.SetActive(true);
            trailerCamera.SetActive(false);
            camEnabled = true;
        }
    }

    private void ToggleSlowMotion()
    {
        if(inSlowMotion)
        {
            Time.timeScale = slowMotionAmount;
            inSlowMotion = false;
        }
        else
        {
            Time.timeScale = 1f;
            inSlowMotion = true;
        }
    }
}