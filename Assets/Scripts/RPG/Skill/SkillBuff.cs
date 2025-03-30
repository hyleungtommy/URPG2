using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillBuff", menuName = "Skill/Buff")]
    public class SkillBuff : Skill
    {
        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();

            foreach (Entity targetEntity in target)
            {
                if (targetEntity.currhp > 0)
                {
                    // foreach (Buff b in buffList)
                    // {
                    //     if ((user as EntityPlayer).havePassiveSkill("Faith of God"))
                    //     {
                    //        b.Rounds += (int)(user as EntityPlayer).getPassiveSkill("Faith of God").Mod;
                    //     }
                    //     targetEntity.buffState.addBuff(b);
                    //     Debug.Log(targetEntity.name + "buff" + b.type + "round" + b.rounds);
                    //     BattleMessage message = new BattleMessage();
                    //     message.SkillAnimationName = animation;
                    //     message.SkillName = name;
                    //     message.sender = message.receiver = targetEntity;
                    //     message.type = BattleMessage.Type.Buff;
                    //     message.AOE = isAOE;
                    //     bundle.Add(message);
                    // }
                }
            }
            return bundle;
        }

        public override bool isAttackSkill()
        {
            return true;
        }

        public override string GetSkillType()
        {
            return "Buff Skill";
        }
    }
}