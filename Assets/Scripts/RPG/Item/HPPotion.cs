using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPPotion", menuName = "Item/HPPotion")]
public class HPPotion : Item
{
    public int minHealAmount;
    public float healPercentage;
    public override Type Type { get { return Type.HPPotion; } }
}
