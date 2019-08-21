using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IState
{
    private Actor actor;
    private bool animationFinished;
    public Attack(Actor actor)
    {
        this.actor = actor;
    }

    public void Enter()
    {
        Debug.Log("now Attacking");
        animationFinished = false;
        actor.StartCoroutine(PlayAttack());
    }

    public IEnumerator PlayAttack()
    {
       //actor.gameObject.GetComponent<Feedback>().NewStateAnimation("attack");
       animationFinished = false;
       yield return new WaitForSeconds(5);
       animationFinished = true;
       yield break;
    }

    public void Execute()
    {
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