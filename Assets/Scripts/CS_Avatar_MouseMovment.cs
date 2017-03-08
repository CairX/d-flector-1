using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_MouseMovment : MonoBehaviour {

    public float boundaryStartX;
    public float boundaryStartY;
    public float boundaryStopX;
    public float boundaryStopY;

    void Update () {
        if (Time.timeScale <= 0) { return; }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x + Input.GetAxis("Mouse X"), boundaryStartX, boundaryStopX),
            Mathf.Clamp(transform.position.y + Input.GetAxis("Mouse Y"), boundaryStartY, boundaryStopY)
        );
    }
}
