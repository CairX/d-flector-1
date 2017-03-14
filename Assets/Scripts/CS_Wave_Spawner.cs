using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CS_Wave_Spawner : MonoBehaviour {

    public Wave[] waves;
    public float timeBetweenWaves;
    private int currenWave = 0;
    private bool spawnNextWave = true;

    public GameObject basic;
    public GameObject bolar;
    public GameObject howlar;

    void Start () {
        CS_Notifications.Instance.Register(this, "EnemyDead");
        waves[currenWave].start();
        CS_Enemy_Holder.Instance.basic = basic;
        CS_Enemy_Holder.Instance.bolar = bolar;
        CS_Enemy_Holder.Instance.howlar = howlar;
    }

    void Update () {
        if (spawnNextWave == true)
        {
            waves[currenWave].start();
            spawnNextWave = false;         
        }
        if (waves[currenWave].waveDone == true)
        {
            if (currenWave + 1 == waves.Length)
            {
                CS_Notifications.Instance.Post(this, "OnVictory");
            }
            else
            {
                timeBetweenWaves -= Time.deltaTime;
                if (timeBetweenWaves <= 0)
                {
                    spawnNextWave = true;
                    currenWave++;
                }
            }
        }
        else
        {
            waves[currenWave].Update();
        }
	}

    void EnemyDead()
    {
        waves[currenWave].enemies--;
    }

}
[System.Serializable]
public class Wave
{
    public string name = "Wave";
    private float time;
    [HideInInspector]
    public bool waveDone = false;
    public WaveProp[] amountOfEnemies;
    [HideInInspector]
    public int enemies;

    public void start()
    {
        enemies = amountOfEnemies.Length;
        for (int index = 0; index < amountOfEnemies.Length; index++)
        {
            amountOfEnemies[index].LoadEnemy();
        }
    }

    public void Update()
    {
        time += Time.deltaTime;

        for (int index = 0; index < amountOfEnemies.Length; index++)
        {
            if (amountOfEnemies[index].spawnDeley <= time)
            {
                if (amountOfEnemies[index].spawned == false)
                {
                    amountOfEnemies[index].enemyObject.transform.position = amountOfEnemies[index].startPos;          
                    amountOfEnemies[index].enemyObject = MonoBehaviour.Instantiate(amountOfEnemies[index].enemyObject, GameObject.FindWithTag("Playing").transform);
                    amountOfEnemies[index].movmentScript = amountOfEnemies[index].enemyObject.GetComponent<CS_Enemy_Movement>();
                    amountOfEnemies[index].movmentScript.path = amountOfEnemies[index].movementPattern;
                    amountOfEnemies[index].spawned = true;
                }
            }

            if (amountOfEnemies[index].movmentScript != null)
            {
                if (amountOfEnemies[index].movmentScript.inPos == false)
                {
                    if (amountOfEnemies[index].enemyObject.transform.position == amountOfEnemies[index].spawnPos)
                    {
                        amountOfEnemies[index].movmentScript.inPos = true;
                        amountOfEnemies[index].movmentScript.InStartPos();
                        CS_Projectile_SpawnerTargetInit script = amountOfEnemies[index].enemyObject.GetComponent<CS_Projectile_SpawnerTargetInit>();
                        script.enabled = true;
                    }
                    else
                    {
                        if (!amountOfEnemies[index].enemyObject.GetComponent<CS_Enemy_Collision>().dead)
                        {
                            amountOfEnemies[index].enemyObject.transform.position = Vector3.MoveTowards(amountOfEnemies[index].enemyObject.transform.position, amountOfEnemies[index].spawnPos, Time.deltaTime * 1f);
                        }
                    }
                }
            }
        }

        if (enemies == 0)
        {
            waveDone = true;
        }
    }
}


[System.Serializable]
public class WaveProp
{
    public string name = "Enemy";
    public enum EnemyType { BASIC, BOLAR, HOWLAL}
    public enum SpawnPos { A1, A2, A3, A4, A5, B1, B2, B3, B4, B5, C1, C2, C3, C4, C5 }
    public EnemyType enemy;
    [HideInInspector]
    public GameObject enemyObject;
    public int spawnDeley;
    public SpawnPos spawnPosition;
    [HideInInspector]
    public Vector3 spawnPos;
    [HideInInspector]
    public bool spawned = false;
    public Transform movementPattern;
    [HideInInspector]
    public CS_Enemy_Movement movmentScript;
    [HideInInspector]
    public Vector3 startPos = new Vector3(6, 0);

    public void LoadEnemy()
    {
        if (enemy == EnemyType.BASIC)
        {
            enemyObject = CS_Enemy_Holder.Instance.basic;
        }
        else if (enemy == EnemyType.BOLAR)
        {
            enemyObject = CS_Enemy_Holder.Instance.bolar;
        }
        else if (enemy == EnemyType.HOWLAL)
        {
            enemyObject = CS_Enemy_Holder.Instance.howlar;
        }

        if (spawnPosition == SpawnPos.A1)
        {
            spawnPos.x = 1f;
            spawnPos.y = 2.0f;
        }
        else if (spawnPosition == SpawnPos.A2)
        {
            spawnPos.x = 1f;
            spawnPos.y = 1.2f;
        }
        else if (spawnPosition == SpawnPos.A3)
        {
            spawnPos.x = 1f;
            spawnPos.y = 0.4f;
        }
        else if (spawnPosition == SpawnPos.A4)
        {
            spawnPos.x = 1f;
            spawnPos.y = -0.4f;
        }
        else if (spawnPosition == SpawnPos.A5)
        {
            spawnPos.x = 1f;
            spawnPos.y = -1.2f;
        }
        else if (spawnPosition == SpawnPos.B1)
        {
            spawnPos.x = 2.4f;
            spawnPos.y = 2f;
        }
        else if (spawnPosition == SpawnPos.B2)
        {
            spawnPos.x = 2.4f;
            spawnPos.y = 1.2f;
        }
        else if (spawnPosition == SpawnPos.B3)
        {
            spawnPos.x = 2.4f;
            spawnPos.y = 0.4f;
        }
        else if (spawnPosition == SpawnPos.B4)
        {
            spawnPos.x = 2.4f;
            spawnPos.y = -0.4f;
        }
        else if (spawnPosition == SpawnPos.B5)
        {
            spawnPos.x = 2.4f;
            spawnPos.y = -1f;
        }
        else if (spawnPosition == SpawnPos.C1)
        {
            spawnPos.x = 3.8f;
            spawnPos.y = 2f;
        }
        else if (spawnPosition == SpawnPos.C2)
        {
            spawnPos.x = 3.8f;
            spawnPos.y = 1.2f;
        }
        else if (spawnPosition == SpawnPos.C3)
        {
            spawnPos.x = 3.8f;
            spawnPos.y = 0.4f;
        }
        else if (spawnPosition == SpawnPos.C4)
        {
            spawnPos.x = 3.8f;
            spawnPos.y = -0.4f;
        }
        else if (spawnPosition == SpawnPos.C5)
        {
            spawnPos.x = 3.8f;
            spawnPos.y = -1.2f;
        }
    }

}

