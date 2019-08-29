using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject clone;
    [SerializeField] Animator anim;
    [SerializeField] Image image;
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject tutorialContainer;
    [SerializeField] GameObject gate;
    [SerializeField] GameObject TutorialText;
    [SerializeField] Slider juiceMeter;

    private TutorialClone cloneAnim;

    public TutorialSteps currentStep;

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
        cloneAnim = clone.GetComponent<TutorialClone>();
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
            if (Input.GetButtonDown("Attack"))
            {
                currentStep = TutorialSteps.TutorialFinish;
                TutorialText.SetActive(false);
            }
        }

        if(currentStep == TutorialSteps.MovementInfo && !tutorialPlay)
        {
            cloneAnim.PlayRunning(true);
            tutorialContainer.SetActive(true);
            image.sprite = sprites[0];
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
            cloneAnim.PlayRunning(false);
            cloneAnim.PlayDash(true);
            tutorialContainer.SetActive(true);
            image.sprite = sprites[1];
            anim.Play("AnimDash");
            tutorialTimer = 5;
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
            tutorialContainer.SetActive(true);
            image.sprite = sprites[2];
            anim.Play("AnimAttack");
            tutorialTimer = 5;
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
            tutorialContainer.SetActive(true);
            image.sprite = sprites[3];
            anim.Play("AnimDance");
            tutorialTimer = 5;
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
            tutorialContainer.SetActive(true);
            image.sprite = sprites[4];
            tutorialTimer = 5;
            tutorialPlay = true;
        }
        if (currentStep == TutorialSteps.JuiceTest)
        {
            if(juiceMeter.value >= 30)
            {
                currentStep += 1;
            }
        }
        if(currentStep == TutorialSteps.TutorialFinish)
        {
            clone.SetActive(false);
            cloneAnim.PlayDance(false);
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
        JuiceInfo,
        JuiceTest,
        TutorialFinish
    }
}