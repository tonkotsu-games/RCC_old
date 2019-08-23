using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState stateCurrent;
    private IState statePrevious;
    private bool paused = false;

    public IState StateCurrent { get => stateCurrent; private set => stateCurrent = value; }

    public void ChangeState(IState newState)
    {
        //Debug.Log("Changing State");

        if(StateCurrent != null)
        {
            StateCurrent.Exit();
        }

        statePrevious = StateCurrent;
        StateCurrent = newState;
        StateCurrent.Enter();
    }

    public void StateExecuteTick()
    {
        //Debug.Log("StateExecuteTick");
        if(StateCurrent != null && !paused)
        {
            StateCurrent.Execute();
        }
    }

    public void ReturnToPreviousState()
    {
        StateCurrent = statePrevious;
    }

    public void TogglePause()
    {
        paused = !paused;
    }
}
