using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BeathovenFeedback))]
public class Beathoven : Actor
{
    private NavMeshAgent navMeshAgent = null;

    private void Awake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.StateMachine.ChangeState(new Idle(this));
    }
}
