using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_WorldManager : CS_Singleton<CS_WorldManager> {
    protected CS_WorldManager() { }

    public CS_StateManager.State state = CS_StateManager.State.StartMenu;
    public int slowdown = 1;
    public int level = 0;
}
