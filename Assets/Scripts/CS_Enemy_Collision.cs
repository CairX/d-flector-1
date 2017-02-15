using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy_Collision : MonoBehaviour {

    public GameObject twinShieldPowerUp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);
            float randomValue = Random.Range(1.0f, 3.0f);
            if (randomValue >= 1.0f && randomValue <= 2.0f)
            {
                twinShieldPowerUp.transform.position = this.transform.position;
                Instantiate(twinShieldPowerUp);
            }
        }
        if (collision.gameObject.tag == "Shield")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
