using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AttackCheck : MonoBehaviour
{
    [SerializeField] PlayerController life;
    [SerializeField] Actor actor;
    [SerializeField] AudioClip playerHitSound;
    [SerializeField] AudioSource enemySoundSource;

    [MinMaxSlider(-3f, 3f)]
    public Vector2 soundPitchRange = new Vector2(1f,1f);

    private bool hit = false;

    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hit && actor.attacking)
        {
            enemySoundSource.clip = playerHitSound;
            enemySoundSource.pitch = Random.Range(soundPitchRange.x, soundPitchRange.y);
            enemySoundSource.Play();
            hit = true;
            life.PlayerBloodSplat();
            if (ScoreTracker.instance != null && life.life > 0)
            {
                ScoreTracker.instance.hitsTaken ++;
            }
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