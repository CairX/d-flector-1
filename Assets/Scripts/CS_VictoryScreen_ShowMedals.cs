using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_VictoryScreen_ShowMedals : MonoBehaviour
{

    public GameObject levelCompletMedal;
    public GameObject noDamageMedal;
    public GameObject speedRunMedal;

    void Start () {

        if (CS_Medals.Instance.levelComplet == false)
        {
            levelCompletMedal.SetActive(false);
        }
        if (CS_Medals.Instance.noDamage == false)
        {
            noDamageMedal.SetActive(false);
        }
        if (CS_Medals.Instance.speedRun == false)
        {
            speedRunMedal.SetActive(false);
        }
    }
	
	void Update () {
		
	}
}
