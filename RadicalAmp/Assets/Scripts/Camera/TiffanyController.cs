using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TiffanyController : MonoBehaviour
{
    public static TiffanyController instance;
    private NavMeshAgent agent;
    private GameObject target;

    private GameObject[] allEnemies;
    [SerializeField] GameObject player;
    [SerializeField] GameObject tiffCam;
    [Header("Offset Properties")]
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    [SerializeField] float heightOffset;

    [Header("FollowTarget Properties")]
    [SerializeField] float smoothSpeed;
    [SerializeField] float tiffWaitTime = 5;
    [SerializeField] float maxRadius = 20;
    [SerializeField] bool debugRange = false;
    [SerializeField] float acceleration = 20;
    [SerializeField] float deceleration = 60;
    [SerializeField] float brakeDistance = 2;

    int layerMask = 1 << 13;

    private Vector3 offset;


    public enum TiffStates { Streaming,MoveToNewTarget, FindNewTarget, AttentionWhore }

    [HideInInspector]
    public TiffStates currentTiffState;

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
        offset = new Vector3(xOffset, yOffset, 0);
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
            if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete)
            {

                agent.isStopped = true;
                ChangeTiffState(TiffStates.Streaming);
            }
        }

        else if (currentTiffState == TiffStates.Streaming)
        {
           // agent.Warp(target.transform.position + offset);
        }

        
        tiffCam.transform.LookAt(target.transform.position);

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
                    //allEnemies = Physics.OverlapSphere(transform.position, maxRadius, layerMask);
                    target = allEnemies[Random.Range(0, allEnemies.Length)];
                    
                    Debug.Log("New Target found: " + target.name);
                    
                    ChangeTiffState(TiffStates.MoveToNewTarget);
                    break;
                case TiffStates.MoveToNewTarget:
                    
                    currentTiffState = TiffStates.MoveToNewTarget;
                    agent.isStopped = false;
                    agent.destination = target.transform.position;
                   

                    break;
                case TiffStates.Streaming:
                    currentTiffState = TiffStates.Streaming;
                    Debug.Log("Starting Timer ");
                    StartCoroutine("TiffChangeCooldown");
                    break;

                case TiffStates.AttentionWhore:
                    currentTiffState = TiffStates.AttentionWhore;
                    target = player;
                    StopCoroutine("TiffChangeCooldown");
                    Debug.Log("ATTENTIONWHORE!!!");
                    ChangeTiffState(TiffStates.MoveToNewTarget);

                    break;
            }
          
        }
    }

   // public void FollowTarget()
   // {
   //     Vector3 desiredPosition = target.transform.position;
   //     Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
   //     agent.SetDestination(smoothedPosition);
   //     Debug.Log("FollowDone");
   // }

    IEnumerator TiffChangeCooldown()
    {
        yield return new WaitForSeconds(tiffWaitTime);
        ChangeTiffState(TiffStates.FindNewTarget);
    }

    private void OnDrawGizmos()
    {
        if (debugRange)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(player.transform.position, maxRadius);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(1, 1, 500, 500), "Current TiffTarget: " + target.name);
        GUI.Label(new Rect(1,10, 500, 500), "Current TiffState: " + currentTiffState);

    }
}
