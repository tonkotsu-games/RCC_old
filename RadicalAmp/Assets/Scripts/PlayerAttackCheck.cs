using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCheck : MonoBehaviour
{
    private PopupDamageController popupController;

    [SerializeField] private GameObject particle;

    AudioSource my_audioSource;
    public AudioClip[] enemyHitSound;

    EnemyHP live;

    public int damage = 2;

    private void Start()
    {
        my_audioSource = GetComponent<AudioSource>();

        popupController = GameObject.FindWithTag("Player").GetComponent<PopupDamageController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.attack == true && other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<EnemyHP>().death)
        {
            live = other.gameObject.GetComponent<EnemyHP>();
            live.life -= damage;
            popupController.CreatePopupText(damage.ToString(), other.gameObject.GetComponent<Transform>().transform);

            my_audioSource.clip = enemyHitSound[Random.Range(0, enemyHitSound.Length)];
            my_audioSource.Play();

            live.BloodSplat();

            if (BeatStrike.beatAttack && !other.gameObject.GetComponent<EnemyHP>().death)
            {
                GameObject particalEffect = Instantiate(particle, this.transform.position, Quaternion.identity);
            }
        }
        else
        {
            return;
        }
    }    
}