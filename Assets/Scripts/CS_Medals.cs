using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Medals : MonoBehaviour {

    public GameObject levelCompletMedal;
    public GameObject noLevelCompletMedal;

    public GameObject allHealfMedal;
    public GameObject noAllHealfMedal;

    public GameObject timeMedal;
    public GameObject noTimeMedal;

    void Start () {
        CS_Notifications.Instance.Register(this, "LevelComplet");
        CS_Notifications.Instance.Register(this, "LevelStart");

    }
	
	void Update () {
		
	}

    void LevelComplet()
    {
        levelCompletMedal.SetActive(true);
        noLevelCompletMedal.SetActive(false);
    }

    void LevelStart()
    {
        levelCompletMedal.SetActive(false);
        timeMedal.SetActive(false);
        allHealfMedal.SetActive(false);

        noAllHealfMedal.SetActive(true);
        noLevelCompletMedal.SetActive(true);
        noTimeMedal.SetActive(true);
    }
}
