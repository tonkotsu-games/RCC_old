using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    Camera introCam;
    public bool testing;
    public State stateTest;

    public static GameState instance;

    public static State stateCurrent = State.Menue;
    public static State StateCurrent

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

    public static void TryChangeState(State requestState)
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
            if (SceneManager.GetActiveScene().name != "Start_Menu")
            {
                SceneManager.LoadScene("Start_Menu");
            }
            Debug.Log("Menue State select a Button");
        }
        else if (StateCurrent == State.Play)
        {
            if (SceneManager.GetActiveScene().name != "Arena_Trailer_scene")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                SceneManager.LoadScene("Metrodome", LoadSceneMode.Additive);
            }
            Debug.Log("Let's Play");
        }
        else if (StateCurrent == State.Exit)
        {
            Debug.Log("Quit the Game");
            Application.Quit();
        }
        else if (StateCurrent == State.Pause)
        {
            Debug.Log("Pause");
        }

        else
        {
            Debug.LogWarning("WARNING");
        }
    }


    void Start()
    {
        if (testing && SceneManager.GetActiveScene().name != "Arena")
        {
            stateCurrent = stateTest;
        }
    }
}
