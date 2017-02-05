using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public float movementX = 0;
    public float movementY = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(transform.position.x + movementX, transform.position.y + movementY);
    }
}
