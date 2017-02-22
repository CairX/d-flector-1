using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PowerUp_Rotating : MonoBehaviour {

    public float rotatinSpeed;

	void Update () {
        if (Time.timeScale <= 0) { return; }
        this.transform.Rotate(Vector3.forward, rotatinSpeed, 0);
    }
}
