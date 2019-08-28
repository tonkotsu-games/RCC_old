﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClone : MonoBehaviour
{
    public static TutorialClone instance;

    Animator cloneAnim;

    public bool running;
    public bool hitting;
    public bool dashing;
    public bool dancing;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cloneAnim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (running)
        {
            PlayRunning(true);
        }
        else
        {
            PlayRunning(false);
        }

        if (hitting)
        {
            PlayAttack(true);
        }
        else
        {
            PlayAttack(false);
        }

        if (dashing)
        {
            PlayDash(true);
        }
        else
        {
            PlayDash(false);
        }

        if (dancing)
        {
            PlayDance(true);
        }
        else
        {
            PlayDance(false);
        }
    }

    public void PlayRunning(bool active)
    {
        cloneAnim.SetBool("Running", active);
    }

    public void PlayAttack(bool active)
    {
        cloneAnim.SetBool("Hitting", active);
    }

    public void PlayDash(bool active)
    {
        cloneAnim.SetBool("Dashing", active);
    }
    public void PlayDance(bool active)
    {
        cloneAnim.SetBool("Dancing", active);
    }
   
}