using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StageRingController : MonoBehaviour
{
    public int ringNumber;

    public static float stepSize;
    public static float moveSpeed;
    public bool isMoving;

    public List<GameObject> agentsOnMesh;

    private Vector3 basePosition;

    private void Start()
    {
        SetBasePos();
    }

    public void Update()
    {
        if (isMoving)
        {
            MoveUp();
        }
        
    }
    public void MoveUp()
    {
        if (transform.position.y <= basePosition.y + stepSize)
        {
            transform.Translate(0,moveSpeed * Time.deltaTime , 0);
        }
        else
        {
            isMoving = false;
        }
    }

    public void SetBasePos()
    {
        basePosition = transform.position;
    }

    public void AddAgentToArray(GameObject agent)
    {
        agentsOnMesh.Add(agent);
    }
    public void DisableAllAgentsOnMesh()
    {
        foreach(GameObject agent in agentsOnMesh)
        {
            agent.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }
    public void EnableAllAgentsOnMesh()
    {
        foreach (GameObject agent in agentsOnMesh)
        {
            agent.GetComponent<NavMeshAgent>().isStopped = false;
        }
    }
}
