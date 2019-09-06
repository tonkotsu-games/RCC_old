using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

    public int damage;
    public int baseDamage;
    [HideInInspector]
    public int damageOnBeat;
    Slider juiceMeter;

    BoxCollider boxCol;

    private void Start()
    {
        my_audioSource = GetComponent<AudioSource>();
        popupController = GameObject.FindWithTag("Player").GetComponent<PopupDamageController>();
        boxCol = GetComponent<BoxCollider>();
        player = GetComponent<GameObject>();
        juiceMeter = Locator.instance.GetJuiceMeter();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.attack == true && 
            (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") && 
            !other.gameObject.GetComponent<EnemyHP>().death)
        {
            boxCol.enabled = false;
            life = other.gameObject.GetComponent<EnemyHP>();
            if (BeatStrike.beatAttack)
            {
                damageOnBeat = baseDamage + Mathf.RoundToInt(juiceMeter.value) * 10;
                damage = damageOnBeat;               
            }
            else
            {
                baseDamage = Random.Range(10, 100);
                damage = baseDamage;
            }
            BeatStrike.beatAttack = false;
            life.life -= damage;
            popupController.CreatePopupText(damage, other.gameObject.GetComponent<Transform>().transform);
            
            my_audioSource.clip = enemyHitSound[Random.Range(0, enemyHitSound.Length)];
            my_audioSource.Play();
    
            life.BloodSplat();

            if (other.gameObject.tag == "Enemy")
            {
                Vector3 direction = other.transform.position - transform.position;
                direction.y = 0;
                other.gameObject.GetComponent<Rigidbody>().transform.position += direction.normalized * knockbackRange;
            }
        }
        else
        {
            return;
        }
    }
}