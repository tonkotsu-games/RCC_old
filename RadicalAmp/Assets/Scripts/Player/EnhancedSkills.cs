using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedSkills : MonoBehaviour
{
    public static EnhancedSkills instance;

    [HideInInspector]
    public enum EnhancedState { First, Second, Active ,Inactive}
    [HideInInspector]
    public enum ActionsToEnhance { Attack,Dash}
    [SerializeField] GameObject enhancedDashHitbox;

    public EnhancedState currentEnhancedState;

    [SerializeField] GameObject spotlights;

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
        
    }

    private void Start()
    {
        currentEnhancedState = EnhancedState.Inactive;
    }

    public void ChangeEnhancedState(EnhancedState requestedState)
    {
        if(requestedState == currentEnhancedState)
        {
            Debug.Log("Enhancement already " + requestedState);
            return;
        }

        else
        {
            switch (requestedState)
            {
                case EnhancedState.First:
                    currentEnhancedState = EnhancedState.First;
                    spotlights.GetComponent<SpotlightGroup>().EnableLights(0);
                    break;
                case EnhancedState.Second:
                    currentEnhancedState = EnhancedState.Second;
                    spotlights.GetComponent<SpotlightGroup>().EnableLights(1);
                    break;
                case EnhancedState.Active:

                    currentEnhancedState = EnhancedState.Active;
                    Debug.Log("Enhanced State now " + currentEnhancedState);
                    spotlights.GetComponent<SpotlightGroup>().EnableLights(2);
                    break;
                case EnhancedState.Inactive:

                    currentEnhancedState = EnhancedState.Inactive;
                    spotlights.GetComponent<SpotlightGroup>().DisableAllActiveLights();
                    Debug.Log("Enhanced State now " + currentEnhancedState);
                    break;
            }
        }
    }

    public void UseEnhancedSkill (ActionsToEnhance baseSkill)
    {
        if(baseSkill == ActionsToEnhance.Dash)
        {
            EnhanceDash();
        }
        ChangeEnhancedState(EnhancedState.Inactive);
        Debug.Log("Using Enhanced " + baseSkill);
    }


    public void EnhanceDash()
    { 
        enhancedDashHitbox.SetActive(true);
    }

    // Called through AnimEvent at the end of the dash
    public void DisableDashHit()
    {
        enhancedDashHitbox.SetActive(false);
    }
}
