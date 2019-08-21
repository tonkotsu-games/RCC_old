using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IState
{
    private Actor actor;
    private bool animationFinished;
    private Transform target;
    
    public Attack(Actor actor, Transform target)
    {
        this.actor = actor;
        this.target = target;
    }

    public void Enter()
    {
        Debug.Log("now Attacking");
        animationFinished = false;
        actor.GetComponent<Feedback>().PlayAnimationForState("windUp");
    }

    public IEnumerator PlayAttack()
    {
       //actor.gameObject.GetComponent<Feedback>().NewStateAnimation("attack");
       animationFinished = false;
       actor.attacking = true;
       yield return new WaitForSeconds(actor.GetComponent<Feedback>().attackAnimation.length);
       animationFinished = true;
       actor.attacking = false;
       yield break;
    }

    public void Execute()
    {
        if(!actor.windupFinished)
        {
            actor.FaceTarget();
        }

        if(actor.windupFinished)
        {
            actor.StartCoroutine(PlayAttack());
            actor.windupFinished = false;
            Debug.Log("Start Coroutine in Attack");
        }

        if(animationFinished)
        {
            Debug.Log("Attack Finished");
            //Go back to Idle
            actor.StateMachine.ChangeState(new Idle(actor));
        }
    }

    public void Exit()
    {
        
    }
}