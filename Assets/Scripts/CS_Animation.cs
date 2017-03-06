using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CS_Animation : MonoBehaviour {

    public Sprite[] tutorial;
    private int frame;

	// Use this for initialization
	void Start () {
        fruitSprites = Resources.LoadAll<Sprite>("fruits");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
