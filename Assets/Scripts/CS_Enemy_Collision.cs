using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy_Collision : MonoBehaviour {

    public GameObject twinShieldPowerUp;

    public bool dead { get; private set; }
    private Rigidbody2D rb;
    private float timer = 10.0f;
    private float randomSpin;
    public Sprite deathsprite;
    private SpriteRenderer spriteRenderer;

    private Color tmp;

    private void Start()
    {
        dead = false;
        randomSpin = Random.Range(-3f, 3f);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject cgo = collision.gameObject;
        if (cgo.tag == "Projectile")
        {
            CS_Projectile_Collision projectileCollision = cgo.GetComponent<CS_Projectile_Collision>();
            if (projectileCollision.isAvatar())
            {
                if (!CS_WorldManager.Instance.powerupExists && Random.Range(0.0f, 1.0f) <= 0.2f)
                {
                    Instantiate(twinShieldPowerUp, transform.position, new Quaternion(), transform.parent);
                    CS_WorldManager.Instance.powerupExists = true;
                }

                Vector3 dir = collision.contacts[0].point - (Vector2)transform.position;
                dir = -dir.normalized;
                OnDeath(dir);
            }
        }
        else if (cgo.tag == "Shield")
        {
            CS_Notifications.Instance.Post(this, "OnAvatarDamage");
            OnDeath(collision.contacts[0].normal);
        }
        else if (cgo.tag == "Player")
        {
            OnDeath(collision.contacts[0].normal);
        }
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

    private void Update()
    {
       
        if (Time.timeScale <= 0) { return; }

        if (dead == true)
        {
            timer -= Time.deltaTime;
            transform.Rotate(Vector3.forward, randomSpin, 0);

            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
