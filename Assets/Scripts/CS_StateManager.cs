using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_StateManager : MonoBehaviour {

    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject victoryMenu;
    public GameObject gameOverMenu;
    public GameObject playing;

	void Awake()
    {
        StartMenu();

        CS_Notifications.Instance.Add(this, "OnVictory");
        CS_Notifications.Instance.Add(this, "OnGameOver");
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

    void OnVictory()
    {
        DisableAll();
    }

    void OnGameOver()
    {
        DisableAll();
    }

    public void StartMenu()
    {
        DisableAll();
        startMenu.SetActive(true);
    }

    public void OptionsMenu()
    {
        DisableAll();
        optionsMenu.SetActive(true);
    }

    public void PlayGame()
    {
        DisableAll();
        playing.SetActive(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
