using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillLifeAbsorpation", menuName = "Skill/Special/LifeAbsorpation")]
    public class SkillLifeAbsorpation : SkillSpecial
    {
        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);

            List<BattleMessage> msgs = new List<BattleMessage>();
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
                    int attackPower = (int)((user.stat.ATK * 1 * UnityEngine.Random.Range(0.9f, 1.1f) * modifier) - (opponent.isDefensing ? opponent.stat.DEF * opponent.defenseModifier : opponent.stat.DEF));

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

                        int healValue = (int)(user.stat.HP * attackPower * 0.05);
                        if (user.currhp + healValue > user.stat.HP)
                            healValue = (int)(user.stat.HP - user.currhp);
                        user.currhp += healValue;
                        if (healValue > 0)
                        {
                            BattleMessage healMsg = new BattleMessage();
                            healMsg.sender = user;
                            healMsg.receiver = user;
                            healMsg.type = BattleMessage.Type.Heal;
                            //healMsg.SkillAnimationName = animation;
                            healMsg.AOE = false;
                            healMsg.SkillName = "";
                            healMsg.value = healValue;
                            msgs.Add(healMsg);
                        }
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

