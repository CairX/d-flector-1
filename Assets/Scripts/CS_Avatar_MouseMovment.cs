using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_MouseMovment : MonoBehaviour {

    public float boundaryStartX;
    public float boundaryStartY;
    public float boundaryStopX;
    public float boundaryStopY;

    void Update () {
        if (Time.timeScale <= 0) { Cursor.lockState = CursorLockMode.None; return; }
        Cursor.lockState = CursorLockMode.Locked;

        Vector2 n = new Vector2(transform.position.x + Input.GetAxis("Mouse X"), transform.position.y + Input.GetAxis("Mouse Y"));
        n.x = (n.x > boundaryStartX && n.x < boundaryStopX) ? n.x : transform.position.x;
        n.y = (n.y > boundaryStartY && n.y < boundaryStopY) ? n.y : transform.position.y;

        transform.position = n;
    }
}
