using System.Collections;
using System.Collections.Generic;

namespace RPG
{
    /// <summary>
    /// A interface for everything that can be used in battle
    /// </summary>
    public interface IFunctionable
    {
        List<BattleMessage> use(Entity user, Entity[] target);
    }
}