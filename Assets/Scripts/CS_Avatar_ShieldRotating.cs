using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_ShieldRotating : MonoBehaviour
{

    public float rotatinSpeed;
    private float timer = 0;
    public Quaternion target;

    void Start()
    {
    }

    void Update()
    {
        if (Time.timeScale <= 0) { return; }
        timer -= Time.deltaTime;

        if (transform.rotation == target)
        {
            Vector3 n = target.eulerAngles;
            n.z -= 45f;
            target.eulerAngles = n;
            timer = 1.0f;
        }
        else if (timer <= 0)
        {
            float step = rotatinSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, step);
        }

        //if (Input.GetMouseButton(1))
        //{
        //    this.transform.Rotate(Vector3.forward, rotatinSpeed / 2, 0);
        //}
        //else if (Input.GetMouseButton(0))
        //{
        //    this.transform.Rotate(Vector3.forward, rotatinSpeed * 2, 0);
        //}
        //else
        //{
        //    this.transform.Rotate(Vector3.forward, rotatinSpeed, 0);
        //}
    }
}
