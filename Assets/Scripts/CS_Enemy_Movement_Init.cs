using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy_Movement_Init : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 target;

    private CS_Enemy_Collision enemyCollision;

    private void Start()
    {
        enemyCollision = GetComponent<CS_Enemy_Collision>();
    }

    private void FixedUpdate()
    {
        if (target == null || enemyCollision.dead) { return; }

        if (transform.position == target)
        {
            GetComponent<CS_Enemy_Movement>().enabled = true;
            GetComponent<CS_Projectile_SpawnerTargetInit>().enabled = true;
            enabled = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        }
    }
}
