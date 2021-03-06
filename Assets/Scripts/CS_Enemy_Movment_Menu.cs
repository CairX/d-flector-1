﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy_Movment_Menu : MonoBehaviour
{
    public float speed = 1.0f;
    public Transform path;

    private int current = 0;
    private List<Vector3> targets = new List<Vector3>();

    void Start()
    {
        if (path != null)
        {
            for (int i = 0; i < path.childCount; i++)
            {
                targets.Add(transform.position + path.GetChild(i).position);
            }
        }
    }

    void Update()
    {
        if (path != null)
        {
            if (targets.Count == 0)
            {
                return;
            }

            if (transform.position == targets[current])
            {
                current = CS_Utils.Mod(current + 1, targets.Count);
            }

            transform.position = Vector3.MoveTowards(transform.position, targets[current], Time.deltaTime * speed);
        }
    }
}

