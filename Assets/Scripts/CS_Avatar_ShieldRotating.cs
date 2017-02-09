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

        this.transform.Rotate(Vector3.forward, rotatinSpeed, 0);
    }
}