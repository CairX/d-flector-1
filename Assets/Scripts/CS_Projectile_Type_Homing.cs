using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Type_Homing : CS_Projectile_Type
{
    private CS_Projectile_Movement projectileMovement;

    private GameObject target;
    private bool targetVulnerable = true;

    private void Start ()
    {
        projectileMovement = GetComponent<CS_Projectile_Movement>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update ()
    {
        if (target && targetVulnerable)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            projectileMovement.UpdateRotation(CS_Utils.PointToDegree(direction));
        }
    }

    private void OnEnable()
    {
        CS_Notifications.Instance.Register(this, "OnAvatarVulnerable");
        CS_Notifications.Instance.Register(this, "OnAvatarInvulnerable");
    }

    private void OnDisable()
    {
        try
        {
            CS_Notifications.Instance.Unregister(this, "OnAvatarVulnerable");
            CS_Notifications.Instance.Unregister(this, "OnAvatarInvulnerable");
        }
        catch (System.NullReferenceException)
        {
            // Unity destroys objects in random order so there is no way
            // to know if this is run before or after the
            // notification center has been destroyed.
        }
    }

    private void OnAvatarVulnerable()
    {
        if (target && target.transform.tag == "Player")
        {
            targetVulnerable = true;
        }
    }

    private void OnAvatarInvulnerable()
    {
        if (target && target.transform.tag == "Player")
        {
            targetVulnerable = false;
        }
    }

    public override void SpecialCollision(Collision2D collision)
    {
        if (collision.transform.tag == "Shield")
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 1)
            {
                target = enemies[0];
            }
            else if (enemies.Length > 1)
            {
                target = enemies[0];

                foreach (GameObject enemy in enemies)
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, target.transform.position))
                    {
                        target = enemy;
                    }
                }
            }
        }
    }
}
