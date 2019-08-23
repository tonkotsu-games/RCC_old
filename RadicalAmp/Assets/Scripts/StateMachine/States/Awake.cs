using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awake : IState
{
    Actor actor;
    bool animationFinished;

    public Awake(Actor actor)
    {
        this.actor = actor;
    }

    public void Enter()
    {
        //Debug.Log(actor.gameObject.name + " Awakening");
        actor.StartCoroutine(PlayAwake());
    }

    public IEnumerator PlayAwake()
    {
       actor.gameObject.GetComponent<Feedback>().PlayAwake();
       animationFinished = false;
       yield return new WaitForSeconds(1);
       animationFinished = true;
       yield break;
    }

    public void Execute()
    {
        if(animationFinished)
        {
            //Go into Idle
            actor.StateMachine.ChangeState(new Idle(actor));
        }
    }
    public void Exit()
    {

    }
}