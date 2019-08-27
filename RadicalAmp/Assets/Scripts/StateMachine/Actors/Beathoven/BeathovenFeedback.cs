using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeathovenFeedback : Feedback
{
    [SerializeField]
    private AttackCheck attackCheck;

    public AnimationClip waveAttackAnimation;
    public AnimationClip waveAttackExitAnimation;

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
            case "waveWindUp":
            PlayWaveWindUp();
            break;
            case "waveAttack":
            PlayWaveAttack();
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
        animator.SetBool("waveAttack", false);
        animator.SetBool("cruising", false);
        attackCheck.EndAttackAnimation();
    }

    private void PlayAttack()
    {
        animator.SetBool("windUpAttack", true);
        animator.SetBool("windUp", false);
    }

    public void PlayWaveWindUp()
    {
        Debug.Log("PlayWaveWindUp");
        animator.SetBool("waveWindUp", true);
        animator.SetBool("windUpAttack", false);
        animator.SetBool("waveAttack", false);
        animator.SetBool("cruising", false);
        attackCheck.EndSpecialAttackAnimation();
    }

    private void PlayWaveAttack()
    {
        animator.SetBool("waveAttack", true);
        animator.SetBool("waveWindUp", false);
    }
}
