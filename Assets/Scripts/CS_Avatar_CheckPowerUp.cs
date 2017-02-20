using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_CheckPowerUp : MonoBehaviour {

    public GameObject powerUpTwinShield;
    public GameObject trail;
    public float powerUpTime;

    private float time = 0;
    private int go;
    private bool powerUpShieldActivated;
    private bool powerUpSlowMotionActivated;
    private bool powerUpStickyActivated;

    private void Update()
    {
        if (powerUpShieldActivated == true)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                powerUpTwinShield.SetActive(false);
                time = 0;
                powerUpShieldActivated = false;
            }
        }
        if(powerUpSlowMotionActivated == true)
        {
            time -= Time.deltaTime;
            
            if (go == 8)
            {
                GameObject shadow = Instantiate(trail);
                shadow.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1.0f);
                go = 0;
            }
            else
            {
                go += 1;
            }
            if (time <= 0)
            {
                powerUpSlowMotionActivated = false;
                time = 0;
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
            powerUpSlowMotionActivated = false;
            powerUpShieldActivated = true;
        }
        if (collision.gameObject.tag == "SlowMotionPowerUp")
        {
            Destroy(collision.gameObject);
            time = powerUpTime;
            powerUpSlowMotionActivated = true;
            powerUpTwinShield.SetActive(false);
            powerUpShieldActivated = false;
        }

    }
}
