using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy_Collision : MonoBehaviour {

    public GameObject twinShieldPowerUp;

    public bool dead { get; private set; }
    private Rigidbody2D rb;
    private float randomSpin;
    public Sprite deathsprite;
    private SpriteRenderer spriteRenderer;

    private Color tmp;

    private void Start()
    {
        dead = false;
        randomSpin = UnityEngine.Random.Range(-3.0f, 3.0f);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Projectile":
                OnProjectileCollisionEnter2D(collision);
                break;
            case "Player":
                OnPlayerCollisionEnter2D(collision);
                break;
            case "Shield":
                OnShieldCollisionEnter2D(collision);
                break;
            default:
                break;
        }
    }

    private void OnProjectileCollisionEnter2D(Collision2D collision)
    {
        CS_Projectile_Collision projectileCollision = collision.gameObject.GetComponent<CS_Projectile_Collision>();
        if (projectileCollision.isAvatar())
        {
            if (!CS_WorldManager.Instance.powerupExists && UnityEngine.Random.Range(0.0f, 1.0f) <= 0.2f)
            {
                Instantiate(twinShieldPowerUp, transform.position, new Quaternion(), transform.parent);
                CS_WorldManager.Instance.powerupExists = true;
            }

            Vector3 dir = collision.contacts[0].point - (Vector2)transform.position;
            dir = -dir.normalized;
            OnDeath(dir);
        }
    }

    private void OnPlayerCollisionEnter2D(Collision2D collision)
    {
        OnDeath(collision.contacts[0].normal);
    }

    private void OnShieldCollisionEnter2D(Collision2D collision)
    {
        CS_Notifications.Instance.Post(this, "OnAvatarDamage");
        OnDeath(collision.contacts[0].normal);
    }

    public void OnDeath(Vector2 direction)
    {
        dead = true;
        CS_Notifications.Instance.Post(this, "EnemyDead");

        GetComponent<CS_Projectile_SpawnerTargetInit>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<CS_Enemy_Movement>().enabled = false;
        
        rb.velocity = (direction * 1.5f);
        spriteRenderer.sprite = deathsprite;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);

    }

    private void Update()
    {
        if (Time.timeScale <= 0) { return; }

        if (dead == true)
        {
            transform.Rotate(Vector3.forward, randomSpin, 0);
        }
    }
}
