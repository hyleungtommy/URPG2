using UnityEngine;
using System.Collections;

namespace RPG
{
    public class BattleMessage
    {
        public enum Type
        {
            NormalAttack,
            MPAttack,
            Heal,
            MPHeal,
            Miss,
            Critical,
            Waiting,
            Buff,
            Debuff,
            Defense,
            Special
        }

        public float value { set; get; }
        public Type type { set; get; }
        public string SkillAnimationName;
        public bool AOE;
        public Entity sender { set; get; }
        public Entity receiver { set; get; }
        public string SkillName;

        public BattleMessage()
        {
            AOE = false;
        }

        public bool isAttackMessage()
        {
            if (type == Type.Critical || type == Type.MPAttack || type == Type.NormalAttack || type == Type.Miss)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return string.Format("[BattleMessage: value={0}, type={1}, sender={2}, receiver={3}]", value, type, sender, receiver);
        }
    }

}
