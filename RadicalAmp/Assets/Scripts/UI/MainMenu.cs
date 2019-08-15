using System.Collections;
using System.Collections.Generic;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		SceneManager.LoadScene("Metrodome", LoadSceneMode.Additive);
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
        Application.Quit();
        Debug.Log("Quit Game"); 
    }

    public void Credits()
    {
        options.SetActive(false);
        credits.SetActive(true);
        eventSystem[1].SetActive(false);
        eventSystem[2].SetActive(true);
    }
}
