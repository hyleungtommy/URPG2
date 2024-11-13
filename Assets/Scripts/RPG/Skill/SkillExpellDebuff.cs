using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillExpellDebuff", menuName = "Skill/Special/ExpellDebuff")]
    public class SkillExpellDebuff : SkillSpecial
    {
        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity e in target)
            {
                //e.buffState.removeAllDebuff();
                BattleMessage message = new BattleMessage();
                message.SkillAnimationName = animation;
                message.SkillName = name;
                message.sender = user;
                message.receiver = e;
                message.value = 0;
                message.type = BattleMessage.Type.Heal;
                bundle.Add(message);
            }
            return bundle;
        }

        public override bool isAttackSkill()
        {
            return false;
        }
    }
}

