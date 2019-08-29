using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttackCheck : MonoBehaviour
{
    private PopupDamageController popupController;
    private GameObject player;

    private AudioSource my_audioSource;

    public AudioClip[] enemyHitSound;

    EnemyHP life;
    [Header("Knockback Range")]
    [Range(0f, 10f)]
    [SerializeField] float knockbackRange;

    public int damage = 2;

    BoxCollider boxCol;

    private void Start()
    {
        my_audioSource = GetComponent<AudioSource>();
        popupController = GameObject.FindWithTag("Player").GetComponent<PopupDamageController>();
        boxCol = GetComponent<BoxCollider>();
        player = GetComponent<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.attack == true && other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<EnemyHP>().death)
        {
            boxCol.enabled = false;
            life = other.gameObject.GetComponent<EnemyHP>();
            life.life -= damage;
            popupController.CreatePopupText(damage.ToString(), other.gameObject.GetComponent<Transform>().transform);

            Vector3 direction = other.transform.position - transform.position;
            direction.y = 0;
            Debug.Log("Direction: " + direction);
            other.gameObject.GetComponent<Rigidbody>().transform.position += direction.normalized * knockbackRange;
    
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