using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BeathovenFeedback))]
public class Beathoven : Actor
{
    public NavMeshAgent navMeshAgent = null;

    public BossScriptableObject bossData = null;

    private Transform player;

    private void Awake()
    {
        if(bossData == null)
        {
            Debug.LogError("bossData not set up!");
            return;
        }

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.StateMachine.ChangeState(new Idle(this));
    }

    public void OnDrawGizmosSelected()
    {
        if(bossData!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, bossData.aggroRange);
        }
    }

    public void ChooseBehaviourAfterIdle(SearchResult searchResult)
    {
        var foundPlayers = searchResult.allHitObjectsWithRequiredTag;
        
        Debug.Log("Choosing After Idle");
        if(foundPlayers.Count > 0)
        {
            Debug.Log("not empty " + foundPlayers.Count);
            player = foundPlayers[0].gameObject.transform;
        }
        else Debug.Log("Empty");


        if(foundPlayers.Count > 0)
        {
            //Choose which attack to choose by using info provided
            StateMachine.ChangeState(new Attack(this));
            //StateMachine.ChangeState(new Attack(this));
        }
        else
        {
            StateMachine.ReturnToPreviousState();
        }
    }

    public override void ChooseBehaviour()
    {
        Debug.Log("Choosing to Search");
        StateMachine.ChangeState(new SearchFor(gameObject, bossData.aggroRange, "Player", ChooseBehaviourAfterIdle));
    }
}
