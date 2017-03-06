using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_BaseMusic : MonoBehaviour {

    private AudioSource speaker;

    public AudioClip musik;
    public Slider musicSlider;

    public GameObject vicktory;
    public GameObject failure;

    // Use this for initialization
    void Start() {
        RestartMusic();


        //milk.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        //speaker.volume = milk.value;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(speaker.isPlaying == true)
            {
                speaker.Pause();
            }
            else
            {
                speaker.UnPause();
            }            
        }
        if(vicktory.activeSelf || failure.activeSelf)
        {
            speaker.Pause();
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
        speaker = null;
        speaker = GetComponent<AudioSource>();
        speaker.PlayOneShot(musik);
        if (speaker.loop == false)
        {
            speaker.loop = true;
        }
        speaker.volume = 0.2f;
        speaker.Pause();

    }
}
