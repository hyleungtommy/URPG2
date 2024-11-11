using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class ItemBox : BasicBox
{
    // Start is called before the first frame update
    public Image rarityImg;
    protected override void BoxHaveItem(IDisplayable obj)
    {
        base.BoxHaveItem(obj);
        Item item = obj as Item;
        rarityImg.gameObject.SetActive(item.rarity > 0);
        rarityImg.color = Constant.itemRarityColor[(int)item.rarity];
    }

    protected override void BoxIsEmpty()
    {
        base.BoxIsEmpty();
        rarityImg.gameObject.SetActive(false);
    }

}
