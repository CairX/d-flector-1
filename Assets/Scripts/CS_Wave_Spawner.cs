using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CS_Wave_Spawner : MonoBehaviour {

    public Wave[] waves;
    public float TimeBetweenWaves;
    private int currenWave = 0;
    private bool SpawnNextWave = true;

    void Start () {
        CS_Notifications.Instance.Register(this, "EnemyDead");
        waves[currenWave].start();
    }
	
	void Update () {
        if (SpawnNextWave == true)
        {
            waves[currenWave].start();
            SpawnNextWave = false;         
        }
        if (waves[currenWave].waveDone == true)
        {
            if (currenWave + 1 == waves.Length)
            {
                CS_Notifications.Instance.Post(this, "OnVictory", null);
            }
            else
            {
                TimeBetweenWaves -= Time.deltaTime;
                if (TimeBetweenWaves <= 0)
                {
                    SpawnNextWave = true;
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
        waves[currenWave].enemys--;
    }


}
[System.Serializable]
public class Wave
{
    private float time;
    public bool waveDone = false;
    public WaveProp[] wave;
    public int enemys;

    public void start()
    {
        enemys = wave.Length;
    }
    public void Update()
    {
        time += Time.deltaTime;

        for (int index = 0; index < wave.Length; index++)
        {
            if (wave[index].spawnDeley <= time)
            {
                if (wave[index].spawned == false)
                {
                    wave[index].enemy.transform.position = wave[index].spawnPos;
                    MonoBehaviour.Instantiate(wave[index].enemy);
                    wave[index].spawned = true;
                }
            }
        }

        if (enemys == 0)
        {
            waveDone = true;
        }
    }



}


[System.Serializable]
public class WaveProp
{
    public GameObject enemy;
    public int spawnDeley;
    public Vector2 spawnPos;
    public bool spawned = false;
}

