using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillDefense", menuName = "Skill/Defense")]
    public class SkillDefense : Skill
    {
        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            BattleMessage message = new BattleMessage();
            message.sender = message.receiver = user;
            message.AOE = false;
            message.SkillName = name;
            message.type = BattleMessage.Type.Defense;
            message.value = -1;
            user.isDefensing = true;
            user.defenseModifier = modifier;
            
            if (name.Equals("Healing Defense"))
            {
                user.currhp += user.stat.HP *0.1f;
                if(user.currhp > user.stat.HP){
                    user.currhp = user.stat.HP;
                }
                BattleMessage healMessage = new BattleMessage();
                healMessage.SkillAnimationName = animation;
                healMessage.SkillName = name;
                healMessage.sender = user;
                healMessage.receiver = user;
                healMessage.value = user.stat.HP *0.1f;
                healMessage.type = BattleMessage.Type.Heal;
                bundle.Add(message);
            }
            else if (name.Equals("Reflective Defense"))
            {
                user.reflectiveDefense = true;
            }
            applyBuff(user);
            
            bundle.Add(message);
            return bundle;
        }

        public override bool isAttackSkill()
        {
            return false;
        }

        public string getTypeName()
        {
            return "Defense";
        }
    }
}