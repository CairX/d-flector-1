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
    }
	
	void Update () {
        if (Time.timeScale <= 0) { return; }

        mousePositionChange.x = Input.GetAxis("Mouse X");
        mousePositionChange.y = Input.GetAxis("Mouse Y");
        mousePosition.x = this.transform.position.x + mousePositionChange.x;
        mousePosition.y = this.transform.position.y + mousePositionChange.y;
        if (mousePosition.x > boundaryStartX && mousePosition.x < boundaryStopX && mousePosition.y > boundaryStartY && mousePosition.y < boundaryStopY)
        {
            this.transform.position = mousePosition;
        }

    }
}
