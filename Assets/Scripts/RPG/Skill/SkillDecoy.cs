using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillDecoy", menuName = "Skill/Special/Decoy")]
    public class SkillDecoy : SkillSpecial
    {
        public override bool isAttackSkill()
        {
            return true;
        }

        public string getTypeName()
        {
            return "Decoy";
        }

        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            BattleMessage b = new BattleMessage();
            b.sender = b.receiver = user;
            b.type = BattleMessage.Type.Special;
            b.SkillAnimationName = animation;
            bundle.Add(b);
            //Debug.Log (target.Length);
            foreach (Entity e in target)
                (e as EntityEnemy).Decoy(user as EntityPlayer, (int)modifier);// add 10000 to hate meter
            return bundle;
        }
    }
}

