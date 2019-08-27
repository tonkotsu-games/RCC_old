using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRangeAdjust : MonoBehaviour
{
    [SerializeField] GameObject player;
    [Range(0,10)]
    [SerializeField] float innerRange = 5;
    [Range(5,20)]
    [SerializeField] float outerRange = 12;
    [SerializeField] int layerID = 13;

    private int layerMask;
    

    [SerializeField] bool debugInnerSphere, debugOuterSphere;

    [SerializeField] Collider[] innerEnemies;
    [SerializeField] Collider[] outerEnemies;

    [SerializeField] int zoomTriggerAmount = 3; // enemies Needed to zoom out

    public bool enoughEnemiesInRange = false;


    private void Start()
    {
         layerMask = 1 << layerID;
    }

    private void Update()
    {
       innerEnemies = Physics.OverlapSphere(player.transform.position, innerRange, layerMask);

       outerEnemies = Physics.OverlapSphere(player.transform.position, outerRange, layerMask);

       CheckForEnemiesInRange();
    }

    /// <summary>
    /// Sphere Debug
    /// </summary>
    private void OnDrawGizmos()
    {
        if (debugInnerSphere)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(player.transform.position, innerRange);
        }

        if (debugOuterSphere)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(player.transform.position, outerRange);
        }
    }


    /// <summary>
    /// Comparing inner cirlce vs outer circle
    /// </summary>
    /// <returns></returns>
    public void CheckForEnemiesInRange()
    {
        CameraDrive camDrive = CameraDrive.instance;

        if(outerEnemies.Length == 0)
        {
            if (camDrive.currentState != CameraDrive.CameraStates.ZoomMin)
            {
                Debug.Log("No Enemies in Range");
                camDrive.ChangeCameraState(CameraDrive.CameraStates.ZoomMin);
            }
        }
    
        else if (outerEnemies.Length == 1)
        {
            if (camDrive.currentState != CameraDrive.CameraStates.ZoomTwo)
            {
                Debug.Log("One Enemy in Range");
                camDrive.ChangeCameraState(CameraDrive.CameraStates.ZoomTwo);
            }
        }

        else if(outerEnemies.Length == 2)
        {
            if (camDrive.currentState != CameraDrive.CameraStates.ZoomThree)
            {
                Debug.Log("Two Enemies in Range");
                camDrive.ChangeCameraState(CameraDrive.CameraStates.ZoomThree);
            }
        }
        else if (outerEnemies.Length == 3)
        {
            if (camDrive.currentState != CameraDrive.CameraStates.ZoomFour)
            {
                Debug.Log("Three Enemies in Range");
                camDrive.ChangeCameraState(CameraDrive.CameraStates.ZoomFour);
            }
        }
        else if (outerEnemies.Length == 4)
        {
            if (camDrive.currentState != CameraDrive.CameraStates.ZoomFour)
            {
                Debug.Log("Four Enemies in Range");
                camDrive.ChangeCameraState(CameraDrive.CameraStates.ZoomFour);
            }
        }

        else if (outerEnemies.Length >= 5)
        {
            if (camDrive.currentState != CameraDrive.CameraStates.ZoomMax)
            {
                Debug.Log("Five or more Enemies in range");
                camDrive.ChangeCameraState(CameraDrive.CameraStates.ZoomMax);
            }
        }
    }
}
