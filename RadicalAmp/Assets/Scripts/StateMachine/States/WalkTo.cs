using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkTo : IState
{
    private Actor actor;
    private Transform target;
    private NavMeshAgent navMeshAgent;
    private float distanceToTarget;
    private float searchRange;
    private float interactRange;

    public WalkTo(Actor actor, Transform target, NavMeshAgent navMeshAgent, float searchRange, float interactRange)
    {
        this.actor = actor;
        this.target = target;
        this.navMeshAgent = navMeshAgent;
        this.searchRange = searchRange;
        this.interactRange = interactRange;
    }

    public void Enter()
    {
        //Debug.Log("Now in WalkTo");
        actor.GetComponent<Feedback>().PlayAnimationForState("WalkTo");
    }

    public void Execute()
    {
        distanceToTarget = Vector3.Distance(target.position, actor.gameObject.transform.position);
        if(distanceToTarget > searchRange)
        {
            //Debug.Log("Out of Range");
            actor.StateMachine.ChangeState(new Idle(actor));
        }
        else if(distanceToTarget < interactRange)
        {
            actor.ChooseBehaviour();
        }
        else
        {
            navMeshAgent.SetDestination(target.position);
        }
        
    }

    public void Exit()
    {
        //Debug.Log("Exiting WalkTo");
        navMeshAgent.SetDestination(actor.gameObject.transform.position);
    }
}
