using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        /// <summary>
        /// Create a skill list when creating an entity
        /// </summary>
        /// <returns>A list of skills that the entity player has</returns>
        // public List<Skill> CreateEntityPlayerSkillList()
        // {
        //     List<Skill> slist = new List<Skill>();
        //     for (int i = 0; i < skills.Count; i++)
        //     {
        //         if (skills[i] != null && skills[i].skillLv > 0)
        //         {
        //             Skill s = skills[i].toSkill();
        //             //Debug.Log(name + "," + s);
        //             if (s != null) slist.Add(s);
        //         }
        //     }
        //     return slist;
        // }

        /// <summary>
        /// Get all skills that the character learnt
        /// </summary>
        /// <returns>A list of skills that character learnt</returns>
        // public List<GeneralSkill> GetLearntSkills()
        // {
        //     return skills.Where(a => a != null && a.skillLv > 0).ToList();
        // }

        /// <summary>
        /// Get all skills that the character has not yet learn
        /// </summary>
        /// <returns>A list of skills that character has not yet learn</returns>
        // public List<GeneralSkill> GetLearnableSkills(BattleCharacter ch)
        // {
        //     return skills.Where(a => a != null && a.reqLv <= ch.lv).ToList();
        // }

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