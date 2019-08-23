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

    private float currentCameraAngle;

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
        if (agent.hasPath)
        {
            agent.acceleration = (agent.remainingDistance < brakeDistance) ? deceleration : acceleration;
        }
        if(currentTiffState == TiffStates.MoveToNewTarget)
        {
            float dist = agent.remainingDistance;
            if (agent.pathStatus == NavMeshPathStatus.PathComplete && dist == 0 && !agent.pathPending)
            {
                agent.isStopped = true;
                ChangeTiffState(TiffStates.Streaming);
            }
        }

        else if (currentTiffState == TiffStates.Streaming)
        {
            agent.enabled = false;
            gameObject.transform.parent = target.transform;
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
            Debug.Log("already in state " + requestedState);
            return;
        }
        else
        {
            if (currentTiffState == TiffStates.Streaming)
            {
                agent.enabled = true;
                gameObject.transform.parent = null;
            }
            switch (requestedState)
            {
                case TiffStates.FindNewTarget:
                    Debug.Log("Changing from state: " + currentTiffState + " to state: " + requestedState);
                    currentTiffState = TiffStates.FindNewTarget;
                    target = allEnemies[Random.Range(0, allEnemies.Length)];               
                    CalculateNextPos();
                    break;

                case TiffStates.MoveToNewTarget:
                    Debug.Log("Changing from state: " + currentTiffState + "to state: " + requestedState);
                    currentTiffState = TiffStates.MoveToNewTarget;
                    agent.isStopped = false;
                   
                    break;

                case TiffStates.Streaming:
                    Debug.Log("Changing from state: " + currentTiffState + "to state: " + requestedState);
                    currentTiffState = TiffStates.Streaming;
                    StartCoroutine("TargetSwapCooldown");
                    break;

                case TiffStates.AttentionWhore:
                    Debug.Log("Changing from state: " + currentTiffState + "to state: " + requestedState);
                    currentTiffState = TiffStates.AttentionWhore;
                    target = player;
                    CalculateNextPos();
                    StopCoroutine("TargetSwapCooldown");

                    break;
            }
          
        }
    }

     void CalculateNextPos()
    {
        currentCameraAngle = Random.Range(0,360);
        newPos.x = target.transform.position.x + distanceFromPlayer * Mathf.Cos(currentCameraAngle);
        newPos.y = target.transform.position.y;
        newPos.z = target.transform.position.z + distanceFromPlayer * Mathf.Sin(currentCameraAngle);
        newPos = new Vector3(newPos.x, newPos.y, newPos.z);
        agent.SetDestination(newPos);       
        ChangeTiffState(TiffStates.MoveToNewTarget);
    }  

    IEnumerator TargetSwapCooldown()
    {
        yield return new WaitForSeconds(tiffWaitTime);
        ChangeTiffState(TiffStates.FindNewTarget);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(1, 1, 500, 500), "Current TiffTarget: " + target.gameObject.name);
        GUI.Label(new Rect(1,10, 500, 500), "Current TiffState: " + currentTiffState);

    }
}
