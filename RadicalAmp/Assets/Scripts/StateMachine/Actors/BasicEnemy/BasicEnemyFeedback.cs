using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyFeedback : Feedback
{
    [SerializeField]
    private AttackCheck attackCheck;

    public override void PlayAnimationForState(string state)
    {
       //NOT REUSABLE --> Array?
        switch(state)
        {
            case "WalkTo":
            PlayWalk();
            break;
            case "Idle":
            PlayIdle();
            break;
            case "windUp":
            PlayWindup();
            break;
            case "Attack":
            PlayAttack();
            break;
            case "specialAttack":
            PlayAttack();
            break;
            case null:
            Debug.Log("no State");
            return;
        }
    }

    private void PlayWalk()
    {
        animator.SetBool("cruising", true);
    }

    private void PlayIdle()
    {
        animator.SetBool("cruising", false);
    }

    private void PlayWindup()
    {
        animator.SetBool("windUp", true);
        animator.SetBool("windUpAttack", false);
        animator.SetBool("cruising", false);
        attackCheck.EndAttackAnimation();
    }

    private void PlayAttack()
    {
        animator.SetBool("windUpAttack", true);
        animator.SetBool("windUp", false);
    }
}
