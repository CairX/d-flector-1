using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_CheckPowerUp : MonoBehaviour {

    public GameObject powerUpTwinShield;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TwinSheildPowerUp")
        {
            Debug.Log("hej");
            powerUpTwinShield.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
}
