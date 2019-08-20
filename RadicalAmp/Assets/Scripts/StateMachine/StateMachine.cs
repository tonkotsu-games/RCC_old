using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState stateCurrent;
    private IState statePrevious;

    public void ChangeState(IState newState)
    {
        Debug.Log("Changing State");

        if(stateCurrent != null)
        {
            stateCurrent.Exit();
        }

        statePrevious = stateCurrent;
        stateCurrent = newState;
        stateCurrent.Enter();
    }

    public void StateExecuteTick()
    {
        //Debug.Log("StateExecuteTick");
        if(stateCurrent != null)
        {
            stateCurrent.Execute();
        }
    }

    public void ReturnToPreviousState()
    {
        stateCurrent = statePrevious;
    }
}
