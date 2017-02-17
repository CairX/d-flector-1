using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Avatar_SlowMotion_Trail : MonoBehaviour {

    private SpriteRenderer renderer;
    public Color aColor = new Vector4(1, 1, 1, 1);

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        aColor = new Vector4(1, 1, 1, (aColor.a - 0.2f));
        this.renderer.color = aColor;
        if(aColor.a == 0)
        {
            Destroy(this);
        }
    }
}