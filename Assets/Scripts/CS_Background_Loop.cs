using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Background_Loop : CS_Background_move
{
    public bool repet;
    public GameObject background;
    CS_Background_Loop code;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= 0) { return; }

        transform.position = new Vector3(transform.position.x + movementX, transform.position.y + movementY, transform.position.z);

        if (repet == true)
        {
            if (this.transform.position.x <= 0)
            {
                //Instantiate(background);
                //background.transform.position = new Vector3(transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x, 0, this.transform.position.z);
                GameObject back = Instantiate(background);
                back.transform.position = new Vector3(transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x, 0, this.transform.position.z);
                code = background.GetComponent<CS_Background_Loop>();
                code.repet = true;
                repet = false;
            }
        }
        else
        {
            Debug.Log(repet);
        }
        if (this.transform.position.x <= -12)
        {
            Destroy(this.gameObject);
        }
    }
}
