using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BaseMusic : MonoBehaviour {

    private AudioSource speaker;

    public AudioClip musik;

    public GameObject vicktory;
    public GameObject failure;

    // Use this for initialization
    private void Start() {
        speaker = GetComponent<AudioSource>();
        speaker.PlayOneShot(musik);
        if (speaker.loop == false)
        {
            speaker.loop = true;
        }
        speaker.volume = 0.2f;
        //speaker.Pause();
        Debug.Log("Noa");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(speaker.isPlaying == true)
            {
                speaker.Pause();
                Debug.Log("Logan");
            }
            else
            {
                Debug.Log("Smurfs");
                speaker.UnPause();
            }       
        }
        
        if(vicktory.activeSelf || failure.activeSelf)
        {
            Debug.Log("Dave");
            speaker.Stop();
        }
        //Debug.Log(speaker.isPlaying);
    }
    public void PlayMusic()
    {
        Debug.Log("Sven");
        speaker.UnPause();
    }
    public void PaseMusic()
    {
        Debug.Log("Ola");
        speaker.Pause();
    }
    public void MusicVolume(float v)
    {
        Debug.Log("Hilda");
        speaker.volume = v;
    }
    public void RestartMusic()
    {
        Debug.Log("Bjork");
        speaker.UnPause();
    }
}
