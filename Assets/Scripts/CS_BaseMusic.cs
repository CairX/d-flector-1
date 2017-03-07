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

    private AudioClip curentloop;

    public GameObject vicktory;
    public GameObject failure;

    public GameObject Menu;

    // Use this for initialization
    private void Start()
    {
        speaker = GetComponent<AudioSource>();
        speaker.PlayOneShot(musik);
        if (speaker.loop == false)
        {
            speaker.loop = true;
        }
        speaker.volume = 0.2f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (speaker.isPlaying == true)
            {
                speaker.Pause();
            }
            else
            {
                speaker.UnPause();
            }
        }
        if (Menu.activeSelf)
        {
            speaker.Pause();
        }

        if (vicktory.activeSelf || failure.activeSelf)
        {
            speaker.Stop();
        }
    }
    public void PlayMusic()
    {
        speaker.UnPause();
    }
    public void PaseMusic()
    {
        speaker.Pause();
    }
    public void MusicVolume(float v)
    {
        speaker.volume = v;
    }
    public void RestartMusic()
    {
        speaker.UnPause();
    }

    public void diffrentlevel(int i)
    {
        if (i == 1)
        {
            curentloop = loop1;
        }
        if (i == 2)
        {
            curentloop = loop2;
        }
        if (i == 3)
        {
            curentloop = loop3;
        }

    }
}