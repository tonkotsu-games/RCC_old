using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject clone;
    [SerializeField] Animator anim;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI tmproText;
    [SerializeField] GameObject tutorialContainer;
    [SerializeField] GameObject gate;
    [SerializeField] GameObject TutorialText;
    [SerializeField] Slider juiceMeter;
    [SerializeField] GameObject doorCamera;
    [SerializeField] GameObject followCamera;

    [SerializeField] float setTimer;

    private TutorialClone cloneAnim;
    [SerializeField] private Material gateMaterial;
    [SerializeField] private Material gateGoldMaterial;

    public TutorialSteps currentStep;

    public List<GameObject> EmpowerClone;
    public List<GameObject> JuiceDashClone;

    private bool inTesting = false;

    bool tutorialPlay = false;

    float tutorialTimer = 5;
    int hitCounter = 0;

    bool animStageChange = false;

    public void StartTutorial()
    {
        currentStep = TutorialSteps.MovementInfo;
    }
    private void Awake()
    {
        if (clone != null)
        {
            cloneAnim = clone.GetComponent<TutorialClone>();
        }
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
                clone.SetActive(true);
            }
            if (Input.GetButtonDown("Jump"))
            {
                currentStep = TutorialSteps.TutorialPreFinish;
                TutorialText.SetActive(false);
            }
        }

        if(currentStep == TutorialSteps.MovementInfo && !tutorialPlay)
        {
            cloneAnim.PlayRunning(true);
            tutorialContainer.SetActive(true);
            tmproText.text = "Use joystick for movement";
            anim.Play("AnimMove");
            tutorialTimer = setTimer;
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
            cloneAnim.PlayRunning(false);
            cloneAnim.PlayDash(true);
            tmproText.text = "Press A to dash, dash three times.";
            anim.Play("AnimDash");
            tutorialTimer = setTimer;
            tutorialPlay = true;
        }
        if (currentStep == TutorialSteps.DashTest)
        {
            if (Input.GetButtonDown("Dash") && !tutorialPlay && hitCounter <= 2)
            {
                hitCounter++;
            }
            else if(hitCounter >=3)
            {
                currentStep += 1;
                hitCounter = 0;
            }
        }
        if (currentStep == TutorialSteps.AttackInfo && !tutorialPlay)
        {
            cloneAnim.PlayDash(false);
            cloneAnim.PlayAttack(true);
            tmproText.text = "Press RB to slash, slash three times.";
            anim.Play("AnimAttack");
            tutorialTimer = setTimer;
            tutorialPlay = true;
        }
        if (currentStep == TutorialSteps.AttackTest)
        {
            if (Input.GetButtonDown("Attack") && !tutorialPlay && hitCounter <= 2)
            {
                hitCounter++;
            }
            else if(hitCounter >= 3)
            {
                currentStep += 1;
                hitCounter = 0;
            }
        }
        if (currentStep == TutorialSteps.DanceInfo && !tutorialPlay)
        {
            cloneAnim.PlayAttack(false);
            cloneAnim.PlayDance(true);
            tmproText.text = "Press B to dance, dance three times.";
            anim.Play("AnimDance");
            tutorialTimer = setTimer;
            tutorialPlay = true;
        }
        if (currentStep == TutorialSteps.DanceTest)
        {
            if (Input.GetButtonDown("Dance") && !tutorialPlay && hitCounter <= 2)
            {
                hitCounter++;
            }
            else if(hitCounter >= 3)
            {
                currentStep += 1;
                hitCounter = 0;
                juiceMeter.value = 0;
            }
        }
        if (currentStep == TutorialSteps.JuiceInfo && !tutorialPlay)
        {
            tmproText.text = "Hit the beat three times, use your dash, shlash or dance.";
            tutorialTimer = setTimer;
            tutorialPlay = true;
        }
        if (currentStep == TutorialSteps.JuiceTest)
        {
            if(juiceMeter.value >= 15)
            {
                currentStep += 1;
                cloneAnim.PlayDance(false);
            }
        }
        if (currentStep == TutorialSteps.EmpowerSlashInfo && !tutorialPlay)
        {
            tmproText.text = "Hit the beat with the dance three times and \n then slash on beat to perform an Empowered Slash. Defeat the holograms.";
            anim.Play("Enchancedslash");
            foreach(GameObject clone in EmpowerClone)
            {
                clone.SetActive(true);
            }
            tutorialTimer = setTimer;
            tutorialPlay = true;

        }
        if (currentStep == TutorialSteps.EmpowerSlashTest)
        {
            if (EmpowerClone.Count == 0)
            {
                currentStep += 1;
            }
        }
        if (currentStep == TutorialSteps.JuiceDashInfo && !tutorialPlay)
        {
            tmproText.text = "At max juice, you can perform the Juice Dash, hold LT and A.\n The longer you charge the more enemies will be hit. Defeat the holograms.";
            anim.Play("Juicedash");
            foreach (GameObject clone in JuiceDashClone)
            {
                clone.SetActive(true);
            }
            tutorialTimer = setTimer;
            tutorialPlay = true;
        }
        if (currentStep == TutorialSteps.JuiceDashTest)
        {
            juiceMeter.value += 1;

            if (JuiceDashClone.Count == 0)
            {
                currentStep += 1;
                juiceMeter.value = 0;
                tutorialContainer.SetActive(false);
            }
        }
        if(currentStep == TutorialSteps.TutorialPreFinish)
        {
            if (gate != null)
            {
                StartDoorCamera();
                gateMaterial.SetFloat("Vector1_36A0E93A", Mathf.Lerp(gateMaterial.GetFloat("Vector1_36A0E93A"), 1f, 0.02f));
                gateGoldMaterial.SetFloat("Vector1_36A0E93A", Mathf.Lerp(gateGoldMaterial.GetFloat("Vector1_36A0E93A"), 1f, 0.02f));

                if (gateMaterial.GetFloat("Vector1_36A0E93A") >= 0.73f)
                {
                    clone.SetActive(false);
                    gate.SetActive(false);
                    currentStep += 1;
                }
            }
        }
        if (currentStep == TutorialSteps.TutorialFinish)
        {

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
        }
    }

    public void Testing()
    {
        currentStep = TutorialSteps.TutorialPreFinish;
        inTesting = true;
    }

    private void OnDisable()
    {
        gateMaterial.SetFloat("Vector1_36A0E93A", 0f);
        gateGoldMaterial.SetFloat("Vector1_36A0E93A", 0f);
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
        JuiceInfo,
        JuiceTest,
        EmpowerSlashInfo,
        EmpowerSlashTest,
        JuiceDashInfo,
        JuiceDashTest,
        TutorialPreFinish,
        TutorialFinish
    }

    private void StartDoorCamera()
    {
        if(inTesting)
        {
            Debug.LogError("Returned");
            return;
        }
        Debug.LogError("StartDoorCamera");
        followCamera.SetActive(false);
        doorCamera.SetActive(true);
    }
}