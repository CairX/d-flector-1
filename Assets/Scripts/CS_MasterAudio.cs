using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public AudioClip ProjectileSound1;
    public AudioClip ProjectileSound2;
    public AudioClip ProjectileSound3;
    public AudioClip ProjectileSound4;
    public AudioClip ProjectileSound5;

    public AudioClip enemyDeath1;
    public AudioClip enemyDeath2;
    public AudioClip enemyDeath3;
    public AudioClip enemyDeath4;
    public AudioClip enemyDeath5;

    public AudioClip avtarLesLife1;
    public AudioClip avtarLesLife2;
    public AudioClip avtarLesLife3;

    public AudioClip netSound1;
    public AudioClip netSound2;
    public AudioClip netSound3;
    public AudioClip netSound4;

    public Slider SoundSlider;

    // Use this for initialization
    void Start()
    {

        speaker = GetComponent<AudioSource>();

        //SoundSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        //speaker.volume = SoundSlider.value;
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
    public void PickupSound(int i)
    {
        if (i == 1)
        {
            speaker.PlayOneShot(twinShieldOnPickup);
        }
        else if (i == 2)
        {
            speaker.PlayOneShot(steroidOnPickup);
        }
        else if (i == 3)
        {
            speaker.PlayOneShot(stickyOnPickup);
        }
    }
    public void WinLose(bool i)
    {
        if (i)
        {
            speaker.PlayOneShot(winSound);
        }
        else
        {
            speaker.PlayOneShot(loseSound);
        }
    }
    public void ButtonPress(int i)
    {
        if (i == 1)
        {
            speaker.PlayOneShot(menuOut);
        }
        else if (i == 2)
        {
            speaker.PlayOneShot(menuIn);
        }
        else if (i == 3)
        {
            speaker.PlayOneShot(menuSart);
        }
    }
    //ProjectileSoundStart needs to be spesifick
    public void ProjectileSoundStart(int i)
    {
        if (i == 1)
        {
            speaker.PlayOneShot(ProjectileSound1);
        }
        else if (i == 2)
        {
            speaker.PlayOneShot(ProjectileSound2);
        }
        else if (i == 3)
        {
            speaker.PlayOneShot(ProjectileSound3);
        }
        else if (i == 4)
        {
            speaker.PlayOneShot(ProjectileSound4);
        }
        else if (i == 5)
        {
            speaker.PlayOneShot(ProjectileSound5);
        }
    }
    //DeathToEnemySounds is probebly weird whitout animation
    public void DeathToEnemySounds()
    {

        int randomValue = Random.Range(1, 5);
        if (randomValue == 1)
        {
            speaker.PlayOneShot(enemyDeath1);
        }
        else if (randomValue == 2)
        {
            speaker.PlayOneShot(enemyDeath2);
        }
        else if (randomValue == 3)
        {
            speaker.PlayOneShot(enemyDeath3);
        }
        else if (randomValue == 4)
        {
            speaker.PlayOneShot(enemyDeath4);
        }
        else if (randomValue == 5)
        {
            speaker.PlayOneShot(enemyDeath5);
        }
    }
    public void AvaterLoseHealth(int i)
    {
        if (i == 3)
        {
            speaker.PlayOneShot(avtarLesLife1);
        }
        else if (i == 2)
        {
            speaker.PlayOneShot(avtarLesLife2);
        }
        else if (i == 1)
        {
            speaker.PlayOneShot(avtarLesLife3);
        }
    }
    public void NetBonce()
    {
        int randomValue = Random.Range(1, 4);


        if (randomValue == 1)
        {
            speaker.PlayOneShot(netSound1);
        }
        else if (randomValue == 2)
        {
            speaker.PlayOneShot(netSound2);
        }
        else if (randomValue == 3)
        {
            speaker.PlayOneShot(netSound3);
        }
        else if (randomValue == 4)
        {
            speaker.PlayOneShot(netSound4);
        }

    }
    //dosent sem to work
    public void OnValueChanged()
    {
        speaker.volume = SoundSlider.value;
        Debug.Log("new volume is " + speaker.volume);
    }
}