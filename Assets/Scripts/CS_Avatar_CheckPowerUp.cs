using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_CheckPowerUp : MonoBehaviour {

    public GameObject powerUpTwinShield;
    public GameObject trail;
    public float powerUpTime;

    private float time = 0;
    private int go;
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
            if (go == 4)
            {
                GameObject shadow = Instantiate(trail);
                shadow.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1.0f);
                go = 0;
            }
            else
            {
                go += 1;
            }
            //shadow.transform.rotation = Quaternion.identity;
            //shadow.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            //shadow.transform.parent = this.transform;

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
        if (collision.gameObject.tag == "SlowMotionPowerUp")
        {
            Destroy(collision.gameObject);
            time = powerUpTime;
            powerUpActivated = true;
        }

    }
}
