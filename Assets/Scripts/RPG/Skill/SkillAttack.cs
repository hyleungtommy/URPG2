using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillAttack", menuName = "Skill/Attack")]
    public class SkillAttack : Skill
    {
        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);

            List<BattleMessage> msgs = new List<BattleMessage>();
            for (int l = 0; l < target.Length; l++)
            {
                Entity opponent = target[l];
                for (int i = 0; i < turn; i++) //+ ModifierFromBuffHelper.getExtraTurnFromSummonSkeleton(user); i++)
                {
                    BattleMessage atkMsg = new BattleMessage();
                    atkMsg.sender = user;
                    atkMsg.receiver = opponent;
                    atkMsg.SkillAnimationName = animation;
                    atkMsg.AOE = isAOE;
                    atkMsg.SkillName = name;

                    float attackModifier = modifier;// * ModifierFromBuffHelper.getAttackModifierFromSpecialBuff(user, name);
                    // if (user is EntityPlayer && (user as EntityPlayer).hasPassiveSkill("Battle Will") && (user.currhp / user.stat.HP) <= 0.25f)
                    // {
                    //     SkillPassive passiveSkill = (user as EntityPlayer).getPassiveSkill("Battle Will");
                    //     attackModifier = attackModifier + passiveSkill.mod;
                    // }

                    float attackPower = (int)((user.stat.ATK * 1 * UnityEngine.Random.Range(0.9f, 1.1f) * attackModifier) - (opponent.isDefensing ? opponent.stat.DEF * opponent.defenseModifier : opponent.stat.DEF)); //* ModifierFromBuffHelper.getTargetDefenseModifierFromSpecialBuff(opponent));
                    // ElementalTemplate combinedElementDamage = elementDamage.plus(user.elementalDamage);
                    // int elementalAttackPower = Util.CalculateElementalDamage(combinedElementDamage, opponent.elementResistance, attackPower);
                    // attackPower += elementalAttackPower;

                    if (attackPower <= 0)
                        attackPower = 1;
                    float hitChance = user.stat.DEX / (opponent.stat.AGI * 1.4f);
                    if (hitChance > 1.0f)
                        hitChance = 1.0f;
                    else if (hitChance <= 0.1f)
                        hitChance = 0.5f;

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

                        // if (opponent is EntityPlayer && (opponent as EntityPlayer).hasPassiveSkill("Potentiality") && attackPower >= opponent.stat.HP / 2 && attackPower >= opponent.currhp && opponent.currhp > 1f)
                        // {
                        //     attackPower = (int)(opponent.currhp - 1);
                        // }

                        opponent.currhp -= attackPower;
                        if (crititcal)
                            atkMsg.type = BattleMessage.Type.Critical;
                        else
                            atkMsg.type = BattleMessage.Type.NormalAttack;
                        if (opponent.currhp < 0)
                            opponent.currhp = 0;



                        atkMsg.value = attackPower;

                        if (opponent.isDefensing && opponent.reflectiveDefense)
                        {
                            user.currhp -= attackPower * 0.1f;
                            BattleMessage refDefMessage = new BattleMessage();
                            refDefMessage.sender = refDefMessage.receiver = user;
                            refDefMessage.value = attackPower * 0.1f;
                            refDefMessage.type = BattleMessage.Type.NormalAttack;
                            msgs.Add(refDefMessage);
                        }

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

        public override string GetSkillType()
        {
            return "Attack Skill";
        }
    }
}