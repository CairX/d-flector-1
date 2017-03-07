using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Type_Homing : CS_Projectile_Type
{
    private Rigidbody2D rb;
    private CS_Projectile_Movement projectileMovement;

    private GameObject target;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileMovement = GetComponent<CS_Projectile_Movement>();

        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update ()
    {
        if (target)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            projectileMovement.UpdateRotation(CS_Utils.PointToDegree(direction));
        }
        else
        {
            // TODO: Handle the case where the target dies while traveling.
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
                foreach (GameObject enemy in enemies)
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, target.transform.position))
                    {
                        target = enemy;
                    }
                }
            }
            else
            {
                // TODO: Handle the case where there is no target.
            }
        }
    }
}
