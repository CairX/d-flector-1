using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy_Collision : MonoBehaviour {

    public GameObject twinShieldPowerUp;
    public GameObject SlowMotionPowerUp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject cgo = collision.gameObject;
        if (cgo.tag == "Projectile" && cgo.GetComponent<CS_Projectile_Collision>().isAvatar())
        {
            Destroy(gameObject);
            float randomValue = Random.Range(1.0f, 6.0f);
            if (randomValue >= 1.0f && randomValue <= 2.0f)
            {
                twinShieldPowerUp.transform.position = transform.position;
                Instantiate(twinShieldPowerUp);
            }
            else if(randomValue >= 3.0f && randomValue <= 4.0f)
            {
                SlowMotionPowerUp.transform.position = transform.position;
                Instantiate(SlowMotionPowerUp);
            }

            CS_Notifications.Instance.Post(this, "EnemyDead");
        }
        if (cgo.tag == "Shield")
        {
            Destroy(gameObject);
            CS_Notifications.Instance.Post(this, "EnemyDead");
        }
        if (cgo.tag == "Player")
        {
            Destroy(gameObject);
            CS_Notifications.Instance.Post(this, "EnemyDead");
        }
    }
}
