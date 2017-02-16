using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_CheckPowerUp : MonoBehaviour {

    public GameObject powerUpTwinShield;
    public float powerUpTime;

    private float time = 0;
    private bool powerUpActivated;

    private void Update()
    {
        if (powerUpActivated == true)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                powerUpTwinShield.SetActive(false);
                time = 0;
                powerUpActivated = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TwinSheildPowerUp")
        {
            powerUpTwinShield.SetActive(true);
            Destroy(collision.gameObject);
            time = powerUpTime;
            powerUpActivated = true;
        }
    }
}
