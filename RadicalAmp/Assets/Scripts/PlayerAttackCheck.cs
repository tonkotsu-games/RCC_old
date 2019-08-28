using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttackCheck : MonoBehaviour
{
    private PopupDamageController popupController;

    AudioSource my_audioSource;
    public AudioClip[] enemyHitSound;

    EnemyHP life;

    public int damage = 2;

    BoxCollider boxCol;

    private void Start()
    {
        my_audioSource = GetComponent<AudioSource>();
        popupController = GameObject.FindWithTag("Player").GetComponent<PopupDamageController>();
        boxCol = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.attack == true && other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<EnemyHP>().death)
        {
            boxCol.enabled = false;
            life = other.gameObject.GetComponent<EnemyHP>();
            life.life -= damage;
            popupController.CreatePopupText(damage.ToString(), other.gameObject.GetComponent<Transform>().transform);
    
            my_audioSource.clip = enemyHitSound[Random.Range(0, enemyHitSound.Length)];
            my_audioSource.Play();
    
            life.BloodSplat();
        }
        else
        {
            return;
        }
    }  
}