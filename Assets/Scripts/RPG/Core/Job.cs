using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace RPG
{
    [CreateAssetMenu(fileName = "Job", menuName = "Character/Job")]
    public class Job : ScriptableObject
    {
        public string JobName;
        public int id;
        public List<int> startingStat;
        public List<int> levelUpGain;
        public List<int> autoAllocateSchedule;
        public List<Skill> skillList;

        /// <summary>
        /// Create a skill list when creating an entity
        /// </summary>
        /// <returns>A list of skills that the entity player has</returns>
        public List<Skill> CreateEntityPlayerSkillList()
        {
            List<Skill> slist = new List<Skill>();
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList[i] != null && skillList[i].skillLv > 0)
                {
                    Skill s = skillList[i];
                    //Debug.Log(name + "," + s);
                    if (s != null) slist.Add(s);
                }
            }
            return slist;
        }

        /// <summary>
        /// Get all skills that the character learnt
        /// </summary>
        /// <returns>A list of skills that character learnt</returns>
        public List<Skill> GetLearntSkills()
        {
            return skillList.Where(a => a != null && a.skillLv > 0).ToList();
        }

        /// <summary>
        /// Get all skills that the character has not yet learn
        /// </summary>
        /// <returns>A list of skills that character has not yet learn</returns>
        public List<Skill> GetLearnableSkills(BattleCharacter ch)
        {
            return skillList.Where(a => a != null && a.reqLv <= ch.lv).ToList();
        }

        // public string onSave()
        // {
        //     List<string> save = new List<string>();
        //     foreach (GeneralSkill s in skills)
        //     {
        //         save.Add(s.onSave());
        //     }
        //     return string.Join(";", save);
        // }

        // public void onLoad(string saveStr)
        // {
        //     string[] data = saveStr.Split(';');
        //     int i = 0;
        //     foreach (GeneralSkill s in skills)
        //     {
        //         if (i < data.Length)
        //         {
        //             s.onLoad(data[i]);
        //         }
        //         i++;
        //     }
        // }
    }
}