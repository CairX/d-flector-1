using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_CheckPowerUp : MonoBehaviour {

    public GameObject powerUpTwinShield;
    public int powerUpTime;

    private int time = 0;
    private bool powerUpActivated;

    private void Update()
    {
        if (powerUpActivated == true)
        {
            if (time != 0)
            {
                if (Mathf.RoundToInt(Time.time) == (time + powerUpTime))
                {
                    Debug.Log("end");
                    powerUpTwinShield.SetActive(false);
                    time = 0;
                    powerUpActivated = false;
                } 
            }
            else
            {
                time = Mathf.RoundToInt(Time.time);
                Debug.Log(time);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TwinSheildPowerUp")
        {
            powerUpTwinShield.SetActive(true);
            Destroy(collision.gameObject);
            powerUpActivated = true;
        }
    }
}
