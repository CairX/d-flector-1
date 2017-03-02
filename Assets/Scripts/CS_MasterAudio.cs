using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MasterAudio : MonoBehaviour
{
    private AudioSource speaker;
    public AudioClip shieldhit1;
    public AudioClip shieldhit2;
    public AudioClip shieldhit3;
    public AudioClip shieldhit4;
    public AudioClip shieldhit5;
    public AudioClip shieldhit6;
    public AudioClip shieldhit7;
    public AudioClip shieldhit8;

    public AudioClip winSound;
    public AudioClip loseSound;

    public AudioClip menuOut;
    public AudioClip menuIn;
    public AudioClip menuSart;

    public AudioClip steroidOnPickup;
    public AudioClip stickyOnPickup;
    public AudioClip twinShieldOnPickup;

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
            speaker.PlayOneShot(shieldhit1);
        }
        else if (randomValue == 2)
        {
            speaker.PlayOneShot(shieldhit2);
        }
        else if (randomValue == 3)
        {
            speaker.PlayOneShot(shieldhit3);
        }
        else if (randomValue == 4)
        {
            speaker.PlayOneShot(shieldhit4);
        }
        else if (randomValue == 5)
        {
            speaker.PlayOneShot(shieldhit5);
        }
        else if (randomValue == 6)
        {
            speaker.PlayOneShot(shieldhit6);
        }
        else if (randomValue == 7)
        {
            speaker.PlayOneShot(shieldhit7);
        }
        else if (randomValue == 8)
        {
            speaker.PlayOneShot(shieldhit8);
        }
    }
}