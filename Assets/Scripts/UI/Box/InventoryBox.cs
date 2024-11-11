using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class InventoryBox : BasicBox
{
    // Start is called before the first frame update
    public Text qty;
    public Image rarityImg;
    public StorageSlot slot { get; set; }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetStorageSlot(StorageSlot slot)
    {
        this.slot = slot;
    }

    public void Render()
    {
        //override basic box render
        base.Render(slot.getContainment());
    }

    protected override void BoxHaveItem(IDisplayable obj)
    {
        base.BoxHaveItem(obj);
        Item item = obj as Item;
        rarityImg.gameObject.SetActive(item.rarity > 0);
        qty.gameObject.SetActive(true);
        rarityImg.color = Constant.itemRarityColor[(int)item.rarity];
        qty.text = slot.getQty().ToString();
    }

    protected override void BoxIsEmpty()
    {
        base.BoxIsEmpty();
        rarityImg.gameObject.SetActive(false);
        qty.gameObject.SetActive(false);
    }

}

