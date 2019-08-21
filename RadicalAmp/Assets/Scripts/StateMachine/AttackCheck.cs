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
        Debug.Log("Hit: " + hit);
        Debug.Log("Gameobject: " + other.gameObject.tag);
        //Debug.Log("Attack: " + attack.attack);
        if (other.gameObject.tag == "Player" && !hit && actor.attacking)
        {
            Debug.Log("Hit");
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