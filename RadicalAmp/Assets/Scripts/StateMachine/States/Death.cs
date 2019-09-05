using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Death : IState
{
    Actor actor;
    bool animationFinished;
    private NavMeshAgent navMeshAgent;

    public Death(Actor actor, NavMeshAgent navMeshAgent)
    {
        this.actor = actor;
        this.navMeshAgent = navMeshAgent;
    }

    public void Enter()
    {
        //Debug.Log(actor.gameObject.name + " Died");
        actor.StateMachine.TogglePause();
        navMeshAgent.SetDestination(actor.transform.position);
    }

    public void Execute()
    {

    }
    public void Exit()
    {

    }
}
