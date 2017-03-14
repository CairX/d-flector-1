using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy_Collision : MonoBehaviour {

    public GameObject twinShieldPowerUp;

    private bool dead = false;
    private bool diedThisFrame = false;
    private Rigidbody2D rb;
    private float randomX;
    private float randomY;
    private float randomSpin;
    private float timer = 2;


    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        randomX = Random.Range(2.0f, 3.0f);
        randomY = Random.Range(-2.0f, 2.0f);
        randomSpin = Random.Range(-3f,3f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject cgo = collision.gameObject;
        if (cgo.tag == "Projectile" && cgo.GetComponent<CS_Projectile_Collision>().isAvatar())
        {
            dead = true;
            diedThisFrame = true;
            if (!CS_WorldManager.Instance.powerupExists && Random.Range(0.0f, 1.0f) <= 0.2f)
            {
                Instantiate(twinShieldPowerUp, transform.position, new Quaternion(), transform.parent);
                CS_WorldManager.Instance.powerupExists = true;
            }

            CS_Notifications.Instance.Post(this, "EnemyDead");
        }
        if (cgo.tag == "Shield")
        {
            dead = true;
            diedThisFrame = true;
            CS_Notifications.Instance.Post(this, "EnemyDead");
            CS_Notifications.Instance.Post(this, "OnAvatarDamage");
        }
        if (cgo.tag == "Player")
        {
            dead = true;
            diedThisFrame = true;
            CS_Notifications.Instance.Post(this, "EnemyDead");
        }
    }

    private void Update()
    {
        if (Time.timeScale <= 0) { return; }

        if (dead == true)
        {
            if (diedThisFrame == true)
            {
                CS_Projectile_SpawnerTargetInit targetScript = this.GetComponent<CS_Projectile_SpawnerTargetInit>();
                targetScript.enabled = false;
                PolygonCollider2D collider = this.GetComponent<PolygonCollider2D>();
                collider.enabled = false;
                CS_Enemy_Movement movmentScript = this.GetComponent<CS_Enemy_Movement>();
                movmentScript.enabled = false;
                diedThisFrame = false;
                
            }
            this.transform.Rotate(Vector3.forward, randomSpin, 0);
            rb.velocity = new Vector2(randomX,randomY);
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
