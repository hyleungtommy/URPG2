using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Party", menuName = "Character/Party")]
public class Party : ScriptableObject
{
    public List<BattleCharacter> battleParty;
    public void Init(){
        foreach(BattleCharacter character in battleParty){
            character.Init();
        }
    }
    /// <summary>
    /// Get all player character that is in the first column (will go to battle)
    /// </summary>
    public BattleCharacter[] GetAllBattleCharacter()
    {
        return battleParty.Where(a => (a.unlocked == true) && a.listPos < 4).ToArray();
    }

    /// <summary>
    /// Get all available characters in the party
    /// </summary>
    /// <returns>A list of available character ordered according to predefined position in selection menu</returns>
    public BattleCharacter[] GetAllUnlockedCharacter()
    {
        BattleCharacter[] list = new BattleCharacter[8];
        for (int i = 0; i < 8; i++)
        {
            if (battleParty[i].unlocked)
            {
                list[battleParty[i].listPos] = battleParty[i];
            }
        }
        return list;
    }

    /// <summary>
    /// Create a list of EntityPlayer from the first column of the selection menu
    /// </summary>
    /// <returns>A list of EntityPlayer object represent the party member that join the battle</returns>
    public EntityPlayer[] CreateBattleParty()
    {

        BattleCharacter[] list = GetAllBattleCharacter();
        List<EntityPlayer> elist = new List<EntityPlayer>();
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] != null)
            {
                elist.Add(list[i].toEntity());
            }
        }
        return elist.ToArray(); ;
    }

    /// <summary>
    /// Unlock a specific character
    /// </summary>
    public void UnlockCharacter(int pos)
    {
        if (pos > 0 && pos < battleParty.Count)
        {
            battleParty[pos].unlocked = true;
            Debug.Log("unlock character " + battleParty[pos].name);
        }

    }

    // public PartySaveData OnSave()
    // {
    //     PartySaveData partySaveData = new PartySaveData();
    //     List<string> characterSaveData = new List<string>();
    //     List<string> equipmentSaveData = new List<string>();
    //     List<string> jobSaveData = new List<string>();
    //     int i = 0;
    //     foreach (BattleCharacter ch in battleParty)
    //     {
    //         string chSave = ch.onsave();
    //         characterSaveData.Add(chSave);
    //         string equipManSave = ch.equipmentManager.onSave();
    //         equipmentSaveData.Add(equipManSave);
    //         i++;
    //     }
    //     i = 0;
    //     foreach (Job j in DB.jobs)
    //     {
    //         string jobSave = j.onSave();
    //         jobSaveData.Add(jobSave);
    //         i++;
    //     }
    //     partySaveData.battleCharacters = characterSaveData.ToArray();
    //     partySaveData.equipmentManagers = equipmentSaveData.ToArray();
    //     partySaveData.jobs = jobSaveData.ToArray();
    //     return partySaveData;
    // }

    // public void OnLoad(PartySaveData partySaveData)
    // {
    //     if (partySaveData.battleCharacters.Length == 8 && partySaveData.equipmentManagers.Length == 8 && partySaveData.jobs.Length == 8)
    //     {
    //         int i = 0;
    //         foreach (BattleCharacter ch in battleParty)
    //         {
    //             string chSave = partySaveData.battleCharacters[i];
    //             ch.onload(chSave);
    //             string equipManSave = partySaveData.equipmentManagers[i];
    //             ch.equipmentManager.onLoad(equipManSave);
    //             i++;
    //         }
    //         i = 0;
    //         foreach (Job j in DB.jobs)
    //         {
    //             string jobSave = partySaveData.jobs[i];
    //             j.onLoad(jobSave);
    //             i++;
    //         }
    //     }
    // }

    public int GetMainCharacterLv()
    {
        return battleParty[0].lv;
    }

    /// <summary>
    /// Testing purpose, unlock all member from start
    /// </summary>
    public void UnlockAllMember()
    {
        foreach (BattleCharacter ch in battleParty)
        {
            ch.unlocked = true;
        }
    }

    /// <summary>
    /// Testing purpose, learn all skill from start
    /// </summary>
    // public void learntAllSkill()
    // {
    //     foreach (BattleCharacter ch in battleParty)
    //     {
    //         foreach (GeneralSkill s in ch.job.skills)
    //         {
    //             s.learn();
    //         }
    //     }
    // }

    /// <summary>
    /// Add upgrade points to the entire party
    /// </summary>
    public void AddUPPTForParty(int amount)
    {
        foreach (BattleCharacter ch in battleParty)
        {
            ch.upptEarned += amount;
        }
    }
}
