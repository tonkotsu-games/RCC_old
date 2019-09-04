using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [Tooltip("The amount of units a ring moves down")]
    public float ringStepSize = 5;
    public float ringMoveSpeed = 1;
    public StageRingController[] rings;

    [SerializeField]
    List<GameObject> activeEnemies;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }

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
                    //Move Ring 3&2&1
                    rings[1].gameObject.GetComponent<NavMeshSurface>().enabled = true;
                    rings[0].gameObject.GetComponent<NavMeshSurface>().enabled = false;
                    rings[1].isMoving = true;
                    break;
                case 1:
                    //Move Ring 2&1
                    rings[2].gameObject.GetComponent<NavMeshSurface>().enabled = true;
                    rings[1].gameObject.GetComponent<NavMeshSurface>().enabled = false;

                    rings[2].SetBasePos();
                    rings[2].isMoving = true;
                    break;
                case 2:
                    //Move Ring 1
                    rings[3].gameObject.GetComponent<NavMeshSurface>().enabled = true;
                    rings[2].gameObject.GetComponent<NavMeshSurface>().enabled = false;

                    rings[3].SetBasePos();
                    rings[3].isMoving = true;
                    break;
                case 3:       
                    //Move Boss down
                    break;
            }

            i++;
        }
    }


    public void AddToEnemyCount(GameObject enemy)
    {
        Debug.Log("Adding to EnemyList");
        activeEnemies.Add(enemy);
    }
    public void DeleteFromEnemyCount(GameObject enemy)
    {
        foreach(GameObject e in activeEnemies)
        {
            if(e == enemy)
            {
                Debug.Log("RemovedEnemyFromList");
                activeEnemies.Remove(e);
            }
        }
    }
}
