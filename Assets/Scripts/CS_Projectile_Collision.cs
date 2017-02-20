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

    private TextMesh text;

    private float collisionTimer;

    void Start () {
        owner = Owner.Enemy;
        speaker = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        text = transform.GetChild(1).GetComponent<TextMesh>();
        text.text = health.ToString();
        Debug.Log("INIT HEALTH: " + health);
        collisionTimer = 0.08f;
    }

    private void Update()
    {
        collisionTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionTimer > 0)
        {
            return;
        }

        health--;
        text.text = health.ToString();
        Debug.Log("HEALTH: " + health);
        Debug.Log("ENTER: " + collision.gameObject + " // " + Time.time);

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

        collisionTimer = 0.08f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("EXIT: " + collision.gameObject + " // " + Time.time);
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
        } else if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
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
