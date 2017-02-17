using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_SlowMotion_Trail : MonoBehaviour
{
    [SerializeField]
    private float fadePerSecond = 2.5f;
    private Transform parent;
    // Use this for initialization
    void Start()
    {
        parent = GameObject.FindWithTag("Playing").transform;
        this.transform.parent = parent;
    }

    // Update is called once per frame
    void Update()
    {
        var material = GetComponent<Renderer>().material;
        var color = material.color;
        material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
        if (color.a == 0)
        {
            Destroy(this);
        }
    }
}