using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillHeal", menuName = "Skill/Heal")]
    public class SkillHeal : Skill
    {
        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity e in target)
            {
                float modifier = base.modifier;
                // if(user is EntityPlayer && (user as EntityPlayer).hasPassiveSkill("Faith of God")){
                //     modifier = base.modifier + (user as EntityPlayer).getPassiveSkill("Faith of God").mod;
                // }
                float healAmount = modifier * user.stat.MATK;
                Debug.Log("Heal :" + healAmount + "Mod:" + base.modifier + "user MATK:" + user.stat.MATK);
                if (healAmount > (float)(e.stat.HP - e.currhp))
                {
                    healAmount = (float)(e.stat.HP - e.currhp);
                }
                if (e.currhp > 0)
                    e.currhp += healAmount;
                //Debug.Log("Heal :" + healAmount + "");
                BattleMessage message = new BattleMessage();
                message.SkillAnimationName = animation;
                message.SkillName = name;
                message.sender = user;
                message.receiver = e;
                message.value = healAmount;
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