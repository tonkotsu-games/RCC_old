using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class PlayerAttackCheck : MonoBehaviour
{
    private PopupDamageController popupController;
    private GameObject player;

    private AudioSource my_audioSource;

    public AudioClip[] enemyHitSound;

    [MinMaxSlider(-3f, 3f)]
    public Vector2 soundPitchRange = new Vector2(1f, 1f);

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
        if (PlayerController.attack == true && 
            (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") && 
            !other.gameObject.GetComponent<EnemyHP>().death)
        {
            boxCol.enabled = false;
            life = other.gameObject.GetComponent<EnemyHP>();
            life.life -= damage;
            popupController.CreatePopupText(damage.ToString(), other.gameObject.GetComponent<Transform>().transform);

            my_audioSource.pitch = Random.Range(soundPitchRange.x, soundPitchRange.y);
            my_audioSource.clip = enemyHitSound[Random.Range(0, enemyHitSound.Length)];
            my_audioSource.Play();
    
            life.BloodSplat();

            if (other.gameObject.tag == "Enemy")
            {
                Vector3 direction = other.transform.position - transform.position;
                direction.y = 0;
                other.gameObject.GetComponent<Transform>().transform.position += direction.normalized * knockbackRange;
            }
        }
        else
        {
            return;
        }
    }
}