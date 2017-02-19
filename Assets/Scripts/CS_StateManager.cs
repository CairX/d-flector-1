using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_StateManager : MonoBehaviour {

    public enum State
    {
        StartMenu,
        PauseMenu,
        OptionsMenu,
        VictoryMenu,
        GameOverMenu,
        Playing
    }

    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject victoryMenu;
    public GameObject gameOverMenu;
    public GameObject playing;

    private GameObject lastState;

	private void Awake()
    {
        Init(CS_WorldManager.Instance.state);
    }

    private void OnEnable()
    {
        CS_Notifications.Instance.Register(this, "OnVictory");
        CS_Notifications.Instance.Register(this, "OnGameOver");
    }

    private void OnDisable()
    {
        try
        {
            CS_Notifications.Instance.Unregister(this, "OnVictory");
            CS_Notifications.Instance.Unregister(this, "OnGameOver");
        }
        catch (System.NullReferenceException)
        {
            // Unity destroys objects in random order so there is no way
            // to know if this is run before or after the
            // notification center has been destroyed.
        }

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (CS_WorldManager.Instance.state == State.Playing)
            {
                PauseMenu();
            } else if (CS_WorldManager.Instance.state == State.PauseMenu)
            {
                UnPauseMenu();
            }
        }
    }

    private void Init(State state)
    {
        switch (state)
        {
            case State.StartMenu:
                StartMenu();
                break;
            case State.PauseMenu:
                break;
            case State.OptionsMenu:
                //not corect objekt use
                OptionsMenu(lastState);
                break;
            case State.VictoryMenu:
                OnVictory();
                break;
            case State.GameOverMenu:
                OnGameOver();
                break;
            case State.Playing:
                PlayGame();
                break;
            default:
                break;
        }
    }

    private void DisableAll()
    {
        if (startMenu != null)
        {
            startMenu.SetActive(false);
        }
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(false);
        }
        if (victoryMenu != null)
        {
            victoryMenu.SetActive(false);
        }
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(false);
        }
        if (playing != null)
        {
            playing.SetActive(false);
        }
    }

    private void OnVictory()
    {
        CS_WorldManager.Instance.state = State.VictoryMenu;
        DisableAll();
        victoryMenu.SetActive(true);
        Cursor.visible = true;
    }

    private void OnGameOver()
    {
        CS_WorldManager.Instance.state = State.GameOverMenu;
        DisableAll();
        gameOverMenu.SetActive(true);
        Cursor.visible = true;
    }

    public void StartMenu()
    {
        CS_WorldManager.Instance.state = State.StartMenu;
        DisableAll();
        startMenu.SetActive(true);
        Cursor.visible = true;
    }

    private void PauseMenu()
    {
        CS_WorldManager.Instance.state = State.PauseMenu;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
    }

    public void UnPauseMenu()
    {
        CS_WorldManager.Instance.state = State.Playing;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }

    public void OptionsMenu(GameObject curentstate)
    {

        lastState = curentstate;
        CS_WorldManager.Instance.state = State.OptionsMenu;
        //DisableAll();
        lastState.SetActive(false);
        optionsMenu.SetActive(true);
        Cursor.visible = true;
    }
    public void ReturnFromOptions()
    {
        CS_WorldManager.Instance.state = State.OptionsMenu;
        optionsMenu.SetActive(false);
        lastState.SetActive(true);
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        CS_WorldManager.Instance.state = State.Playing;
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void PlayGame()
    {
        CS_WorldManager.Instance.state = State.Playing;
        DisableAll();
        playing.SetActive(true);
        Cursor.visible = false;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
