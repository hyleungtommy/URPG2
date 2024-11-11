using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
using System.Linq;

public class ShopScene : BasicScene
{
    public GameObject infoBox;
    public GameObject scrollViewContent;
    public GameObject boxPrefab;
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
        int noOfBox = shopList.Count;
        Transform contentTran = scrollViewContent.transform;
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;
            box = (GameObject)Instantiate(boxPrefab, contentTran);
            ShopBox boxCtrl = box.GetComponent<ShopBox>();
            boxCtrl.Render(shopList[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.OnClickItem(j));
        }
        buyQty = 1;
        infoBox.gameObject.SetActive(false);
        money.text = Game.money.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickItem(int slotId)
    {
        this.selectedSlotId = slotId;
        infoBox.gameObject.SetActive(true);
        Item item = shopList[slotId];
        itemName.text = item.itemName;
        itemDesc.text = item.Type.ToString() + "\n\n" + item.desc.ToString();
        ChangeBuyQty(0);
    }

    public void ChangeBuyQty(int buyQty)
    {
        this.buyQty += buyQty;
        if (this.buyQty < 1) this.buyQty = 1;
        if (this.buyQty > 99) this.buyQty = 99;
        
        textSum.text = calculateSum().ToString();
        textBuyQty.text = this.buyQty.ToString();
        btnBuy.enabled = canBuy();
    }

    public int calculateSum()
    {
        return shopList[selectedSlotId].buyPrice * buyQty;
    }

    public bool canBuy()
    {
        Debug.Log(calculateSum());
        bool canBuy = true;
        if (calculateSum() > Game.money) canBuy = false;
        Debug.Log(canBuy);
        return canBuy;
    }

    public void OnBuy()
    {
        Game.money -= calculateSum();
        Game.inventory.smartInsert(shopList[selectedSlotId], this.buyQty);
        money.text = Game.money.ToString();
    }




}

