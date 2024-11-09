using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "Map", menuName = "Map/Map")]
    public class Map : ScriptableObject
    {
        public int id;
        public string mapName;
        public string desc;
        public Sprite bgImg;
        public Sprite battleImg;
        public int reqLv;
        public int maxLv;
        public int maxZone;
        public Enemy[] enemyList;
        public int[] appearChance;
        public Enemy boss;
        public Enemy rareEnemy;
        public int currZone { get; set; }
        public bool unlocked { get; set; }
        public int platinumCoinGain {get; set;}
        public int dungeonId {get; set;}

        /// <summary>
        /// Generate enemies for next zone, according to which zone player is in
        /// </summary>
        /// <returns>a list of up to 5 Entity Enemy</returns>
        public EntityEnemy[] generateEnemy()
        {
            List<EntityEnemy> generatedEnemyList = new List<EntityEnemy>();
            //if player is in last zone, generate boss enemy
            if (Game.currLoc.currZone == Game.currLoc.maxZone)
            {
                generatedEnemyList.Add(boss.toEntity());
            }
            else
            {
                //determine if a rare enemy will be generated
                int rndRareEnemy = UnityEngine.Random.Range(0, 100);
                if (rareEnemy != null && rndRareEnemy <= Param.rareEnemyAppearChance)
                {
                    Game.rareEnemyAppeared = true;
                    generatedEnemyList.Add(rareEnemy.toEntity());
                }
                else
                {
                    int maxEnemy = 5;
                    int enemyNum = UnityEngine.Random.Range(1, maxEnemy);
                    float mapEnemyModifier = (currZone - 1) * 0.02f;
                    if (mapEnemyModifier > Param.maxMapEnemyModifier)
                    {
                        mapEnemyModifier = Param.maxMapEnemyModifier;
                    }

                    for (int i = 0; i < enemyNum; i++)
                    {
                        //int monsterCodexValue = Game.globalBuffManager.GetMonsterCodexValue();
                        int rndEnemyStrength = 0;
                        // if(monsterCodexValue == 1){
                        //     rndEnemyStrength = UnityEngine.Random.Range(3, 5);
                        // }else if(monsterCodexValue == -1){
                        //     rndEnemyStrength = UnityEngine.Random.Range(0, 3);
                        // }else{
                        //     rndEnemyStrength = UnityEngine.Random.Range(0, 5);
                        // }
                        EntityEnemy enemy = enemyList[Util.getRandomIndexFrom(appearChance, 100f)].toEntity(rndEnemyStrength, mapEnemyModifier);
                        generatedEnemyList.Add(enemy);
                    }
                }

            }

            return generatedEnemyList.ToArray();
        }

        /// <summary>
        /// Move to the next zone
        /// </summary>
        public void progressZone()
        {
            if (Game.currentMapMode == Constant.MapModeProgressive)
            {
                if (currZone < maxZone)
                {
                    currZone = currZone + 1;
                    Debug.Log("curr zone=" + currZone);
                }
            }
        }

        /// <summary>
        /// When player reached last zone or leave during progression, move them back to first zone or the last 5th zone
        /// </summary>
        public void resetZoneStatus()
        {
            if (Game.currentMapMode == Constant.MapModeProgressive)
            {
                if (currZone == maxZone)
                {
                    currZone = 1;
                }
                else if (currZone % 5 != 0) // if current area is not 0,5,10,15,... Move back to the last 5th zone
                {
                    currZone = Mathf.FloorToInt((float)currZone / 5f) * 5;
                    if (currZone < 1) currZone = 1;
                }
            }
        }
    }

}
