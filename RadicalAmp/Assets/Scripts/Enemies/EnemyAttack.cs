using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerController life;

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
        if (other.gameObject.tag == "Player" && !hit && EnemyController.attack)
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