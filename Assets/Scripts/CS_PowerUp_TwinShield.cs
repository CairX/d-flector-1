using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PowerUp_TwinShield : MonoBehaviour {

    public GameObject twinShield;
    public GameObject shield;

	void Start () {
    }
	
	void Update () {
        Vector3 shieldRotation = shield.transform.eulerAngles;
        shieldRotation.z += 180F;
        twinShield.transform.eulerAngles = shieldRotation;
    }
}
