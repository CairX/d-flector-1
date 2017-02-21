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
        if (waves[currenWave].waveDone_X_ == true)
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
        waves[currenWave].enemies_X_--;
    }


}
[System.Serializable]
public class Wave
{
    private float time;
    public bool waveDone_X_ = false;
    public WaveProp[] wave;
    public int enemies_X_;

    public void start()
    {
        enemies_X_ = wave.Length;
    }
    public void Update()
    {
        time += Time.deltaTime;

        for (int index = 0; index < wave.Length; index++)
        {
            if (wave[index].spawnDeley <= time)
            {
                if (wave[index].spawned_X_ == false)
                {
                    wave[index].enemy.transform.position = wave[index].spawnPos;          
                    wave[index].enemy = MonoBehaviour.Instantiate(wave[index].enemy);
                    CS_Enemy_Movement script = wave[index].enemy.GetComponent<CS_Enemy_Movement>();
                    script.path = wave[index].movementPattern;
                    wave[index].spawned_X_ = true;
                }
            }
        }

        if (enemies_X_ == 0)
        {
            waveDone_X_ = true;
        }
    }



}


[System.Serializable]
public class WaveProp
{
    public GameObject enemy;
    public int spawnDeley;
    public Vector2 spawnPos;
    public bool spawned_X_ = false;
    public Transform movementPattern;
}

