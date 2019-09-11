using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{

    // The camera to which to switch to when the toggleCamera() method is called.
    public Camera otherCamera;
    // The main camera.
    Camera mainCamera;
    // A flag indicating wether the other camera is active.
    bool inFocus = false;

    // Use this for initialization
    void Start()
    {
        // Disable the other camera.
        otherCamera.enabled = false;
    }

    // Toggles the active camera.
    public void toggleCamera()
    {

        if (inFocus)
        {
            mainCamera.enabled = true;
            otherCamera.enabled = false;
            inFocus = false;
        }
        else
        {
            mainCamera = Camera.main;
            otherCamera.enabled = true;
            mainCamera.enabled = false;
            inFocus = true;
        }
    }
}
