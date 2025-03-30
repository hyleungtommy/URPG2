using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillDebuff", menuName = "Skill/Debuff")]
    public class SkillDebuff : Skill
    {
        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity targetEntity in target)
            {
                // foreach (Buff b in buffList)
                // {

                //     float applyChance = ((float)user.stat.MATK / (float)targetEntity.stat.MDEF * 2f) * ModifierFromBuffHelper.getExtraDebuffChanceFromSummonDarkSpirit(user);
                //     int rnd = UnityEngine.Random.Range(0, (int)applyChance);
                //     if (rnd < applyChance)
                //         targetEntity.buffState.addBuff(b);

                //     //Debug.Log (targetEntity.Name + "buff" + b.Type);
                //     BattleMessage message = new BattleMessage();
                //     message.SkillAnimationName = animation;
                //     message.SkillName = name;
                //     message.sender = message.receiver = targetEntity;
                //     //message.value = (int)b.type;
                //     message.type = BattleMessage.Type.Debuff;
                //     message.AOE = aoe;
                //     bundle.Add(message);
                //     //Debug.Log (healAmount);
                // }
            }
            return bundle;
        }

        public override bool isAttackSkill()
        {
            return true;
        }

        public override string GetSkillType()
        {
            return "Debuff Skill";
        }
    }
}