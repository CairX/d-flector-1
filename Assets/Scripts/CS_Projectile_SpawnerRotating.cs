using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_SpawnerRotating : MonoBehaviour
{
    public CS_Projectile_Movement projectile;
    public float projectileSpeed;

    public Transform spawnLocation;
    public Transform rotateObject;
    public float spawnRate = 0.0f;

    public float angle = 0.0f;
    public float angleStep = 45.0f;

    private float timer;
    private Transform parent;

    void Start()
    {
        parent = GameObject.FindWithTag("Playing").transform;
        timer = spawnRate;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            CS_Projectile_Movement i = Instantiate(projectile, spawnLocation.position, spawnLocation.rotation, parent);
            i.angle = angle;
            if (rotateObject)
            {
                rotateObject.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 90);
            }
            i.speed = projectileSpeed;
            timer = spawnRate;

            angle = CS_Utils.Mod(angle + angleStep, 360);
        }
    }
}
