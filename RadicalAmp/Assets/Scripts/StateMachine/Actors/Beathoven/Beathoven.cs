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
    }
    void Start()
    {
        this.StateMachine.ChangeState(new Idle(this));
    }

    private void Update()
    {
        StateMachine.StateExecuteTick();

        if(StateMachine.StateCurrent is Idle && player != null)
        {
            FaceTarget();
        }
    }

    public void OnDrawGizmosSelected()
    {
        if(bossData!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, bossData.aggroRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, bossData.meleeAttackRange);
        }
    }

    public void ChooseBehaviourAfterIdle(SearchResult searchResult)
    {
        var foundPlayers = searchResult.allHitObjectsWithRequiredTag;
        
        //Search Results
        Debug.Log("Choosing After Idle");
        if(foundPlayers.Count > 0)
        {
            Debug.Log("not empty " + foundPlayers.Count);
            player = foundPlayers[0].gameObject.transform;
            Debug.Log(player.gameObject.name);
        }
        else Debug.Log("Empty");


        //Choose what to do by using info provided by the Search
        if(foundPlayers.Count > 0)
        {
            Debug.Log("player " + player.position + " beathoven " + this.gameObject.transform.position );
            //Attack Player in Melee AttackRange
            if (Vector3.Distance(player.position, this.gameObject.transform.position) < bossData.meleeAttackRange)
            {
                StateMachine.ChangeState(new Attack(this, player));
            }
            //Walk to Player in AggroRange
            else if(Vector3.Distance(player.position, this.gameObject.transform.position) < bossData.aggroRange)
            {
                StateMachine.ChangeState(new WalkTo(this, player, navMeshAgent, bossData.aggroRange, ActorData.meleeAttackRange));
            }
        }
        else
        {
            StateMachine.ReturnToPreviousState();
        }
    }

    public override void ChooseBehaviour()
    {
        if(StateMachine.StateCurrent is Idle)
        {
            Debug.Log("Choosing to Search");
            StateMachine.ChangeState(new SearchFor(gameObject, bossData.aggroRange, "Player", ChooseBehaviourAfterIdle));
        }
        else if(StateMachine.StateCurrent is WalkTo)
        {
            StateMachine.ChangeState(new Attack(this, player));
        }
    }

    public override void FaceTarget()
    {
        Vector3 direction = (player.position - this.gameObject.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        this.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2.5f);
    }
}
