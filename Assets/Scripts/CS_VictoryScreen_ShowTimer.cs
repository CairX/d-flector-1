using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_VictoryScreen_ShowTimer : MonoBehaviour {

    private Text text;
    private Transform textObject;

    private float timer = 0.0f;

    void Start () {
        textObject = transform.GetChild(14);
        text = textObject.GetComponent<Text>();
        timer = CS_Medals.Instance.timer;
    }
	
	void Update () {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
