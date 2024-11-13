using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "SkillDeath", menuName = "Skill/Special/Death")]
    public class SkillDeath : SkillSpecial
    {
        public override bool isAttackSkill()
        {
            return true;
        }

        public string getTypeName()
        {
            return "Death";
        }

        public override List<BattleMessage> Use(Entity user, Entity[] target)
        {
            base.Use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            BattleMessage b = new BattleMessage();
            b.sender = user;
            b.receiver = target[0];
            b.AOE = false;
            b.type = BattleMessage.Type.NormalAttack;
            b.SkillAnimationName = animation;
            //Debug.Log (target.Length);
            float deathChance = (user.stat.MATK / target[0].stat.MDEF) * 0.01f;
            //Debug.Log ("deathChance" + deathChance);
            if (deathChance > 0.1f)
                deathChance = 0.1f;
            //deathChance = 1f;
            float rnd = UnityEngine.Random.Range(0f, 1f);
            if (rnd < deathChance)
            {
                //Debug.Log (target [0].Name + " dead");
                b.value = target[0].currhp;
                target[0].currhp = -1;
            }else{
                int attackPower = (int)((user.stat.MATK * 1 * UnityEngine.Random.Range(0.5f, 1.5f) * modifier) - target[0].stat.MDEF);
                if (attackPower <= 0)
                    attackPower = 1;
                target[0].currhp -= attackPower;
                if (target[0].currhp < 0)
                    target[0].currhp = 0;
                b.value = attackPower;
            }
            bundle.Add(b);
            return bundle;
        }
    }
}

