using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_Health : MonoBehaviour {

    public Sprite invulnerableSprite;
    public float invulnerableTime;
    private float invulnerableIimer;

    private int invincibleLayer;
    private int originalLayer;

    private bool changeSprite;

    public HealthPoint[] healthPoints;
    private SpriteRenderer spriteRenderer;
    private int index;

    private void OnEnable()
    {
        CS_Notifications.Instance.Register(this, "OnAvatarDamage");
    }

    private void OnDisable()
    {
        try
        {
            CS_Notifications.Instance.Unregister(this, "OnAvatarDamagar");
        }
        catch (System.NullReferenceException)
        {
            // Unity destroys objects in random order so there is no way
            // to know if this is run before or after the
            // notification center has been destroyed.
        }
    }

    private void Start ()
    {
        invincibleLayer = LayerMask.NameToLayer("Void");
        originalLayer = transform.parent.gameObject.layer;

        spriteRenderer = GetComponent<SpriteRenderer>();
        index = 0;
    }

    private void Update ()
    {
        invulnerableIimer -= Time.deltaTime;
        if (changeSprite && invulnerableIimer <= 0)
        {
            BecomeVulnerable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject cgo = collision.gameObject;

        if (cgo.tag == "Enemy")
        {
            foreach (HealthPoint hp in healthPoints)
            {
                hp.hud.SetActive(false);
            }

            spriteRenderer.sprite = healthPoints[healthPoints.Length -1].avatar;
            changeSprite = false;

            CS_Notifications.Instance.Post(this, "OnGameOver");
        }

        if (cgo.tag == "Projectile" && cgo.GetComponent<CS_Projectile_Collision>().isEnemy())
        {
            OnAvatarDamage();
        }
    }

    public void OnAvatarDamage()
    {
        CS_All_Audio.Instance.AvaterLoseHealth(healthPoints.Length - 1);
        CS_Medals.Instance.TookDamage();
        if (invulnerableIimer <= 0 && index < healthPoints.Length - 1)
        {
            BecomeInvulnerable();
        }
        else if (invulnerableIimer <= 0 && index == healthPoints.Length - 1)
        {
            healthPoints[index].hud.SetActive(false);
            spriteRenderer.sprite = healthPoints[index].avatar;
            changeSprite = false;

            CS_Notifications.Instance.Post(this, "OnGameOver");
        }
    }

    private void BecomeInvulnerable()
    {
        invulnerableIimer = invulnerableTime;
        spriteRenderer.sprite = invulnerableSprite;
        Color color = spriteRenderer.color;
        color.a = 0.8f;
        spriteRenderer.color = color;

        SetLayerRecursively(transform.parent.gameObject, invincibleLayer);

        changeSprite = true;
        CS_Notifications.Instance.Post(this, "OnAvatarInvulnerable");
    }

    private void BecomeVulnerable()
    {
        spriteRenderer.sprite = healthPoints[index].avatar;
        Color color = spriteRenderer.color;
        color.a = 1.0f;
        spriteRenderer.color = color;

        healthPoints[index].hud.SetActive(false);

        SetLayerRecursively(transform.parent.gameObject, originalLayer);

        changeSprite = false;
        index++;
        CS_Notifications.Instance.Post(this, "OnAvatarVulnerable");
    }

    private static void SetLayerRecursively(GameObject parent, int layer)
    {
        parent.layer = layer;
        foreach (Transform transform in parent.transform.GetComponentsInChildren<Transform>(true))
        {
            transform.gameObject.layer = layer;
        }
    }
}

[System.Serializable]
public class HealthPoint
{
    public Sprite avatar;
    public GameObject hud;
}
