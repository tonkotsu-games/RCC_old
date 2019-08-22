using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;

    bool menuOpen = false;

    [SerializeField] AudioSource[] audioSource;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetButtonDown("Start") && !menuOpen)
        {
            for(int i = 0;i<audioSource.Length;i++)
            {
                audioSource[i].Pause();

            }
            Cursor.visible = true;
            menuOpen = true;
            menu.SetActive(true);
            Time.timeScale = 0;
            GameState.TryChangeState(GameState.State.Pause);
        }
        else if(Input.GetButtonDown("Start") && menuOpen)
        {
            for (int i = 0; i < audioSource.Length; i++)
            {
                audioSource[i].UnPause();
            }
            Cursor.visible = false;
            menuOpen = false;
            menu.SetActive(false);
            Time.timeScale = 1;
            GameState.TryChangeState(GameState.State.Play);
        }
    }
    public void Exit()
    {
        menuOpen = false;
        menu.SetActive(false);
        Time.timeScale = 1;
        GameState.TryChangeState(GameState.State.Menue);
    }
    public void Resume()
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i].UnPause();
        }
        Cursor.visible = false;
        menuOpen = false;
        menu.SetActive(false);
        Time.timeScale = 1;
        GameState.TryChangeState(GameState.State.Play);
    }
}