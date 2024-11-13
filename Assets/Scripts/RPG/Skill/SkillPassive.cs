using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillPassive", menuName = "Skill/Passive")]
    public class SkillPassive : Skill
    {
        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            return bundle;
        }

        public override bool isAttackSkill()
        {
            return false;
        }
    }
}