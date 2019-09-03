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
    }

    private void ToggleCamera()
    {
        if(camEnabled)
        {
            mainCamera.SetActive(false);
            mainCamera.SetActive(true);
            camEnabled = false;
        }
        else
        {
            mainCamera.SetActive(true);
            mainCamera.SetActive(false);
            camEnabled = true;
        }
    }

    private void ToggleSlowMotion()
    {

    }
}