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

            switch (i)
            {
                case 0:
                    //Move only Ring4
                    rings[i].isMoving = true;
                    break;
                case 1:
                    //Move Ring 4 & 3
                    for (int round = 0; i <= 1; round++)
                    {
                        rings[i - round].isMoving = true;
                    }
                    break;
                case 2:
                    //Move Ring 4 & 3 & 2
                    for (int round = 0; i <= 2; round++)
                    {
                        rings[i - round].isMoving = true;
                    }
                    break;
                case 3:
                    //Move Ring 4 & 3 & 2 & 1
                    for (int round = 0; i <= 3; round++)
                    {
                        rings[i - round].isMoving = true;
                    }
                    break;
            }

            i++;
        }
    }

}
