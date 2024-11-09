using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

public static class Game
{
    public static State state;
    public static Map currLoc;
    public static int difficulty;
    public static bool rareEnemyAppeared;
    public static int currentMapMode;
    public static int money;
    public static int platinumCoin;
    public enum State{
        FreeRoam, Dialog, Battle
    }
    

    public static void DisablePlayerControl()
    {
        state = State.Dialog;
    }

    public static void EnablePlayerControl()
    {
        if(state == State.Dialog){
            state = State.FreeRoam;
        }
    }
}