using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UseOn
{
    Opponent, Partner, Self
}

namespace RPG
{
    public abstract class Skill : ScriptableObject, IDisplayable, IFunctionable
    {
        public int id;
        public string skillName;
        public string desc;
        public Sprite img;
        public bool isAOE;
        public string animation;
        public int maxSkillLv;
        public float modifierStart;
        public float modifierPerLv;
        public int turn;
        public int cooldown;
        public int reqLvStart;
        public int reqLvPerLv;
        public int reqMpStart;
        public int reqMpPerLv;
        public int priceStart;
        public int pricePerLv;
        public int skillPtsStart;
        public int skillPtsPerLv;
        public int reqMp
        {
            get
            {
                return reqMpStart + (reqMpPerLv * (skillLv - 1));
            }
        }
        public float modifier
        {
            get
            {
                return modifierStart + (modifierPerLv * (skillLv));
            }
        }
        public int price
        {
            get
            {
                return priceStart + (pricePerLv * (skillLv));
            }
        }
        public int skillPts
        {
            get
            {
                return skillPtsStart + (skillPtsPerLv * (skillLv));
            }
        }
        public string fullName
        {
            get
            {
                return name + " " + Util.ToRomanNum(skillLv);
            }
        }
        public string fullNameSkillCenter
        {
            get
            {
                return name + " " + Util.ToRomanNum(skillLv + 1);
            }
        }
        public int reqLv
        {
            get
            {
                return reqLvStart + (reqLvPerLv * (skillLv));
            }
        }
        public int skillLv { get; set; }//need to save
        public int currCooldown { get; set; }

        public Sprite GetDisplayingImage()
        {
            return img;
        }

        public virtual List<BattleMessage> Use(Entity user, Entity[] target)
        {
            user.currmp -= reqMp;// * ModifierFromBuffHelper.getMPUseModifierFromBuff(user) * (user is EntityPlayer ? ModifierFromBuffHelper.getMPModifierFromPassiveSkill(user as EntityPlayer) : 1f);
            currCooldown = cooldown;// - ModifierFromBuffHelper.getCooldownModifier(user);
            if(currCooldown < 0) currCooldown = 0;
            return null;
        }

        protected void applyDebuff(Entity user, Entity target)
        {
            // if (buffList != null)
            // {
            //     foreach (Buff buff in buffList)
            //     {
            //         float applyChance = ((float)user.stat.MATK / (float)target.stat.MDEF * 2f);
            //         int rnd = UnityEngine.Random.Range(0, (int)applyChance);
            //         if (rnd < applyChance)
            //             target.buffState.addBuff(buff);
            //     }
            // }
        }

        protected void applyBuff(Entity target)
        {
            // if (buffList != null)
            // {
            //     foreach (Buff buff in buffList)
            //     {
            //         target.buffState.addBuff(buff);
            //     }
            // }

        }

        public abstract bool isAttackSkill();



    }
}

