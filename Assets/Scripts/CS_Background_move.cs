using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Background_move : MonoBehaviour
{
    private float startXpos;

    private float startYpos;
    private float startpos = 35;
    private float endpos = -10;
    public float movementX = 0;
    public float movementY = 0;

    public Renderer rend;

    // Use this for initialization
    void Start () {

        rend = GetComponent<Renderer>();
        
        startXpos = this.transform.position.x;
        if (movementY == 0)
        {
            startYpos = this.transform.position.y;

        }
        else
        {
            startYpos = this.transform.position.y * (startpos / startXpos) - (this.transform.position.y * 3);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale <= 0) { return; }

        transform.position = new Vector3(transform.position.x + (movementX / CS_WorldManager.Instance.slowdown),transform.position.y + (movementY / CS_WorldManager.Instance.slowdown), transform.position.z);

        if (this.transform.position.x <= (endpos * -1))
        {
            rend.enabled = true;
        }
        else
        {
            rend.enabled = false;
        }

        if (this.transform.position.x <= endpos)
        {
            transform.position = new Vector3(startpos,startYpos, transform.position.z);
        }
    }
}
