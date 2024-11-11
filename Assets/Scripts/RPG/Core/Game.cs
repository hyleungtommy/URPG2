using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RPG
{
    public static class Game
    {
        public static State state { get; private set; }
        public static Map currLoc;
        public static int difficulty;
        public static bool rareEnemyAppeared;
        public static MapMode currentMapMode;
        public static int money = 10000;
        public static int platinumCoin;
        public static StorageSystem inventory = new StorageSystem(50);
        public enum State
        {
            FreeRoam, Dialog, Battle, OpenUI
        }
        public enum MapMode
        {
            Progression, Explore, Dungeon
        }
        //for better tracking of game state
        public static void ChangeGameState(State _state)
        {
            state = _state;
        }
    }
}