using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_CheckPowerUp : MonoBehaviour {

    public GameObject powerUpTwinShield;
    public GameObject shield;
    public GameObject trail;
    public GameObject stickyBombShield;
    public float powerUpTime;

    private float time = 0;
    private int go;
    private bool powerUpShieldActivated;
    private bool powerUpSlowMotionActivated;
    private bool powerUpStickyBombActivated;

    private void Update()
    {
        if (powerUpShieldActivated == true)
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                powerUpTwinShield.SetActive(false);
                powerUpShieldActivated = false;
                time = 0;
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
                CS_WorldManager.Instance.slowdown = 1;
                time = 0;
            }
        }
        if (powerUpStickyBombActivated == true)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                powerUpStickyBombActivated = false;
                shield.SetActive(true);
                stickyBombShield.SetActive(false);
                CS_Notifications.Instance.Post(this, "Relese");
                time = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TwinSheildPowerUp")
        {
            CS_All_Audio.Instance.PickupSound(1);
            Destroy(collision.gameObject);
            time = powerUpTime;
            powerUpTwinShield.SetActive(true);
            powerUpSlowMotionActivated = false;
            powerUpShieldActivated = true;

            CS_WorldManager.Instance.slowdown = 1;
            powerUpStickyBombActivated = false;
            shield.SetActive(true);
            stickyBombShield.SetActive(false);
        }
        if (collision.gameObject.tag == "SlowMotionPowerUp")
        {
            CS_All_Audio.Instance.PickupSound(2);
            Destroy(collision.gameObject);
            time = powerUpTime;
            powerUpSlowMotionActivated = true;
            powerUpTwinShield.SetActive(false);
            powerUpShieldActivated = false;

            CS_WorldManager.Instance.slowdown = 4;
            powerUpStickyBombActivated = false;
            shield.SetActive(true);
            stickyBombShield.SetActive(false);
        }
        if (collision.gameObject.tag == "StickyBombPowerUp")
        {
            CS_All_Audio.Instance.PickupSound(3);
            Destroy(collision.gameObject);
            time = powerUpTime;
            powerUpSlowMotionActivated = false;
            powerUpTwinShield.SetActive(false);
            powerUpShieldActivated = false;
            powerUpStickyBombActivated = true;
            shield.SetActive(false);
            stickyBombShield.SetActive(true);
            CS_WorldManager.Instance.slowdown = 1;
        }

    }
}
