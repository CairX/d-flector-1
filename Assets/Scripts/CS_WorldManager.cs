using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_WorldManager : CS_Singleton<CS_WorldManager> {
    protected CS_WorldManager() { }

    public CS_StateManager.State state = CS_StateManager.State.StartMenu;
    public int level = 0;
    public float volumeMusic = 0.5f;
    public float volumeSoundEfeckt = 0.5f;
    public bool powerupExists = false;
}
