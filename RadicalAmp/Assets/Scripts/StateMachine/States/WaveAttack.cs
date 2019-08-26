using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack : IState
{
    private Actor actor;
    private bool animationFinished;
    private Transform target;
    private bool hasToCheck;
    
    public WaveAttack(Actor actor, Transform target)
    {
        this.actor = actor;
        this.target = target;
    }

    public void Enter()
    {
        Debug.Log("now in Wave Attack");
        animationFinished = false;
        hasToCheck = true;
    }

    public IEnumerator PlaySpecialAttack()
    {
        Debug.Log("in Coroutine");
       //actor.gameObject.GetComponent<Feedback>().NewStateAnimation("specialAttack");
       animationFinished = false;
       actor.attacking = true;
       yield return new WaitForSeconds(actor.GetComponent<Feedback>().specialAttackAnimation.length);
       animationFinished = true;
       actor.attacking = false;
       yield break;
    }

    public void Execute()
    {
        if(hasToCheck)
        {
            Debug.Log("CheckingBeat");
            if(actor.CheckBeat(this))
            {
                Debug.Log("HitBeat");
                actor.GetComponent<Feedback>().PlayAnimationForState("specialWindup");
                Debug.Log("SetSpecialWindup");
                hasToCheck = false;
            }
        }

        if(!actor.windupFinished)
        {
            actor.FaceTarget();
        }

        if(actor.windupFinished)
        {
            actor.StartCoroutine(PlaySpecialAttack());
            actor.windupFinished = false;
            Debug.Log("Start Coroutine in SpecialAttack");
        }

        if(animationFinished)
        {
            Debug.Log("Special Attack Finished");
            //Go back to Idle
            actor.StateMachine.ChangeState(new Idle(actor));
        }
    }

    public void Exit()
    {
        
    }
}
