using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CS_Animation : MonoBehaviour {

    public Sprite[] tutorial;
    
    private int frame = 0;
    private int tutorialPage = 0;
    SpriteRenderer sr;

        // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (frame == 4)
        {
            frame = 0;

            sr.sprite = tutorial[tutorialPage];
            
            
            if(tutorialPage == 12)
            {
                CS_All_Audio.Instance.ProjectileVsShield();
            }
            tutorialPage++;
            if(tutorialPage == tutorial.Length)
            {
                tutorialPage = 0;
            }
        }
        frame++;
	}
}
