using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
using System.Linq;

public class ShopScene : ListScene
{
    public GameObject scrollViewContent;
    List<Item> shopList;
    public Text textSum;
    public Text textBuyQty;
    public Text itemName;
    public Text itemDesc;
    public Text money;
    public Button btnBuy;
    public int buyQty { get; set; }
    public int selectedSlotId { get; set; }
    // Start is called before the first frame update
    void Awake()
    {
        shopList = DBManager.Instance.Items.Where(item => item.buyPlace == BuyPlace.Shop).ToList();
        buyQty = 1;
        money.text = Game.money.ToString();
        RenderContentView<ShopBox>(shopList.ConvertAll<IDisplayable>(d => d));
    }

    public override void OnClickInfoBox(IDisplayable item)
    {
        base.OnClickInfoBox(item);
        Item displayItem = item as Item;
        itemName.text = displayItem.itemName;
        itemDesc.text = displayItem.Type.ToString() + "\n\n" + displayItem.desc.ToString();
        ChangeBuyQty(0);
    }

    public void ChangeBuyQty(int buyQty)
    {
        this.buyQty += buyQty;
        if (this.buyQty < 1) this.buyQty = 1;
        if (this.buyQty > 99) this.buyQty = 99;
        
        textSum.text = CalculateSum().ToString();
        textBuyQty.text = this.buyQty.ToString();
        btnBuy.enabled = CanBuy();
    }

    public int CalculateSum()
    {
        return shopList[selectedId].buyPrice * buyQty;
    }

    public bool CanBuy()
    {
        return CalculateSum() > Game.money;
    }

    public void OnBuy()
    {
        Game.money -= CalculateSum();
        Game.inventory.smartInsert(shopList[selectedId], buyQty);
        money.text = Game.money.ToString();
    }

}

