using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class ShopBox : BasicLargeBox
{
    // Start is called before the first frame update
    public Text textItemName;
    public Text textPrice;
    public Image rarity;

    protected override void BoxHaveItem(IDisplayable obj)
    {
        base.BoxHaveItem(obj);
        Item item = obj as Item;
        textItemName.text = item.itemName;
        textPrice.text = item.buyPrice.ToString();
        rarity.color = Constant.itemRarityColor[(int)item.rarity];
    }

    protected override void BoxIsEmpty()
    {
        base.BoxIsEmpty();
    }

}
