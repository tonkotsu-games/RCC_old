using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] PlayerController life;
    [SerializeField] EnemyController attack;
    [SerializeField] AudioClip playerHitSound;
    [SerializeField] AudioSource enemySoundSource;

    private bool hit = false;

    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hit && attack.attack)
        {
            enemySoundSource.clip = playerHitSound; 
            enemySoundSource.Play();
            hit = true;
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
}