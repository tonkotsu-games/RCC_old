using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraStateTrigger : MonoBehaviour
{
    bool changedCam = false;
    [SerializeField] bool usingEnemyDetection = true;
    [SerializeField] int desiredCameraState = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && changedCam == false)
        {
            CameraFollow.usingEnemyRange = usingEnemyDetection;
            CameraFollow.ChangeCameraState(desiredCameraState);
        }
    }
}
