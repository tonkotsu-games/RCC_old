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
      //  innerEnemies = Physics.OverlapSphere(player.transform.position, innerRange, layerMask);

        //outerEnemies = Physics.OverlapSphere(player.transform.position, outerRange, layerMask);

        //CheckForEnemiesInRange();
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

        if (outerEnemies.Length >= zoomTriggerAmount)
        {
            Debug.Log("ENEMIES IN RANGE");
            CameraDrive.instance.ChangeCameraState(CameraDrive.CameraStates.ZoomedOut);
        }

        else
        {
            Debug.Log("RANGE CLEAR AGAIN");
            if (CameraDrive.instance.readyToZoomIn)
            {
                CameraDrive.instance.ChangeCameraState(CameraDrive.CameraStates.ZoomedIn);
            }
        }
    }

}
