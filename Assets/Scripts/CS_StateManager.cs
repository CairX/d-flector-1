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
        Playing,
        Tutorial
    }

    public static State GetState(string s)
    {
        switch (s)
        {
            case "StartMenu":
                return State.StartMenu;
            case "PauseMenu":
                return State.PauseMenu;
            case "OptionsMenu":
                return State.OptionsMenu;
            case "VictoryMenu":
                return State.VictoryMenu;
            case "GameOverMenu":
                return State.GameOverMenu;
            case "Playing":
                return State.Playing;
            case "Tutorial":
                return State.Tutorial;
            default:
                return State.StartMenu;
        }
    }

    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject victoryMenu;
    public GameObject gameOverMenu;
    public GameObject playing;
    public GameObject tutorialScreen;

    private State previousState;

    private void Start()
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
                OptionsMenu("StartMenu");
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
            case State.Tutorial:
                GoToTutorial();
                break;
            default:
                break;
        }
    }

    private GameObject GetStateObject(State state)
    {
        switch (state)
        {
            case State.StartMenu:
                return startMenu;
            case State.PauseMenu:
                return pauseMenu;
            case State.OptionsMenu:
                return optionsMenu;
            case State.VictoryMenu:
                return victoryMenu;
            case State.GameOverMenu:
                return gameOverMenu;
            case State.Playing:
                return playing;
            case State.Tutorial:
                return tutorialScreen;
            default:
                throw new System.Exception("No GameObject matched with State.");
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
        if (tutorialScreen != null)
        {
            tutorialScreen.SetActive(false);
        }
    }

    public void OnVictory()
    {
        CS_All_Audio.Instance.WinLose(true);
        CS_WorldManager.Instance.state = State.VictoryMenu;
        DisableAll();
        victoryMenu.SetActive(true);
        Cursor.visible = true;
        CS_Medals.Instance.LevelComplet();
    }

    private void OnGameOver()
    {
        CS_All_Audio.Instance.WinLose(false);
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

        CS_Notifications.Instance.Post(this, "OnStartMenu");
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

    public void OptionsMenu(string previous)
    {
        previousState = GetState(previous);

        CS_WorldManager.Instance.state = State.OptionsMenu;
        GetStateObject(previousState).SetActive(false);
        optionsMenu.SetActive(true);
        Cursor.visible = true;
    }
    public void ReturnFromOptions()
    {
        CS_WorldManager.Instance.state = previousState;
        optionsMenu.SetActive(false);
        GetStateObject(previousState).SetActive(true);
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        CS_WorldManager.Instance.state = State.Playing;
        SceneManager.LoadScene(0);
    }

    private void PlayGame()
    {
        CS_WorldManager.Instance.state = State.Playing;
        Time.timeScale = 1;
        DisableAll();
        playing.SetActive(true);
        Cursor.visible = false;
        CS_Medals.Instance.LevelStart();
        CS_Notifications.Instance.Post(this, "OnPlayGame");
    }
    public void GoToTutorial()
    {
        CS_WorldManager.Instance.state = State.Tutorial;
        DisableAll();
        tutorialScreen.SetActive(true);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
}
