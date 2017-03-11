using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Background_Loop : MonoBehaviour
{
    public bool repet;
    public GameObject background;
    CS_Background_Loop code;
    public float movementX = 0;
    public float movementY = 0;

    public Renderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= 0) { return; }


        if (this.transform.position.x <= 12)
        {
            rend.enabled = true;
        }
        else
        {
            rend.enabled = false;
        }

        transform.position = new Vector3(transform.position.x + movementX, transform.position.y + movementY, transform.position.z);

        if (repet == true)
        {
            if (this.transform.position.x <= 0)
            {
           
                //background.transform.position = new Vector3(transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x, 0, this.transform.position.z);
                GameObject back = Instantiate(background);
                back.transform.SetParent(this.transform.parent);
                back.transform.position = new Vector3(transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x, 0, this.transform.position.z);
                code = background.GetComponent<CS_Background_Loop>();
                code.repet = true;
                repet = false;
            }
        }
        if (this.transform.position.x <= -12)
        {
            Destroy(this.gameObject);
        }
    }
}
