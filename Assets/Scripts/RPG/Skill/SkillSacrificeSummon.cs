using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillSacrificeSummon", menuName = "Skill/Special/SacrificeSummon")]
    public class SkillSacrificeSummon : SkillSpecial
    {
        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);
            List<BattleMessage> msgs = new List<BattleMessage>();
            //user.buffState.removeSummons();
            for (int l = 0; l < target.Length; l++)
            {
                Entity opponent = target[l];

                for (int i = 0; i < turn; i++)
                {
                    BattleMessage atkMsg = new BattleMessage();
                    atkMsg.sender = user;
                    atkMsg.receiver = opponent;
                    atkMsg.SkillAnimationName = animation;
                    atkMsg.AOE = isAOE;
                    atkMsg.SkillName = name;
                    int attackPower = (int)(user.stat.MATK * 1 * UnityEngine.Random.Range(0.5f, 1.5f) * modifier //* ModifierFromBuffHelper.getMagicModifierFromSpecialBuff(user, name))
                     - opponent.stat.MDEF);// * ModifierFromBuffHelper.getTargetDefenseModifierFromSpecialBuff(opponent));
                    if (attackPower <= 0)
                        attackPower = 1;
                    float hitChance = user.stat.DEX / (opponent.stat.AGI * 2);
                    if (hitChance > 1.0f)
                        hitChance = 1.0f;
                    else if (hitChance <= 0.1f)
                        hitChance = 0.1f;

                    if (UnityEngine.Random.Range(0.0f, 1.0f) > hitChance)
                        atkMsg.type = BattleMessage.Type.Miss;
                    else
                    {
                        bool crititcal = false;
                        float critChance = Mathf.Log((float)user.stat.DEX / (float)opponent.stat.AGI);
                        if (critChance < 0.05f)
                            critChance = 0.05f;
                        if (UnityEngine.Random.Range(0.0f, 1.0f) <= critChance)
                        {
                            crititcal = true;
                            attackPower *= (int)((user.stat.DEX / opponent.stat.DEX) * 2);
                        }
                        opponent.currhp -= attackPower;
                        if (crititcal)
                            atkMsg.type = BattleMessage.Type.Critical;
                        else
                            atkMsg.type = BattleMessage.Type.NormalAttack;
                        if (opponent.currhp < 0)
                            opponent.currhp = 0;

                        atkMsg.value = attackPower;

                        applyDebuff(user, opponent);

                    }
                    msgs.Add(atkMsg);
                }
            }
            return msgs;
        }

        public override bool isAttackSkill()
        {
            return true;
        }
    }
}

