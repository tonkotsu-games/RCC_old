using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{

    [SerializeField] Animator anim;
	[SerializeField] TextMeshProUGUI tmproText;
    [SerializeField] GameObject tutorialContainer;
    [SerializeField] GameObject gate;
    [SerializeField] GameObject TutorialText;
    public TutorialSteps currentStep;

    bool tutorialPlay = false;

    float tutorialTimer = 5;

    public void StartTutorial()
    {
        currentStep = TutorialSteps.MovementInfo;
    }
    private void Awake()
    {
        tutorialContainer.SetActive(false);
    }

    private void Update()
    {
        if (currentStep == TutorialSteps.PreTutorial)
        {
            if (Input.GetButtonDown("Dash"))
            {
                StartTutorial();
                TutorialText.SetActive(false);
            }
            if (Input.GetButtonDown("Attack"))
            {
                currentStep = TutorialSteps.TutorialFinish;
                TutorialText.SetActive(false);
            }
        }

        if(currentStep == TutorialSteps.MovementInfo && !tutorialPlay)
        {
            tutorialContainer.SetActive(true);
            tmproText.text = "stage1";
            anim.Play("AnimMovement");
            tutorialTimer = 5;
            tutorialPlay = true;
        }
        if(currentStep == TutorialSteps.MovementTest)
        {
            if((Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || 
                Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0) && 
                !tutorialPlay)
            {
                tutorialTimer = 2;
                tutorialPlay = true;
            }
        }
        if (currentStep == TutorialSteps.DashInfo && !tutorialPlay)
        {
            tutorialContainer.SetActive(true);
            tmproText.text = "stage2";
            anim.Play("AnimDash");
            tutorialTimer = 5;
            tutorialPlay = true;
        }
        if (currentStep == TutorialSteps.DashTest)
        {
            if (Input.GetButtonDown("Dash") && !tutorialPlay)
            {
                tutorialTimer = 2;
                tutorialPlay = true;
            }
        }
        if (currentStep == TutorialSteps.AttackInfo && !tutorialPlay)
        {
            tutorialContainer.SetActive(true);
            tmproText.text = "stage3";
            anim.Play("AnimAttack");
            tutorialTimer = 5;
            tutorialPlay = true;
        }
        if (currentStep == TutorialSteps.AttackTest)
        {
            if (Input.GetButtonDown("Attack") && !tutorialPlay)
            {
                tutorialTimer = 2;
                tutorialPlay = true;
            }
        }
        if (currentStep == TutorialSteps.DanceInfo && !tutorialPlay)
        {
            tutorialContainer.SetActive(true);
            tmproText.text = "stage4";
            anim.Play("AnimDance");
            tutorialTimer = 5;
            tutorialPlay = true;
        }
        if (currentStep == TutorialSteps.DanceTest)
        {
            if (Input.GetButtonDown("Dance") && !tutorialPlay)
            {
                tutorialTimer = 2;
                tutorialPlay = true;
            }
        }
        if(currentStep == TutorialSteps.TutorialFinish)
        {
            gate.SetActive(false);
        }

        if (tutorialPlay)
        {
            //Debug.Log(tutorialTimer);
            tutorialTimer -= Time.deltaTime;
        }
        if (tutorialTimer <= 0)
        {
            tutorialPlay = false;
            currentStep += 1;
            tutorialTimer = 1;
            tutorialContainer.SetActive(false);
        }
    }

    public void Testing()
    {
        currentStep = TutorialSteps.TutorialFinish;
    }

    public enum TutorialSteps
    {
        PreTutorial,
        MovementInfo,
        MovementTest,
        DashInfo,
        DashTest,
        AttackInfo,
        AttackTest,
        DanceInfo,
        DanceTest,
        TutorialFinish
    }
}