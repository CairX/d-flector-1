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
            Debug.Log("Menu");

            speaker.clip = menuMusic;
            speaker.Play();
            speaker.loop = true;
        }
        else if (lv1.activeSelf)
        {
            Debug.Log("Level 1");
            DiffrentLevel(1);
        }
        else if (lv2.activeSelf)
        {
            Debug.Log("Level 2");
            DiffrentLevel(2);
        }
        else if (lv3.activeSelf)
        {
            Debug.Log("Level 3");
            DiffrentLevel(3);
        }
        SoundSlider.value = CS_WorldManager.Instance.volumeMusic;

        }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseScreen();
        }
        if (Menu.activeSelf)
        {
            okay = false;
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
            Debug.Log("How Did i get here?");
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
        Debug.Log("Got Here");
        Debug.Log(i);
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
        Debug.Log(stage);
        if (stage == 1)
        {
            Debug.Log("intro");
            speaker.PlayOneShot(curentstart);           
            stage = 2;
        }
        else if (stage == 2)
        {
            Debug.Log("loop");
            speaker.clip = curentloop;
            speaker.Play();
            speaker.loop = true;
            stage = 3;
            Debug.Log("Huh");
        }
        else if (stage == 3 && speaker.loop == false)
        {
            Debug.Log("outro");
            speaker.PlayOneShot(end);
            speaker.loop = false;
        }
        else if (stage == 4 && speaker.loop == false)
        {
            CS_Notifications.Instance.Post(this, "OnVictory");
        }

    }

    public void OnValueChanged()
    {
        CS_WorldManager.Instance.volumeMusic = SoundSlider.value;
        speaker.volume = SoundSlider.value;
        Debug.Log("new volume is " + speaker.volume);
    }
    private void OnSoundV()
    {
        speaker.loop = false;
    }
}