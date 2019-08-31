using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;


public class PlayerHealthConcept : MonoBehaviour
{
//Player Health in %.
    //current actual HP
    [SerializeField] private float healthCurrent;
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
    [SerializeField] private int soundShellshockBoundMax = 6000;
    private int soundShellshockRangeMax;
    [SerializeField] private int soundShellshockBoundMin = 4350;
    [SerializeField] private int soundCriticalBoundMax = 4350;
    private int soundCriticalRangeMax;
    [SerializeField] private int soundCriticalBoundMin = 500;
//Saturation
    [SerializeField]
    ColorGrading colorGrading;
    [SerializeField]
    PostProcessVolume volume;
    [SerializeField] private int desaturationStateBoundMax = 30;
    private int desaturationStateRangeMax;
    [SerializeField] private int desaturationStateBoundMin = 0;
    [SerializeField] private int desaturationCriticalBoundMax = 100;
    private int desaturationCriticalRangeMax;
    [SerializeField] private int desaturationCriticalBoundMin = 30;


    public float HealthCurrent { get => healthCurrent; private set => healthCurrent = value; }

    //Presentation UI
    [SerializeField]
    private InputField intputField;
    [SerializeField]
    private TextMeshProUGUI tmpText;

    

    private void Awake()
    {
        HealthCurrent = healthMax;
        healthCurrentFeedback = HealthCurrent;
        SetFeedbackToDefault();
        soundShellshockRangeMax = soundShellshockBoundMax - soundShellshockBoundMin;
        soundCriticalRangeMax = soundCriticalBoundMax - soundCriticalBoundMin;
        desaturationStateRangeMax = desaturationStateBoundMax - desaturationStateBoundMin;
        desaturationCriticalRangeMax = desaturationCriticalBoundMax - desaturationCriticalBoundMin;
        volume.profile.TryGetSettings(out colorGrading);

        Cursor.visible=true;
        intputField.ActivateInputField();
        intputField.Select();
    }

    private void Update()
    {
        SetHealthFeedback();
        ChooseFeedback();
        tmpText.SetText("Current FeedbackHP: " + healthCurrentFeedback);
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
            int valueInRange = Mathf.RoundToInt((relativePercent * soundShellshockRangeMax)/100);
            mixer.SetFloat("lowPass", valueInRange + soundShellshockBoundMin);
    }

    private void DesatState()
    {
        if(healthCurrentFeedback > 50)
        {
            colorGrading.saturation.value = 0f;
        }
        else
        {
            float relativePercent = 100-((healthCurrentFeedback-20)*100)/30;
            int valueInRange = Mathf.RoundToInt((relativePercent * desaturationStateRangeMax)/100);
            Debug.Log("value in range " + valueInRange);
            colorGrading.saturation.value = -(valueInRange + desaturationStateBoundMin);
            Debug.Log(-(valueInRange + desaturationStateBoundMin));
        }
    }

    private void CriticalState()
    {
        if(healthCurrentFeedback <= 20)
        {
            //Sound
            //0-20% --> 15/20 = x/100  & 20/100 = x/4500
            float relativePercent = (healthCurrentFeedback*100)/20;
            int valueInRange = Mathf.RoundToInt((relativePercent * soundCriticalBoundMax)/100);
            mixer.SetFloat("lowPass", valueInRange + soundCriticalBoundMin);

            //Saturation
            float relativePercentSat = 100-((healthCurrentFeedback)*100)/20;
            int valueInRangeSat = Mathf.RoundToInt((relativePercentSat * desaturationCriticalRangeMax)/100);
            colorGrading.saturation.value = -(valueInRangeSat + desaturationCriticalBoundMin);
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

    public void InputField()
    {
        TakeDamage(float.Parse(intputField.text));
        intputField.text = "";
        intputField.Select();
    }

    public void DamageButton(float damage)
    {
        TakeDamage(damage);
    }
}
