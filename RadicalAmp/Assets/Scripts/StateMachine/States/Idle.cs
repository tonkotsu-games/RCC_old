using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{
    float idleDuration;
    Actor actor;
    Timer idleTimer = new Timer();
    

    public Idle(Actor actor)
    {
        this.actor = actor;
    }

    public void Enter()
    {
        actor.GetComponent<Feedback>().PlayAnimationForState("Idle");
        idleDuration = actor.ActorData.idleDuration;
        //Debug.Log("Now in Idle");
        //idleDuration += Random.Range(0,1);
        //Debug.Log("Timer: " + idleDuration);
        idleTimer.Start(idleDuration);
    }

    public void Execute()
    {
        idleTimer.Tick();
        
        if(idleTimer.timeCurrent <= 0)
        {
            idleTimer.ResetTimer();
            ChooseBehaviour();
        }
    }

    private void ChooseBehaviour()
    {
        //Debug.Log("Choosing");
        actor.ChooseBehaviour();
    }

    public void Exit()
    {

    }
}
