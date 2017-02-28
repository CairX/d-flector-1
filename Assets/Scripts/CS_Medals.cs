using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Medals : CS_Singleton<CS_Medals> {

    public bool levelComplet = false;
    public bool noDamage = false;
    public bool speedRun = false;

    protected CS_Medals() { }

    public void LevelStart()
    {
        levelComplet = false;
        noDamage = false;
        speedRun = false;
    }

    public void LevelComplet()
    {
        levelComplet = true;
    }
}
