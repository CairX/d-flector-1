using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BaseMusic : MonoBehaviour
{

    private AudioSource speaker;

    public AudioClip musik;


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

    public GameObject Menu;

    public float transitionTime;

    private int stage = 1;

    private bool okay = true;

    // Use this for initialization
    private void Start()
    {
        speaker = GetComponent<AudioSource>();
        speaker.volume = 0.2f;
        DiffrentLevel(3);
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
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
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
        if (Menu.activeSelf)
        {
            speaker.Pause();
            okay = false;
        }

        if (vicktory.activeSelf || failure.activeSelf)
        {
            speaker.Stop();
            okay = false;
        }
        else if (okay && speaker.isPlaying == false && speaker.loop == false)
        {
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
    public void MusicVolume(float v)
    {
        speaker.volume = v;
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
    }
    public void StopLoop()
    {
        speaker.loop = false;
    }
    public void Nextstep()
    {
        
        if (stage == 1)
        {
            //speaker.loop = false;
            speaker.PlayOneShot(curentstart);
            
            stage = 2;
        }
        else if (stage == 2)
        {         
            speaker.clip = curentloop;
            speaker.Play();
            speaker.loop = true;
            stage = 3;
            Debug.Log("Yes");
        }
        else if (stage == 3 && speaker.loop == false)
        {
            Debug.Log("Noooo!");
            speaker.PlayOneShot(end);
            speaker.loop = false;
        }
        /*
        else if (stage == 4)
        {
            stage = 5;
        }
        else if (stage == 5)
        {
            stage = 6;
        }
        else if (stage == 6)
        {
            stage = 7;
        }
        else if (stage == 7)
        {
            
        }
        */
    }
}