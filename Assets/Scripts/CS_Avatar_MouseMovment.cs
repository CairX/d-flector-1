using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_MouseMovment : MonoBehaviour {

    private Vector2 mousePosition;
    private Vector2 mousePositionChange;

    void Start () {
        Cursor.visible = false;
    }
	
	void Update () {

        mousePositionChange.x = Input.GetAxis("Mouse X");
        mousePositionChange.y = Input.GetAxis("Mouse Y");
        mousePosition.x = this.transform.position.x + mousePositionChange.x;
        mousePosition.y = this.transform.position.y + mousePositionChange.y;
        this.transform.position = mousePosition;
    }
}
