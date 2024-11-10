using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MPPotion", menuName = "Item/MPPotion")]
public class MPPotion : Item
{
    public int minHealAmount;
    public float healPercentage;
    public override Type Type { get { return Type.MPPotion; } }
}

