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
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= 0) { return; }

        transform.position = new Vector3(transform.position.x + (movementX / CS_WorldManager.Instance.slowdown), transform.position.y + (movementY / CS_WorldManager.Instance.slowdown), transform.position.z);

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
        if (this.transform.position.x <= -12)
        {
            Destroy(this.gameObject);
        }
    }
}
