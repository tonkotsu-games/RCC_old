using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRangeAdjust : MonoBehaviour
{
    [SerializeField] GameObject player;
    [Range(0,10)]
    [SerializeField] float innerRange = 5;
    [Range(5,20)]
    [SerializeField] float outerRange = 10;
    [SerializeField] int layerID = 13;

    private int layerMask;
    

    [SerializeField] bool debugInnerSphere, debugOuterSphere;

    [SerializeField] Collider[] innerEnemies;
    [SerializeField] Collider[] outerEnemies;

    [SerializeField] int zoomTriggerAmount = 3; // enemies Needed to zoom out


    private void Start()
    {
         layerMask = 1 << layerID;
    }

    public void FixedUpdate()
    {
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
    public bool CheckForEnemiesInRange()
    {
        innerEnemies = Physics.OverlapSphere(player.transform.position, innerRange, layerMask);

        outerEnemies = Physics.OverlapSphere(player.transform.position, outerRange, layerMask);

        if (outerEnemies.Length - innerEnemies.Length > zoomTriggerAmount)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

}
