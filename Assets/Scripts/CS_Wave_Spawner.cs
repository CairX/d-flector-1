using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CS_Wave_Spawner : MonoBehaviour {

    public Wave[] waves;
    public float timeBetweenWaves;
    private int currenWave = 0;
    private bool spawnNextWave = true;

    void Start () {
        CS_Notifications.Instance.Register(this, "EnemyDead");
        waves[currenWave].start();
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
                CS_Notifications.Instance.Post(this, "OnVictory", null);
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
    private float time;
    [HideInInspector]
    public bool waveDone = false;
    public WaveProp[] wave;
    [HideInInspector]
    public int enemies;

    public void start()
    {
        enemies = wave.Length;
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
                    wave[index].enemy = MonoBehaviour.Instantiate(wave[index].enemy);
                    CS_Enemy_Movement script = wave[index].enemy.GetComponent<CS_Enemy_Movement>();
                    script.path = wave[index].movementPattern;
                    wave[index].spawned = true;
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
    public GameObject enemy;
    public int spawnDeley;
    public Vector2 spawnPos;
    [HideInInspector]
    public bool spawned = false;
    public Transform movementPattern;
}

