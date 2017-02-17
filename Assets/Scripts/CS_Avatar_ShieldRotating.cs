using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_ShieldRotating : MonoBehaviour
{

    public float rotatinSpeed;

    void Start()
    {

    }

    void Update()
    {
        if (Time.timeScale <= 0) { return; }

        if (Input.GetMouseButton(1))
        {
            this.transform.Rotate(Vector3.forward, rotatinSpeed / 2, 0);
        }
        else if (Input.GetMouseButton(0))
        {
            this.transform.Rotate(Vector3.forward, rotatinSpeed * 2, 0);
        }
        else
        {
            this.transform.Rotate(Vector3.forward, rotatinSpeed, 0);
        }
    }
        
}