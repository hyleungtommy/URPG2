using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class EntityPlayer : Entity
    {
        public BattleScene scene { set; get; }
        //public List<Skill> skillList { set; get; }
        // public EntityPlayer(string name, BasicStat stat, Sprite img, List<Skill> skillList) : base(name, stat, img)
        // {
        //     Debug.Log(name + "," + skillList.Count);
        //     this.skillList = skillList;
        // }

        public EntityPlayer(string name, BasicStat stat, Sprite img) : base(name, stat, img)
        {
        }

        public override void TakeAction(IFunctionable functionable)
        {
            List<BattleMessage> bundle = functionable.Use(this, opponent);
            curratb = 0;
            scene.CreateBattleAnimation(bundle);
        }

        public override void PassRound()
        {
            base.PassRound();
            // foreach (Skill s in skillList)
            // {
            //     if (s != null && s.currCooldown > 0) s.currCooldown -= 1;
            // }
        }

        // public bool hasPassiveSkill(string name){
        //     return skillList.Exists(skill=>skill.name.StartsWith(name));
        // }

        // public SkillPassive getPassiveSkill(string name){
        //     if(hasPassiveSkill(name)){
        //         return skillList.Find(skill=>skill.name.StartsWith(name)) as SkillPassive;
        //     }
        //     return null;
        // }
    }
}