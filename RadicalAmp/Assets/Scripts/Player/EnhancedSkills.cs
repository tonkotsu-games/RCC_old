using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedSkills : MonoBehaviour
{
    public static EnhancedSkills instance;

    [HideInInspector]
    public enum EnhancedState { Active,Inactive}
    [HideInInspector]
    public enum ActionsToEnhance { Attack,Dash}

    public EnhancedState currentEnhancedState;

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
            if(requestedState == EnhancedState.Active)
            {
                currentEnhancedState = EnhancedState.Active;
                Debug.Log("Enhanced State now " + currentEnhancedState);
            }

            else if(requestedState == EnhancedState.Inactive)
            {
                currentEnhancedState = EnhancedState.Inactive;
                Debug.Log("Enhanced State now " + currentEnhancedState);
            }
        }
    }

    public void UseEnhancedSkill (ActionsToEnhance baseSkill)
    {
        Debug.Log("Using Enhanced " + baseSkill);
    }

}
