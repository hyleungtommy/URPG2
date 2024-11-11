using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG{
    [CreateAssetMenu(fileName = "HPPotion", menuName = "Item/HPPotion")]
    public class HPPotion : FunctionalItem
    {
        public int minHealAmount;
        public float healPercentage;
        public override Type Type { get { return Type.HPPotion; } }
        public override int MaxStack { get { return 10; } }
        public override bool IsUseOnPlayerParty { get { return true; } }
        public override List<BattleMessage> Use(Entity user, Entity[] target){
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity e in target)
            {
                float healAmount = healPercentage * e.stat.HP;

                if (healAmount < minHealAmount)
                    healAmount = minHealAmount;
                if (healAmount > (float)(e.stat.HP - e.currhp))
                {
                    healAmount = (float)(e.stat.HP - e.currhp);
                }
                //Debug.Log(healAmount + "," + healPercentage);
                if (e.currhp > 0)
                    e.currhp += healAmount;

                BattleMessage message = new BattleMessage();
                message.sender = user;
                message.receiver = e;
                message.value = healAmount;
                message.type = BattleMessage.Type.Heal;
                bundle.Add(message);
                //Debug.Log (healAmount);
            }
            return bundle;
        }
    }
}

