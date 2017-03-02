using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_All_Audio : CS_Singleton<CS_MasterAudio>
{
    GameObject theAudio;
    CS_MasterAudio other;
    protected CS_All_Audio() { }
    // Use this for initialization
    void Start () {
        theAudio = GameObject.Find("Audio Source");
        other = (CS_MasterAudio)theAudio.GetComponent(typeof(CS_MasterAudio));
    }
	
	// Update is called once per frame
	void Update () {
    }
    public void ProjectileVsShieldReal()
    {
        other.ProjectileVsShield();
    }
}
