using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public abstract class FunctionalItem : Item, IFunctionable
    {
        public abstract List<BattleMessage> Use(Entity user, Entity[] target);
        public abstract bool IsUseOnPlayerParty {get;}
    }
}
