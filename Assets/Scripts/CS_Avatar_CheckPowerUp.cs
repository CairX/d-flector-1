using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Avatar_CheckPowerUp : MonoBehaviour {

    public GameObject powerUpTwinShield;

    public float powerUpTime;
    private float time = 0;
    private Image timerImage;

    private bool powerupActivated;

    private void Start()
    {
        timerImage = GameObject.FindGameObjectWithTag("PowerUpTimer").GetComponent<Image>();
        timerImage.fillAmount = 0.0f;
    }

    private void Update()
    {
        if (powerupActivated == true)
        {
            UpdateTimer();

            if (time <= 0)
            {
                CS_WorldManager.Instance.powerupExists = false;
                powerupActivated = false;
                powerUpTwinShield.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TwinSheildPowerUp")
        {
            ChangeActivePowerup(collision.gameObject.tag);
            CS_All_Audio.Instance.PickupSound(1);
            Destroy(collision.gameObject);
            ResetTimer();
        }
    }

    private void ChangeActivePowerup(string powerup)
    {
        powerupActivated = true;
        powerUpTwinShield.SetActive((powerup == "TwinSheildPowerUp"));
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
