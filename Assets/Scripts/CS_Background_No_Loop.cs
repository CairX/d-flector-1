using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Background_No_Loop : MonoBehaviour {

    public float movementX = 0;
    public float movementY = 0;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) { return; }

        transform.position = new Vector3(transform.position.x + (movementX / CS_WorldManager.Instance.slowdown), transform.position.y + (movementY / CS_WorldManager.Instance.slowdown), transform.position.z);



        if (this.transform.position.x <= -12)
        {
            Destroy(gameObject);
        }
    }
}
