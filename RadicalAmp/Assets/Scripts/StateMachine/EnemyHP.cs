﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.VFX;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] VisualEffect blood;
    [SerializeField] ParticleSystem[] bloodSplatter;

    [Header("Enemy Health")]
    public int life;

    private Animator EnemyAnim;
    private Renderer birdDissolve;

    private float NavmeshSpeed;

    private NavMeshAgent EnemyNav;

    public bool death = false;

    public float dissolveValue = -0.2f;

    // Start is called before the first frame update
    void Start()
    {
        EnemyAnim = gameObject.GetComponentInChildren<Animator>();
        EnemyAnim.SetBool("dead", false);
        EnemyNav = gameObject.GetComponent<NavMeshAgent>();
        blood.SetFloat("Velocity Multiplier", 6f);
        blood.Stop();
        birdDissolve = gameObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        birdDissolve.material.SetFloat("_DissolveController", dissolveValue);

        if (life <= 0 && !death)
        {
            EnemyDeath();
            death = true;
        }
    }

    public void BloodSplat()
    {
        blood.Play();
        //Debug.Log("BLOOD!");
        bloodSplatter[Random.Range(0,bloodSplatter.Length)].Play();
    }

    private void EnemyDeath()
    {
        EnemyAnim.SetBool("dead", true);
        Debug.Log(EnemyNav.gameObject.name + " died!");
        EnemyNav.gameObject.GetComponent<Actor>().Death();

        //Debug.Log("Deregister Boss from list");


        //Respawn.enemyDead++;
    }
}
