using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "Resource", menuName = "Item/Resource")]
    public class Resource : Item
    {
        public override Type Type { get { return Type.Resources; } }
        public override int MaxStack { get { return 99; } }
    }
}