using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Collision : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
