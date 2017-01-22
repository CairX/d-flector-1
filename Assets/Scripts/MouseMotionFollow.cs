using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMotionFollow : MonoBehaviour {

    void Start () {
        Cursor.visible = false;
    }

    void Update() {
        Vector3 temp = Input.mousePosition;
        Vector3 camera = Camera.main.ScreenToWorldPoint(temp);
        camera.z = this.transform.position.z;
        this.transform.position = camera;
	}
}
