using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MasterAudio : CS_Singleton<CS_MasterAudio>
{
    private AudioSource speaker;
    static public AudioClip shieldhit1;
    static public AudioClip shieldhit2;
    static public AudioClip shieldhit3;
    static public AudioClip shieldhit4;
    static public AudioClip shieldhit5;
    static public AudioClip shieldhit6;
    static public AudioClip shieldhit7;
    static public AudioClip shieldhit8;

    protected CS_MasterAudio() { }

    // Use this for initialization
    void Start()
    {

        speaker = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ProjectileVsShield()
    {
        int randomValue = Random.Range(1, 8);

        if (randomValue == 1)
        {
            //speaker.PlayOneShot(shieldhit1);
        }
        else if (randomValue == 2)
        {
            //speaker.PlayOneShot(shieldhit2);
        }
        else if (randomValue == 3)
        {
            //speaker.PlayOneShot(shieldhit3);
        }
        else if (randomValue == 4)
        {
            //speaker.PlayOneShot(shieldhit4);
        }
        else if (randomValue == 5)
        {
            //speaker.PlayOneShot(shieldhit5);
        }
        else if (randomValue == 6)
        {
            //speaker.PlayOneShot(shieldhit6);
        }
        else if (randomValue == 7)
        {
            //speaker.PlayOneShot(shieldhit7);
        }
        else if (randomValue == 8)
        {
            //speaker.PlayOneShot(shieldhit8);
        }
    }
}