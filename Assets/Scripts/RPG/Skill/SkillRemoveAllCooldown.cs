using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillRemoveAllCooldown", menuName = "Skill/Special/RemoveAllCooldown")]
    public class SkillRemoveAllCooldown : SkillSpecial
    {
        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity e in target)
            {
                EntityPlayer targetPlayer = e as EntityPlayer;
                // foreach(Skill skill in targetPlayer.skillList){
                //     if(!skill.name.Equals(name))
                //         skill.currCooldown = 0;
                // }
                BattleMessage message = new BattleMessage();
                message.SkillAnimationName = animation;
                message.SkillName = name;
                message.sender = user;
                message.receiver = e;
                message.value = 0;
                message.type = BattleMessage.Type.Heal;
                bundle.Add(message);
                applyBuff(e);
            }
            return bundle;
        }

        public override bool isAttackSkill()
        {
            return false;
        }
    }
}