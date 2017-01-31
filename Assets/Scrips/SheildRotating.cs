using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildRotating : MonoBehaviour {

    public int rotatinSpeed;

    void Start () {
		
	}
	
	void Update () {
        this.transform.Rotate(Vector3.forward, rotatinSpeed, 0);
    }
}
