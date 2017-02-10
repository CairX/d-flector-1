using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_MouseMovment : MonoBehaviour {

    private Vector2 mousePosition;
    private Vector2 mousePositionChange;

    public float boundaryStartX;
    public float boundaryStartY;
    public float boundaryStopX;
    public float boundaryStopY;

    void Start () {
        Cursor.visible = false;
    }
	
	void Update () {

        mousePositionChange.x = Input.GetAxis("Mouse X");
        mousePositionChange.y = Input.GetAxis("Mouse Y");
        mousePosition.x = this.transform.position.x + mousePositionChange.x;
        mousePosition.y = this.transform.position.y + mousePositionChange.y;
        Debug.Log(mousePosition.x);
        if (mousePosition.x > boundaryStartX && mousePosition.x < boundaryStopX && mousePosition.y > boundaryStartY && mousePosition.y < boundaryStopY)
        {
            this.transform.position = mousePosition;
        }

    }
}
