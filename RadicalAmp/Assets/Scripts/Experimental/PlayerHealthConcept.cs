using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHealthConcept : MonoBehaviour
{
//Player Health in %.
    //current actual HP
    [SerializeField]
    private float healthCurrent;
    //feedback HP
    private float healthCurrentFeedback;
    //maximum HP
    private float healthMax = 100;
    //Speed at which the Feedback health moves towards the actual one
    [SerializeField] private float speed;
    private bool fullHP = true;

//Sound
    [SerializeField]
    private AudioMixer mixer;
    int soundDefault = 20000;
    //Bounds calculation: max 6000 - min 500 --> 30% = 1650 --> 6000-4350 & 4350 - 500
    private int soundShellshockBoundMax = 6000;
    private int soundShellshockRangeMax;
    private int soundShellshockBoundMin = 4350;
    private int soundCriticalBoundMax = 4350;
    private int soundCriticalRangeMax;
    private int soundCriticalBoundMin = 500;
//Saturation


    public float HealthCurrent { get => healthCurrent; private set => healthCurrent = value; }

    private void Awake()
    {
        HealthCurrent = healthMax;
        healthCurrentFeedback = HealthCurrent;
        SetFeedbackToDefault();
        soundShellshockRangeMax = soundShellshockBoundMax - soundShellshockBoundMin;
        Debug.Log(soundShellshockRangeMax);
        soundCriticalRangeMax = soundCriticalBoundMax - soundCriticalBoundMin;
    }

    private void Update()
    {
        SetHealthFeedback();
        ChooseFeedback();
    }

    private void SetHealthFeedback()
    {
        if(healthMax - healthCurrentFeedback < 0.01 && healthCurrent == healthMax)
        {
            healthCurrentFeedback = healthCurrent;
            SetFeedbackToDefault();
        }
        else
        {
            healthCurrentFeedback += (healthCurrent - healthCurrentFeedback) * Time.deltaTime * speed * 0.1f;
        }
    }

    private void ChooseFeedback()
    {
        if(healthCurrentFeedback > 90)
        {
            SetFeedbackToDefault();
        }
        else if(healthCurrentFeedback > 20)
        {
            ShellshockState();
            DesatState();
        }
        else
        {
            CriticalState();
        }
    }

    private void ShellshockState()
    {
            //20-90% --> 23/70 = x/100 --> (23*100)/70  & 20/100 = x/1500
            float relativePercent = ((healthCurrentFeedback-20)*100)/70;
            //Debug.Log(relativePercent);
            int valueInRange = Mathf.RoundToInt((relativePercent * soundShellshockRangeMax)/100);
            Debug.Log("value" + valueInRange);
            mixer.SetFloat("lowPass", valueInRange + soundShellshockBoundMin);
    }

    private void DesatState()
    {
        if(healthCurrentFeedback > 50)
        {
            //set desat to default
        }
        else
        {
            //check and set
        }
    }

    private void CriticalState()
    {
        if(healthCurrentFeedback <= 20)
        {
            //Debug.Log("Critical Range!");
            //0-20% --> 15/20 = x/100  & 20/100 = x/4500
            float relativePercent = (healthCurrentFeedback*100)/20;
            int valueInRange = Mathf.RoundToInt((relativePercent * soundCriticalBoundMax)/100);
            mixer.SetFloat("lowPass", valueInRange + soundCriticalBoundMin);
        }
    }

    private void SetFeedbackToDefault()
    {
        mixer.SetFloat("lowPass", soundDefault);
    }

    public void TakeDamage(float amount)
    {
        if(healthCurrent - amount > 0)
        {
            HealthCurrent = healthCurrent - amount;
        }
        else if(healthCurrent - amount <= 0 && healthCurrent == healthMax)
        {
            HealthCurrent = 1;
        }
        else
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.LogError("Player died!");
    }
}
