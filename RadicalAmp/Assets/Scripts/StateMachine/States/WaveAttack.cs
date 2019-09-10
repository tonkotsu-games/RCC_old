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
        animationFinished = false;
        hasToCheck = true;
    }

    public IEnumerator PlaySpecialAttack()
    {
        animationFinished = false;
        yield return new WaitForSeconds(actor.GetComponent<BeathovenFeedback>().waveAttackAnimation.length + actor.GetComponent<BeathovenFeedback>().waveAttackExitAnimation.length);
        animationFinished = true;
        actor.attacking = false;
        yield break;
    }

    public void Execute()
    {
        if(hasToCheck)
        {
            if(actor.CheckBeat(this))
            {
                actor.GetComponent<Feedback>().PlayAnimationForState("waveWindUp");
                hasToCheck = false;
            }
        }

        if(actor.windupFinished)
        {
            actor.StartCoroutine(PlaySpecialAttack());
            actor.windupFinished = false;
        }

        if(animationFinished)
        {
            //Go back to Idle
            actor.StateMachine.ChangeState(new Idle(actor));
        }
    }

    public void Exit()
    {
        
    }
}
