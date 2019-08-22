using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TiffanyController : MonoBehaviour
{
    public static TiffanyController instance;
    private NavMeshAgent agent;
    private GameObject target;
    Vector3 newPos;

    private GameObject[] allEnemies;
    [SerializeField] GameObject player;
    [SerializeField] GameObject tiffCam;
    [Header("Offset Properties")]
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    [SerializeField] float heightOffset;

    [Header("FollowTarget Properties")]
    [SerializeField] float distanceFromPlayer = 5;
    [SerializeField] float tiffWaitTime = 3;
    [SerializeField] bool debugRange = false;
    [SerializeField] float acceleration = 20;
    [SerializeField] float deceleration = 60;
    [SerializeField] float brakeDistance = 2;

    int layerMask = 1 << 13;

    public enum TiffStates { Streaming,MoveToNewTarget, FindNewTarget, AttentionWhore }

    [HideInInspector]
    public TiffStates currentTiffState = TiffStates.MoveToNewTarget;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();   
        ChangeTiffState(TiffStates.FindNewTarget);
    }

    private void Update()
    {
        if (!agent.hasPath)
        {
            Debug.LogWarning("NO PATH FOUND");
        }
        if (agent.hasPath)
        {
            agent.acceleration = (agent.remainingDistance < brakeDistance) ? deceleration : acceleration;
        }
        if(currentTiffState == TiffStates.MoveToNewTarget)
        {
            float dist = agent.remainingDistance;
            if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && dist == 0)
            {
                Debug.Log(agent.destination);
                Debug.Log("target reached");
                agent.isStopped = true;
                ChangeTiffState(TiffStates.Streaming);
            }
        }

        else if (currentTiffState == TiffStates.Streaming)
        {
          
        }

        Transform lookAtTarget = target.GetComponent<TiffTarget>().tiffTarget;
        tiffCam.transform.LookAt(lookAtTarget);

        if(target == null)
        {
            allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            Debug.Log("Refilling targets");
        }
    }


    public void ChangeTiffState(TiffStates requestedState)
    {
        if(requestedState == currentTiffState)
        {
            return;
        }
        else
        {
            switch (requestedState)
            {
                case TiffStates.FindNewTarget:
                    currentTiffState = TiffStates.FindNewTarget;

                    Debug.Log("Looking for new Target");
                    target = allEnemies[Random.Range(0, allEnemies.Length)];               
                    Debug.Log("New Target found: " + target.name);
                    CalculateNextPos();
                    break;

                case TiffStates.MoveToNewTarget:                
                    currentTiffState = TiffStates.MoveToNewTarget;
                    agent.isStopped = false;
                   
                    break;

                case TiffStates.Streaming:
                    currentTiffState = TiffStates.Streaming;
                    Debug.Log("Starting Timer ");
                    StartCoroutine("TiffChangeCooldown");
                    break;

                case TiffStates.AttentionWhore:
                    currentTiffState = TiffStates.AttentionWhore;
                    target = player;
                    CalculateNextPos();
                    StopCoroutine("TiffChangeCooldown");
                    Debug.Log("ATTENTIONWHORE!!!");

                    break;
            }
          
        }
    }

     void CalculateNextPos()
    {
        float Angle = Random.Range(0,360);
        newPos.x = target.transform.position.x + distanceFromPlayer * Mathf.Cos(Angle);
        newPos.y = target.transform.position.y;
        newPos.z = target.transform.position.z + distanceFromPlayer * Mathf.Sin(Angle);
        newPos = new Vector3(newPos.x, newPos.y, newPos.z);
        agent.destination = newPos;
        ChangeTiffState(TiffStates.MoveToNewTarget);
    }  

    IEnumerator TiffChangeCooldown()
    {
        yield return new WaitForSeconds(tiffWaitTime);
        ChangeTiffState(TiffStates.FindNewTarget);
    }

    private void OnDrawGizmos()
    {
        if (debugRange)
        {
           // Gizmos.color = Color.yellow;
           // Gizmos.DrawSphere(player.transform.position, maxRadius);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(1, 1, 500, 500), "Current TiffTarget: " + target.gameObject.name);
        GUI.Label(new Rect(1,10, 500, 500), "Current TiffState: " + currentTiffState);

    }
}
