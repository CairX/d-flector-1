using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LookAtTarget : MonoBehaviour
{
    public Transform rotateObject;
    public Transform target;

    void Update()
    {
        float angle = CS_Utils.PointToDegree(target.position - rotateObject.position);
        rotateObject.rotation = Quaternion.Euler(0.0f, 0.0f, angle + 90);
    }
}
