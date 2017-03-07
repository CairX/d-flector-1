using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Medals : CS_Singleton<CS_Medals> {

    public bool levelComplet = false;
    public bool noDamage = true;
    public bool speedRun = true;

    protected CS_Medals() { }

    public void LevelStart()
    {
        levelComplet = false;
        noDamage = true;
        speedRun = true;
    }

    public void LevelComplet()
    {
        levelComplet = true;
    }

    public void TookDamage()
    {
        noDamage = false;
    }

    public void Time()
    {
        speedRun = false;
    }
}
