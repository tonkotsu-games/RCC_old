using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum State
    {
        Menue,
        Play,
        Pause,
        Test,
        Exit
    }

    

    State stateCurrent = State.Menue;

    public static GameState instance;

    public State StateCurrent
    {
        get
        {
            return stateCurrent;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TryChangeState(State requestState)
    {
        Debug.Log("Try to change State from "
            + StateCurrent.ToString()
            + " to "
            + requestState.ToString());

        if (requestState == StateCurrent)
        {
            Debug.Log("Already in State "
                + requestState.ToString());
            return;
        }
        else
        {
            stateCurrent = requestState;
        }

        if (StateCurrent == State.Menue)
        {
            Debug.Log("Menue State select a Button");
        }

        else if (StateCurrent == State.Play)
        {
            Debug.Log("Let's Play");
        }

        else if (StateCurrent == State.Exit)
        {
            Debug.Log("Quit the Game");
            Application.Quit();
        }

        else
        {
            Debug.LogWarning("WARNING");
        }
    }


    void Start()
    {
    }

}
