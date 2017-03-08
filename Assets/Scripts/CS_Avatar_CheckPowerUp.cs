using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Avatar_CheckPowerUp : MonoBehaviour {

    public GameObject powerUpTwinShield;
    public GameObject shield;
    public GameObject trail;
    public GameObject stickyBombShield;

    public float powerUpTime;
    private float time = 0;
    private Image timerImage;

    private int go;
    private bool powerUpShieldActivated;
    private bool powerUpSlowMotionActivated;
    private bool powerUpStickyBombActivated;

    private void Start()
    {
        timerImage = GameObject.FindGameObjectWithTag("PowerUpTimer").GetComponent<Image>();
        timerImage.fillAmount = 0.0f;
    }

    private void Update()
    {
        if (powerUpShieldActivated == true)
        {
            UpdateTimer();

            if (time <= 0)
            {
                powerUpTwinShield.SetActive(false);
                powerUpShieldActivated = false;
                time = 0;
            }
        }
        if(powerUpSlowMotionActivated == true)
        {
            UpdateTimer();

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
            UpdateTimer();

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
            RemoveAllPickupExcept(collision.gameObject.tag);
            CS_All_Audio.Instance.PickupSound(1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "SlowMotionPowerUp")
        {
            RemoveAllPickupExcept(collision.gameObject.tag);
            CS_All_Audio.Instance.PickupSound(2);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "StickyBombPowerUp")
        {
            RemoveAllPickupExcept(collision.gameObject.tag);
            CS_All_Audio.Instance.PickupSound(3);
            Destroy(collision.gameObject);
        }

    }
    private void RemoveAllPickupExcept(string p)
    {
        ResetTimer();

        if (p != "TwinSheildPowerUp")
        {
            powerUpShieldActivated = false;
            powerUpTwinShield.SetActive(false);
        }
        else
        {
            powerUpShieldActivated = true;
            powerUpTwinShield.SetActive(true);
        }
        if(p != "SlowMotionPowerUp")
        {
            powerUpSlowMotionActivated = false;
           
            CS_WorldManager.Instance.slowdown = 1;
            shield.SetActive(true);
        }
        else
        {
            powerUpSlowMotionActivated = true;
            CS_WorldManager.Instance.slowdown = 4;
        }
        if (p != "StickyBombPowerUp")
        {
            shield.SetActive(true);
            powerUpStickyBombActivated = false;
            stickyBombShield.SetActive(false);
        }
        else
        {
            shield.SetActive(false);
            stickyBombShield.SetActive(true);
            powerUpStickyBombActivated = true;
        }
    }

    private void UpdateTimer()
    {
        time -= Time.deltaTime;
        timerImage.fillAmount = time / powerUpTime;
    }

    private void ResetTimer()
    {
        time = powerUpTime;
        timerImage.fillAmount = time / powerUpTime;
    }
}
