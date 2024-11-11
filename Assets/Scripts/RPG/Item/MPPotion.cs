using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "MPPotion", menuName = "Item/MPPotion")]
    public class MPPotion : FunctionalItem
    {
        public int minHealAmount;
        public float healPercentage;
        public override Type Type { get { return Type.MPPotion; } }
        public override int MaxStack { get { return 10; } }
        public override bool IsUseOnPlayerParty { get { return true; } }
        public override List<BattleMessage> Use(Entity user, Entity[] target){
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity e in target)
            {
                float healAmount = healPercentage * e.stat.HP;

                if (healAmount < minHealAmount)
                    healAmount = minHealAmount;
                if (healAmount > (float)(e.stat.MP - e.currmp))
                {
                    healAmount = (float)(e.stat.MP - e.currmp);
                }
                if (e.currmp > 0)
                    e.currmp += healAmount;

                BattleMessage message = new BattleMessage();
                message.sender = user;
                message.receiver = e;
                message.value = healAmount;
                message.type = BattleMessage.Type.MPHeal;
                bundle.Add(message);
                //Debug.Log (healAmount);
            }
            return bundle;
        }
    }
}

