using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Collision : MonoBehaviour {

    public int health;

    public AudioClip netBounce;
    public AudioClip shieldBounce;

    private AudioSource speaker;

    void Start () {
        speaker = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health--;
        if (health <= 0 || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "shield")
        {
            speaker.PlayOneShot(shieldBounce);
        }
        else if (collision.gameObject.tag == "player")
        {
            speaker.PlayOneShot(shieldBounce);
        }
        else if (collision.gameObject.tag == "net")
        {
            speaker.PlayOneShot(netBounce);
        }
    }
}
