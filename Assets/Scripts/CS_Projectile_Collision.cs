﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Collision : MonoBehaviour {

    public enum Owner
    {
        Avatar,
        Enemy
    }

    private static float COLLISION_COOLDOWN = 0.08f;

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
        collisionTimer = COLLISION_COOLDOWN;
    }

    private void Update()
    {
        collisionTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Prevent collisions from triggering to frequantly,
        // instantly killing the projectile. This often happens
        // when a player pushed a projectile with the shield.
        // The timer cooldown value is entirely arbitary.
        if (collisionTimer > 0) { return; }
        collisionTimer = COLLISION_COOLDOWN;

        UpdateHealth();
        RouteOnCollisionEnter2D(collision);
    }

    private void UpdateHealth()
    {
        health--;
        text.text = health.ToString();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void RouteOnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                OnPlayerCollisionEnter2D(collision);
                break;
            case "Shield":
                OnShieldCollisionEnter2D(collision);
                break;
            case "net":
                OnNetCollisionEnter2D(collision);
                break;
            default:
                break;
        }
    }

    private void OnNetCollisionEnter2D(Collision2D collision)
    {
        speaker.PlayOneShot(netBounce);
    }

    private void OnPlayerCollisionEnter2D(Collision2D collision)
    {
        if (owner == Owner.Enemy)
        {
            Destroy(gameObject);
        }
    }

    private void OnShieldCollisionEnter2D(Collision2D collision)
    {
        if (owner == Owner.Enemy)
        {
            owner = Owner.Avatar;
            spriteRenderer.sprite = avatarSprite;
            TrailRenderer trail = transform.GetChild(0).GetComponent<TrailRenderer>();
            trail.startColor = avatarTrailColor;
            trail.endColor = avatarTrailColor;
        }

        speaker.PlayOneShot(shieldBounce);
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
