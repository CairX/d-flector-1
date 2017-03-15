using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_BaseMusic : MonoBehaviour
{

    public Slider SoundSlider;

    private AudioSource speaker;

    public AudioClip intro;

    public AudioClip loop1;
    public AudioClip transistion1;
    public AudioClip loop2;
    public AudioClip transistion2;
    public AudioClip loop3;

    public AudioClip end;

    public AudioClip menuMusic;

    private AudioClip curentstart;
    private AudioClip curentloop;

    public GameObject vicktory;
    public GameObject failure;

    public GameObject lv1;
    public GameObject lv2;
    public GameObject lv3;
    public GameObject pause;
    public GameObject Menu;

    public GameObject gUI;
    public GameObject game;

    private int stage = 1;

    private bool okay = true;

    private bool test = false;

    // Use this for initialization
    private void Start()
    {
        CS_Notifications.Instance.Register(this, "OnSoundV");
        speaker = GetComponent<AudioSource>();
        //speaker.volume = 0.2f;
        speaker.Stop();
        if (Menu.activeSelf)
        {
            speaker.clip = menuMusic;
            speaker.Play();
            speaker.loop = true;
        }
        else if (lv1.activeSelf)
        {
            DiffrentLevel(1);
        }
        else if (lv2.activeSelf)
        {
            DiffrentLevel(2);
        }
        else if (lv3.activeSelf)
        {
            DiffrentLevel(3);
        }
        SoundSlider.value = CS_WorldManager.Instance.volumeMusic;
        speaker.volume = CS_WorldManager.Instance.volumeMusic;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && gUI.activeSelf && game.activeSelf)
        {
            PauseScreen();
        }
        if (Menu.activeSelf)
        {
            okay = false;
            if (speaker.isPlaying == false)
            {
                speaker.clip = menuMusic;
                speaker.Play();
                speaker.loop = true;
            }
        }
        
        if (vicktory.activeSelf || failure.activeSelf)
        {
            speaker.Stop();
            okay = false;
        }
        else if (lv1.activeSelf && test == false)
        {
            speaker.Stop();
            DiffrentLevel(1);
        }
        else if (lv2.activeSelf && test == false)
        {
            speaker.Stop();
            DiffrentLevel(2);
        }
        else if (lv3.activeSelf && test == false)
        {
            speaker.Stop();
            DiffrentLevel(3);
        }
        else if (okay && speaker.isPlaying == false && speaker.loop == false)
        {
            speaker.Stop();
            Nextstep();
        }
       
    }
    public void PlayMusic()
    {
        speaker.UnPause();
        okay = true;
    }
    public void PaseMusic()
    {
        speaker.Pause();
        okay = true;
    }
    public void RestartMusic()
    {
        speaker.UnPause();
        okay = true;
    }
    public void DiffrentLevel(int i)
    {
        if (i == 1)
        {
            curentloop = loop1;
            curentstart = intro;
        }
        if (i == 2)
        { 
            curentloop = loop2;
            curentstart = transistion1;
        }
        if (i == 3)
        {
            curentloop = loop3;
            curentstart = transistion2;
        }
        stage = 1;
        speaker.loop = false;
        test = true;
    }
    public void PauseScreen()
    {
        if (speaker.isPlaying == true)
        {
            speaker.Pause();
            okay = false;
        }
        else
        {
            speaker.UnPause();
            okay = true;
        }
    }
    public void Nextstep()
    {
        if (stage == 1)
        {
            speaker.PlayOneShot(curentstart);
            stage++;
        }
        else if (stage == 2)
        {
            speaker.clip = curentloop;
            speaker.Play();
            speaker.loop = true;
            stage++;
        }
        else if (stage == 3 && speaker.loop == false)
        {
            speaker.PlayOneShot(end);
            stage++;
        }
        else if (stage == 4)
        {
            CS_Notifications.Instance.Post(this, "OnVictory");
        }

    }

    public void OnValueChanged()
    {
        CS_WorldManager.Instance.volumeMusic = SoundSlider.value;
        speaker.volume = SoundSlider.value;
    }
    private void OnSoundV()
    {
        speaker.loop = false;
    }
}