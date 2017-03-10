using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Avatar_CheckPowerUp : MonoBehaviour {

    public GameObject powerUpTwinShield;

    public float powerUpTime;
    private float time = 0;
    private Image timerImage;

    private bool powerUpShieldActivated;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TwinSheildPowerUp")
        {
            RemoveAllPickupExcept(collision.gameObject.tag);
            CS_All_Audio.Instance.PickupSound(1);
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
