using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject options;
    [SerializeField] GameObject[] eventSystem;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credits;

    public void PlayGame ()
    {
        GameState.TryChangeState(GameState.State.Play);
    }

    public void Options()
    {
        options.SetActive(true);
        mainMenu.SetActive(false);
        credits.SetActive(false);
        eventSystem[0].SetActive(false);
        eventSystem[1].SetActive(true);
        eventSystem[2].SetActive(false);
    }

    public void Menu()
    {
        options.SetActive(false);
        eventSystem[0].SetActive(true);
        eventSystem[1].SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame ()
    {
        GameState.TryChangeState(GameState.State.Exit);
    }

    public void Credits()
    {
        options.SetActive(false);
        credits.SetActive(true);
        eventSystem[1].SetActive(false);
        eventSystem[2].SetActive(true);
    }
}