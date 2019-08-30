using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    [SerializeField] PlayerController life;
    [SerializeField] Actor actor;
    [SerializeField] AudioClip playerHitSound;
    [SerializeField] AudioSource enemySoundSource;

    private bool hit = false;

    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hit && actor.attacking)
        {
            enemySoundSource.clip = playerHitSound; 
            enemySoundSource.Play();
            hit = true;
            //Adding to the "Hits taken" stat for the score screen
            //ScoreTracker.instance.statContainer[4]++;
            life.life -= damage;
        }
        else
        {
            return;
        }
    }

    public void EndAttackAnimation()
    {
        hit = false;
    }

    public void EndSpecialAttackAnimation()
    {
        hit = false;
    }
}