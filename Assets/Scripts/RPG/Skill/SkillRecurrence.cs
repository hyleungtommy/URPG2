using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillRecurrence", menuName = "Skill/Special/Recurrence")]
    public class SkillRecurrence : SkillSpecial
    {
        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity e in target)
            {
                if (e.currhp <= 0)
                {
                    float healAmount = e.stat.HP * 0.25f;

                    // if (user is EntityPlayer && (user as EntityPlayer).hasPassiveSkill("Angel Will"))
                    // {
                    //     healAmount = e.stat.HP - e.currhp;
                    //     e.currmp = e.stat.MP;
                    // }

                    e.currhp = healAmount;
                    BattleMessage message = new BattleMessage();
                    message.SkillAnimationName = animation;
                    message.sender = user;
                    message.receiver = e;
                    message.value = healAmount;
                    message.type = BattleMessage.Type.Heal;
                    bundle.Add(message);
                }

            }
            return bundle;
        }

        public override bool isAttackSkill()
        {
            return false;
        }
    }
}
