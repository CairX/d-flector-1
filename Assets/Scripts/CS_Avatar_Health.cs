using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_Health : MonoBehaviour {

    public SpriteRenderer invincibleSpriteRender;
    public float invincibleTime;
    private float timer;
    private bool changeSprite;

    public SpriteRenderer[] healthSpriteRenders;
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
            UnityEditor.EditorUtility.CopySerialized(healthSpriteRenders[index], spriteRenderer);
            changeSprite = false;
            index++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Enemy")
        {
            if (timer <= 0 && index < healthSpriteRenders.Length - 1)
            {
                timer = invincibleTime;
                UnityEditor.EditorUtility.CopySerialized(invincibleSpriteRender, spriteRenderer);
                changeSprite = true;
            }
            else if (timer <= 0 && index == healthSpriteRenders.Length - 1)
            {
                UnityEditor.EditorUtility.CopySerialized(healthSpriteRenders[index], spriteRenderer);
                changeSprite = false;

                CS_Notifications.Instance.Post(this, "OnGameOver", null);
            }
        }
       
    }
}
