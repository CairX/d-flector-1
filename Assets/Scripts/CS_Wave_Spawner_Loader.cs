using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Wave_Spawner_Loader : MonoBehaviour
{

    public List<GameObject> levels = new List<GameObject>();

    void Start ()
    {
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }

        levels[CS_WorldManager.Instance.level].SetActive(true);
    }
}
