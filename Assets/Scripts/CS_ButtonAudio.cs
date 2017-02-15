using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CS_ButtonAudio : MonoBehaviour, IPointerEnterHandler{

    public AudioClip enter;
    public AudioClip exit;
    public AudioClip click;

    private AudioSource speaker;

    // Use this for initialization
    void Start () {
        speaker = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        speaker.PlayOneShot(enter);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        speaker.PlayOneShot(enter);
    }
}
