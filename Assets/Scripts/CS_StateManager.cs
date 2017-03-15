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
        LevelSelect,
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
            case "LevelSelect":
                return State.LevelSelect;
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
    public GameObject levelSelect;

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
            case State.LevelSelect:
                levelSelectScreen();
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
            case State.LevelSelect:
                return levelSelect;
            default:
                throw new System.Exception("No GameObject matched with State.");
        }
    }

    private void DisableAllStates()
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
        if(levelSelect != null)
        {
            levelSelect.SetActive(false);
        }

        RestoreCursor();
    }

    private void RestoreCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnVictory()
    {
        DisableAllStates();

        CS_All_Audio.Instance.WinLose(true);
        CS_WorldManager.Instance.state = State.VictoryMenu;
        victoryMenu.SetActive(true);
        CS_Medals.Instance.LevelComplet();
    }

    private void OnGameOver()
    {
        DisableAllStates();

        CS_All_Audio.Instance.WinLose(false);
        CS_WorldManager.Instance.state = State.GameOverMenu;
        gameOverMenu.SetActive(true);
    }

    public void StartMenu()
    {
        DisableAllStates();

        CS_WorldManager.Instance.state = State.StartMenu;
        startMenu.SetActive(true);

        CS_Notifications.Instance.Post(this, "OnStartMenu");
    }

    private void PauseMenu()
    {
        RestoreCursor();

        CS_WorldManager.Instance.state = State.PauseMenu;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void UnPauseMenu()
    {
        CS_WorldManager.Instance.state = State.Playing;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void OptionsMenu(string previous)
    {
        RestoreCursor();

        previousState = GetState(previous);
        CS_WorldManager.Instance.state = State.OptionsMenu;
        GetStateObject(previousState).SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void ReturnFromOptions()
    {
        RestoreCursor();

        CS_WorldManager.Instance.state = previousState;
        optionsMenu.SetActive(false);
        GetStateObject(previousState).SetActive(true);
    }

    public void LoadLevel(int level)
    {
        CS_Wave_Spawner_Loader loader = playing.GetComponent<CS_Wave_Spawner_Loader>();
        if (level < loader.levels.Count)
        {
            CS_WorldManager.Instance.level = level;
            RestartGame();
        } else
        {
            levelSelectScreen();
        }
    }

    public void Nextlevel()
    {
        LoadLevel(CS_WorldManager.Instance.level + 1);
    }

    public void RestartGame()
    {
        CS_WorldManager.Instance.state = State.Playing;
        SceneManager.LoadScene(0);
    }

    private void PlayGame()
    {
        DisableAllStates();

        CS_WorldManager.Instance.state = State.Playing;
        Time.timeScale = 1;
        playing.SetActive(true);
        CS_Medals.Instance.LevelStart();
        CS_Notifications.Instance.Post(this, "OnPlayGame");
    }

    public void GoToTutorial()
    {
        CS_WorldManager.Instance.state = State.Tutorial;
        DisableAllStates();
        tutorialScreen.SetActive(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void levelSelectScreen()
    {
        CS_WorldManager.Instance.state = State.LevelSelect;
        DisableAllStates();
        levelSelect.SetActive(true);
    }
}