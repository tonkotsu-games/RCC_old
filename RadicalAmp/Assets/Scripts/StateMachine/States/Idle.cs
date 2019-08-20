using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{
    [SerializeField]
    float idleDuration = 0;

    float timerStart = 10;
    float timerCurrent = 0;
    Actor actor;
    Timer idleTimer = new Timer();
    

    public Idle(Actor actor)
    {
        this.actor = actor;
    }

    public void Enter()
    {
        Debug.Log("Now in Idle");
        idleDuration = idleDuration + Random.Range(0,1);
        Debug.Log("Timer: " + idleDuration);
        idleTimer.Start(idleDuration);
    }

    public void Execute()
    {
        idleTimer.Tick();
        
        if(idleTimer.timeCurrent <= 0)
        {
            ChooseBehaviour();
        }
    }

    private void ChooseBehaviour()
    {
        Debug.Log("Choosing");
        //actor.StateMachine.ChangeState(new State1_SearchFor(actor.playerLayer, actor.gameObject, actor.viewRange, actor.playerTag, actor.ChooseBehaviourAfterIdle));
    }

    public void Exit()
    {

    }
}
