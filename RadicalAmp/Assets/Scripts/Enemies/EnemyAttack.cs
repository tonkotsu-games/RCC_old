using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerController life;
    [SerializeField] EnemyController attack;

    private bool hit = false;

    public int damage = 1;

    [SerializeField] AudioClip playerHitSound;
    [SerializeField] AudioSource enemySoundSource; 

    public void Start()
    {
        life = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + hit);
        Debug.Log("Gameobject: " + other.gameObject.tag);
        Debug.Log("Attack: " + attack.attack);
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