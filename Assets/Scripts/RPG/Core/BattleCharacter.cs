using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "BattleCharacter", menuName = "Character/BattleCharacter")]
    public class BattleCharacter : ScriptableObject, Displayable
    {
        public int id;
        public string characterName;
        public int startLv;
        public bool unlockAtStart;
        public Sprite bodyImg;
        public Sprite faceImg;
        public Job job;
        public int lv { get; set; } //Need To Save
        public int currexp { get; set; } //Need To Save
        public int expneed { get; set; }
        public int uppt
        {
            get
            {
                return upptEarned - upptSpend;
            }
        }
        public int upptSpend
        {
            get
            {
                return (int)Util.calculateSum(upptAlloc);
            }
        }
        public BattleCharacterStat stat { get; set; }
        public int[] upptAlloc { get; set; } //Need To Save
        public int upptEarned { get; set; } //Need To Save
        public bool unlocked { get; set; } //Need To Save
        public int listPos { get; set; } //Need To Save
        //public EquipmentManager equipmentManager { get; set; } //Need To Save
        public int skillPtsAvailable
        {
            get
            {
                return skillPtsEarned - skillPtsSpent;
            }
        }
        public int skillPtsEarned { get; set; }
        public int skillPtsSpent { get; set; }
        public Sprite GetDisplayingImage(){
            return faceImg;
        }

        public void Init(){
            lv = startLv;
            currexp = 0;
            expneed = Util.getRequireEXPForLevel(lv);
            upptAlloc = new int[]{0,0,0,0,0};
            upptEarned = 0;
            unlocked = unlockAtStart;
            listPos = id - 1;
            skillPtsEarned = 0;
            skillPtsSpent = 0;
            UpdateBattleCharacterStat();
        }

        public void onload(string save)
        {
            string[] data = save.Split('|');
            if (data.Length == 12)
            {
                lv = int.Parse(data[0]);
                currexp = int.Parse(data[1]);
                expneed = Util.getRequireEXPForLevel(lv);
                upptEarned = int.Parse(data[2]);

                upptAlloc[0] = int.Parse(data[3]);
                upptAlloc[1] = int.Parse(data[4]);
                upptAlloc[2] = int.Parse(data[5]);
                upptAlloc[3] = int.Parse(data[6]);
                upptAlloc[4] = int.Parse(data[7]);

                //UpdateBattleCharacterStat();

                unlocked = (int.Parse(data[8]) == 1 ? true : false);
                listPos = int.Parse(data[9]);
                skillPtsEarned = int.Parse(data[10]);
                skillPtsSpent = int.Parse(data[11]);
            }
            else
            {
                Debug.Log("length is not correct :" + save);
            }

        }

        public string onsave()
        {
            int unlock = (unlocked ? 1 : 0);
            return lv + "|" + currexp + "|" + upptEarned + "|" + upptAlloc[0] + "|" + upptAlloc[1] + "|" + upptAlloc[2] + "|" + upptAlloc[3] + "|" + upptAlloc[4] + "|" + unlock + "|" + listPos + "|" + skillPtsEarned + "|" + skillPtsSpent;
        }

        /// <summary>
        /// Create a entity object that is used during battle
        /// </summary>
        /// <returns>An Entity Player object that represent the character</returns>
        public EntityPlayer toEntity()
        {
            BasicStat combinedStat = this.stat.toBasicStat();
            // combinedStat = combinedStat.plus(equipmentManager.getEquipmentStat());
            // combinedStat = combinedStat.multiply(equipmentManager.getEquipmentEnchantmentStat());

            // ElementalTemplate combinedElementalDamage = equipmentManager.GetEquipmentElementalDamage();
            // ElementalTemplate combinedElementalResistance = equipmentManager.GetEquipmentElementalResistance();
            
            
            //Debug.Log(name + " stat:" + combinedStat.ToString() + " equip stat: " + equipmentManager.getEquipmentStat().ToString() + " enchant stat=" + equipmentManager.getEquipmentEnchantmentStat().ToString() + " elementalDamage=" + combinedElementalDamage.ToString() + " elementalResistance=" + combinedElementalResistance.ToString());
            
            //EntityPlayer player = new EntityPlayer(name, combinedStat, faceImg, job.CreateEntityPlayerSkillList());
            EntityPlayer player = new EntityPlayer(name, combinedStat, faceImg);
            // player.elementResistance = combinedElementalResistance;
            // player.elementalDamage = combinedElementalDamage;
            return player;
        }

        /// <summary>
        /// Increase exp of player character
        /// </summary>
        /// <returns>bool to indicate if a player has leveled up</returns>
        public bool assignEXP(int value)
        {
            currexp += value;
            if (currexp >= expneed)
            {
                HandleLevelUp();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Assign upgrade points to player character
        /// </summary>
        /// 
        public void assignUPPT(int[] upptTempAlloc){
            for(int i = 0 ; i < 5 ; i++){
                upptAlloc[i] += upptTempAlloc[i];
            }
            UpdateBattleCharacterStat();
        }

        /// <summary>
        /// Level up character and reset the exp bar
        /// </summary>
        private void HandleLevelUp()
        {
            lv++;
            currexp -= expneed;
            upptEarned += Param.upptGainPerLv;
            skillPtsEarned += Param.skillPtsGainPerLv;
            expneed = Util.getRequireEXPForLevel(lv);
            UpdateBattleCharacterStat();

        }

        /// <summary>
        /// update the 5 stat of battle character
        /// </summary>
        private void UpdateBattleCharacterStat()
        {
            stat = new BattleCharacterStat();
            stat.stamina = upptAlloc[0] + job.levelUpGain[0] * lv + job.startingStat[0];
            stat.strength = upptAlloc[1] + job.levelUpGain[1] * lv + job.startingStat[1];
            stat.mana = upptAlloc[2] + job.levelUpGain[2] * lv + job.startingStat[2];
            stat.agility = upptAlloc[3] + job.levelUpGain[3] * lv + job.startingStat[3];
            stat.dexterity = upptAlloc[4] + job.levelUpGain[4] * lv + job.startingStat[4];
        }
    }
}

