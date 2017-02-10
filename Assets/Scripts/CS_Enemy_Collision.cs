using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy_Collision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);
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
