using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Collision : MonoBehaviour {

    public enum Owner
    {
        Avatar,
        Enemy
    }

    public int health;

    public Sprite avatarSprite;
    public Color avatarTrailColor;

    public AudioClip netBounce;
    public AudioClip shieldBounce;

    private Owner owner;
    private AudioSource speaker;
    private SpriteRenderer spriteRenderer;

    void Start () {
        owner = Owner.Enemy;
        speaker = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (owner == Owner.Enemy)
        {
            OnEnemyCollisionEnter2D(collision);
        }

        if (collision.gameObject.tag == "Shield")
        {
            speaker.PlayOneShot(shieldBounce);
        }
        else if (collision.gameObject.tag == "net")
        {
            speaker.PlayOneShot(netBounce);
        }
    }

    private void OnEnemyCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Shield")
        {
            owner = Owner.Avatar;
            spriteRenderer.sprite = avatarSprite;
            TrailRenderer trail = transform.GetChild(0).GetComponent<TrailRenderer>();
            trail.startColor = avatarTrailColor;
            trail.endColor = avatarTrailColor;
        }
    }

    public bool isAvatar()
    {
        return (owner == Owner.Avatar);
    }

    public bool isEnemy()
    {
        return (owner == Owner.Enemy);
    }
}
