using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Timer : MonoBehaviour
{
    private Text text;
    private float timer = 0.0f;
    private bool running = false;

    private void OnEnable()
    {
        CS_Notifications.Instance.Register(this, "OnStartMenu");
        CS_Notifications.Instance.Register(this, "OnPlayGame");
        CS_Notifications.Instance.Register(this, "OnVictory");
        CS_Notifications.Instance.Register(this, "OnGameOver");
    }

    private void OnDisable()
    {
        try
        {
            CS_Notifications.Instance.Unregister(this, "OnStartMenu");
            CS_Notifications.Instance.Unregister(this, "OnPlayGame");
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

    private void Start ()
    {
        text = GetComponent<Text>();
    }

    private void Update ()
    {
        if (!running) { return; }

        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        CS_Medals.Instance.timer = timer;

        if (CS_WorldManager.Instance.level == 0)
        {
            if (timer >= 106)
            {
                CS_Medals.Instance.Time();
            }
        }
        else if (CS_WorldManager.Instance.level == 1)
        {
            if (timer >= 123)
            {
                CS_Medals.Instance.Time();
            }
        }
        else if (CS_WorldManager.Instance.level == 2)
        {
            if (timer >= 111)
            {
                CS_Medals.Instance.Time();
            }
        }
    }

    public void TimerStart()
    {
        timer = 0.0f;
        running = true;
    }

    public void TimerPause()
    {
        running = false;
    }

    public void TimerResume()
    {
        running = true;
    }

    public void OnStartMenu()
    {
        running = false;
        if (text)
        {
            text.text = "";
        }
    }

    public void OnPlayGame()
    {
        TimerStart();
    }

    public void OnVictory()
    {
        TimerPause();
    }

    private void OnGameOver()
    {
        TimerPause();
    }
}
