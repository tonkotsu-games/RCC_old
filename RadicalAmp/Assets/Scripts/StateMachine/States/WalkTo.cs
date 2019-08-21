using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkTo : IState
{
    Actor actor;
    Transform target;
    NavMeshAgent navMeshAgent;

    public WalkTo(Actor actor, Transform target, NavMeshAgent navMeshAgent)
    {
        this.actor = actor;
        this.target = target;
        this.navMeshAgent = navMeshAgent;
    }

    public void Enter()
    {

    }

    public void Execute()
    {
        //Check
        navMeshAgent.SetDestination(target.position);
    }

    public void Exit()
    {

    }
}
