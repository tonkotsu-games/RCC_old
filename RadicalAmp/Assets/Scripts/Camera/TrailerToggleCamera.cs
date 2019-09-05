using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerToggleCamera : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject trailerCamera;
    [SerializeField] GameObject uiObject;
    [SerializeField] float slowMotionAmount = 0.2f;
    private bool camEnabled = true;
    private bool uiEnabled = true;
    private bool inSlowMotion = false;
    int slowMotionState = 0;


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
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleUI();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            slowMotionState++;

            if(slowMotionState > 2)
            {
                slowMotionState = 0;
            }

            Debug.Log(slowMotionState);

            ToggleSlowMotion();
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
            if(slowMotionState == 0)
            {
                slowMotionAmount = 0.01f;
            }
            else if (slowMotionState == 1)
            {
                slowMotionAmount = 0.1f;
            }
            else if (slowMotionState == 2)
            {
                slowMotionAmount = 0.5f;
            }
            else
            {
                Debug.Log("State Problem");
            }
            Time.timeScale = slowMotionAmount;
            inSlowMotion = false;
        }
        else
        {
            Time.timeScale = 1f;
            inSlowMotion = true;
        }
    }

    private void ToggleUI()
    {
        if(uiEnabled == true)
        {
            uiObject.SetActive(false);
            uiEnabled = false;
        }
        else
        {
            uiObject.SetActive(true);
            uiEnabled = true;
        }
        
    }
}