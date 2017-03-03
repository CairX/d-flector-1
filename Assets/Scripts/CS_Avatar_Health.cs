using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_Health : MonoBehaviour {

    public Sprite invincibleSprite;
    public float invincibleTime;
    private float timer;
    private bool changeSprite;

    public Sprite[] healthSprite;
    private SpriteRenderer spriteRenderer;
    private int index;

	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        index = 0;
    }

    void Update ()
    {
        timer -= Time.deltaTime;
        if (changeSprite && timer <= 0)
        {
            spriteRenderer.sprite = healthSprite[index];
            Color color = spriteRenderer.color;
            color.a = 1.0f;
            spriteRenderer.color = color;
            changeSprite = false;
            index++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject cgo = collision.gameObject;

        if (cgo.tag == "Enemy" || (cgo.tag == "Projectile" && cgo.GetComponent<CS_Projectile_Collision>().isEnemy()))
        {
            CS_All_Audio.Instance.AvaterLoseHealth(healthSprite.Length - 1);
            if (timer <= 0 && index < healthSprite.Length - 1)
            {
                timer = invincibleTime;
                spriteRenderer.sprite = invincibleSprite;
                Color color = spriteRenderer.color;
                color.a = 0.8f;
                spriteRenderer.color = color;
                changeSprite = true;
            }
            else if (timer <= 0 && index == healthSprite.Length - 1)
            {
                spriteRenderer.sprite = healthSprite[index];
                changeSprite = false;

                CS_Notifications.Instance.Post(this, "OnGameOver", null);
            }
        }
       
    }
}
