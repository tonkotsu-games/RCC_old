using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Tooltip("The amount of units a ring moves down")]
    public float ringStepSize = 5;
    public float ringMoveSpeed = 1;
    public StageRingController[] rings;


    void Start()
    {
        rings = GetComponentsInChildren<StageRingController>();
        StageRingController.stepSize = ringStepSize;
        StageRingController.moveSpeed = ringMoveSpeed;
        Debug.Log("StartingTimer");
        StartCoroutine("StageChangeTest");
    }

    IEnumerator StageChangeTest()
    {
        int i = 0;
        while (i <= 4)
        {
            Debug.Log("Round: " + i + " -> starting countdown");
            yield return new WaitForSeconds(10);
            Debug.Log("setting " + rings[i].gameObject.name + "'s isMoving to true");

            switch (i)
            {
                case 1:
                    //Move only Ring4
                    break;
                case 2:
                    //Move Ring 4 & 3
                    break;
                case 3:
                    //Move Ring 4 & 3 & 2
                    break;
                case 4:
                    //Move all rings
                    break;
            }

            rings[i].isMoving = true;
            i++;
        }
    }

}
