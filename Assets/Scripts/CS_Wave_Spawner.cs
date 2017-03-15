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

    void Start() {
        CS_Notifications.Instance.Register(this, "EnemyDead");
        CS_Enemy_Holder.Instance.basic = basic;
        CS_Enemy_Holder.Instance.bolar = bolar;
        CS_Enemy_Holder.Instance.howlar = howlar;
    }

    void Update() {
        if (spawnNextWave == true)
        {
            waves[currenWave].start(this);
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
    [HideInInspector]
    public bool waveDone = false;
    public WaveProp[] amountOfEnemies;
    [HideInInspector]
    public int enemies;
    public Quaternion rotation;
    public Transform parent;

    public void start(MonoBehaviour something)
    {
        rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, 270);
        parent = GameObject.FindWithTag("Playing").transform;

        enemies = amountOfEnemies.Length;
        for (int index = 0; index < amountOfEnemies.Length; index++)
        {
            amountOfEnemies[index].LoadEnemy();
        }

        Dictionary<int, List<WaveProp>> timeSplit = new Dictionary<int, List<WaveProp>>();
        for (int index = 0; index < amountOfEnemies.Length; index++)
        {
            int time = amountOfEnemies[index].spawnDeley;
            if (!timeSplit.ContainsKey(time))
            {
                timeSplit[time] = new List<WaveProp>();
            }
            timeSplit[time].Add(amountOfEnemies[index]);
        }

        foreach (var wave in timeSplit)
        {
            something.StartCoroutine(SpawnWave(wave.Key, wave.Value));
        }
    }

    public IEnumerator SpawnWave(float delay, List<WaveProp> wave)
    {
        yield return new WaitForSeconds(delay);
        foreach (WaveProp item in wave)
        {
            Debug.Log(item);
            GameObject enemy = MonoBehaviour.Instantiate(item.enemyObject, item.startPos, rotation, parent);
            enemy.GetComponent<CS_Enemy_Movement_Init>().target = item.spawnPos;
            enemy.GetComponent<CS_Enemy_Movement>().path = item.movementPattern;
            item.enemyObject = enemy;
            item.spawned = true;
        }
    }

    public void Update()
    {
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

