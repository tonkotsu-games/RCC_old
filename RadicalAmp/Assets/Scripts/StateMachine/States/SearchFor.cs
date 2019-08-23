using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchFor : IState
{
    private GameObject actorGameObject;
    private float searchRadius;
    private string tagToLookFor;
    public bool searchCompleted;
    private System.Action<SearchResult> searchResultCallback;

    public SearchFor(GameObject actorGameObject, float searchRadius, string tagToLookFor, System.Action<SearchResult> searchResultCallback)
    {
        this.actorGameObject = actorGameObject;
        this.searchRadius = searchRadius;
        this.tagToLookFor = tagToLookFor;
        this.searchResultCallback = searchResultCallback;
    }

    public void Enter()
    {
        //Debug.Log("Now in Searching");
    }

    public void Execute()
    {
        if(searchCompleted)
        {
            return;
        }

        //Debug.Log("Executing SearchFor");
        var hitObjects = Physics.OverlapSphere(this.actorGameObject.transform.position, searchRadius);
        var allHitObjectsWithRequiredTag = new List<Collider>();

        for (int i = 0; i < hitObjects.Length; i++)
        {
            //Debug.Log("Searching " + i);

            if(hitObjects[i].CompareTag(this.tagToLookFor))
            {
                //this.navMeshAgent.SetDestination(HitObjects[i].transform.position);
                //Debug.Log("Found Object");
                allHitObjectsWithRequiredTag.Add(hitObjects[i]);
            }      
        }

        var searchResult = new SearchResult(hitObjects, allHitObjectsWithRequiredTag);
        searchCompleted = true;
        this.searchResultCallback(searchResult);
    }

    public void Exit()
    {

    }
}

public class SearchResult
{
    public Collider[] allHitObjectsInSearchRadius;
    public List<Collider> allHitObjectsWithRequiredTag;

    //more info like closest or furthest object etc.

    public SearchResult(Collider[] allHitObjectsInSearchRadius, List<Collider> allHitObjectsWithRequiredTag)
    {
        this.allHitObjectsInSearchRadius = allHitObjectsInSearchRadius;
        this.allHitObjectsWithRequiredTag = allHitObjectsWithRequiredTag;
    }
}