using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class InventoryScene : BasicScene
{
    public GameObject invContent;
    public GameObject invBoxPrefab;
    public GameObject itemInfoBox;
    public GameObject equipmentInfoBox;
    private StorageSystem storageSystem;
    [SerializeField] Text itemName;
    [SerializeField] Text itemDesc;
    // Start is called before the first frame update
    void Start()
    {
        // if(Game.inventorySceneType.Equals("warehouse")){
        //     storageSystem = Game.town.Warehouse.ItemStorage;
        // }else{
        //     storageSystem = Game.inventory;
        // }
        // header.render();
        itemInfoBox.gameObject.SetActive(false);
        storageSystem = Game.inventory;
        render();
    }

    public void render()
    {
        int noOfBox = storageSystem.getSize();
        Transform contentTran = invContent.transform;
        GameObject invBox;
        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;
            invBox = (GameObject)Instantiate(invBoxPrefab, contentTran);
            InventoryBox invBoxCtrl = invBox.GetComponent<InventoryBox>();
            invBoxCtrl.SetStorageSlot(storageSystem.getSlot(i));
            invBoxCtrl.Render();
            invBox.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
        }
        // itemInfoBox.Hide();
        // equipmentInfoBox.hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickItem(int slotId)
    {
        
        //Debug.Log(Game.inventory.getSlot(slotId).getContainment());
        if (storageSystem.getSlot(slotId) != null && storageSystem.getSlot(slotId).getContainment() != null)
        {
            Item item = storageSystem.getSlot(slotId).getContainment();
            itemInfoBox.gameObject.SetActive(true);
            itemName.text = item.itemName.ToString();
            itemDesc.text = item.Type.ToString() + "\n\n" + item.desc.ToString();
            // if(storageSystem.getSlot(slotId).getContainment() is Equipment){
            //     equipmentInfoBox.setStoageSlot(storageSystem.getSlot(slotId));
            //     equipmentInfoBox.show();
            // }else{
            //     itemInfoBox.setStoageSlot(storageSystem.getSlot(slotId));
            //     itemInfoBox.show();
            // }
        }
    }

}

