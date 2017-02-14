using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Collision : MonoBehaviour {

    public AudioClip netBounce;
    public AudioClip shieldBounce;

    private AudioSource speaker;

    void Start () {
        speaker = this.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
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
