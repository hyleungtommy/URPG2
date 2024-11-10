using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Item/Resource")]
public class Resource : Item
{
    public override Type Type { get { return Type.Resources; } }
}
