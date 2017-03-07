using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_Health : MonoBehaviour {

    public Sprite invincibleSprite;
    public float invincibleTime;
    private float timer;
    private bool changeSprite;

    public HealthPoint[] healthPoints;
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
            spriteRenderer.sprite = healthPoints[index].avatar;
            Color color = spriteRenderer.color;
            color.a = 1.0f;
            spriteRenderer.color = color;

            healthPoints[index].hud.SetActive(false);

            changeSprite = false;
            index++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject cgo = collision.gameObject;

        if (cgo.tag == "Enemy" || (cgo.tag == "Projectile" && cgo.GetComponent<CS_Projectile_Collision>().isEnemy()))
        {
            CS_All_Audio.Instance.AvaterLoseHealth(healthPoints.Length - 1);
            CS_Medals.Instance.TookDamage();
            if (timer <= 0 && index < healthPoints.Length - 1)
            {
                timer = invincibleTime;
                spriteRenderer.sprite = invincibleSprite;
                Color color = spriteRenderer.color;
                color.a = 0.8f;
                spriteRenderer.color = color;
                changeSprite = true;
            }
            else if (timer <= 0 && index == healthPoints.Length - 1)
            {
                healthPoints[index].hud.SetActive(false);
                spriteRenderer.sprite = healthPoints[index].avatar;
                changeSprite = false;
                
                CS_Notifications.Instance.Post(this, "OnGameOver");
            }
        }
    }
}

[System.Serializable]
public class HealthPoint
{
    public Sprite avatar;
    public GameObject hud;
}
