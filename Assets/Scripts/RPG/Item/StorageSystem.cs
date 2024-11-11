using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace RPG
{
    /// <summary>
    /// Represent any storage system that can store items and equipments
    /// </summary>
    public class StorageSystem
    {
        private StorageSlot[] content;
        private int size;
        public StorageSystem(int size)
        {
            //Debug.Log("Inv size " + size);
            this.size = size;
            content = new StorageSlot[size];
            for (int i = 0; i < size; i++)
            {
                content[i] = new StorageSlot();
                content[i].setId(i);
            }
        }
        /// <summary>
        /// Return the StorageSlot in a particular index
        /// </summary>
        /// <returns>a StorageSlot object</returns>
        public StorageSlot getSlot(int index)
        {
            return content[index];
        }

        /// <summary>
        /// Return the size of the system
        /// </summary>
        /// <returns>size of storage system</returns>
        public int getSize()
        {
            return size;
        }

        /// <summary>
        /// Get all items that can be use in battle
        /// </summary>
        /// <returns>a list of slot that contains item used in battle</returns>
        public StorageSlot[] getOnlyBattleItem()
        {
            List<StorageSlot> slots = new List<StorageSlot>();
            foreach (StorageSlot s in content)
            {
                if (s.getContainment() is FunctionalItem)
                    slots.Add(s);
            }
            return slots.ToArray();
        }

        /// <summary>
        /// Return the StorageSlot that contains equipments
        /// </summary>
        /// <returns>a list of slot that contains equipment</returns>
        // public StorageSlot[] getOnlyEquipment()
        // {
        //     List<StorageSlot> slots = new List<StorageSlot>();
        //     foreach (StorageSlot s in content)
        //     {
        //         if (s.getContainment() is Equipment)
        //             slots.Add(s);
        //     }
        //     return slots.ToArray();
        // }


        /// <summary>
        /// Insert an item into the system, will spill to next slot if the slot is full
        /// </summary>
        /// <returns>a number representing how many items are left to fill if the storage system is full</returns>
        public int smartInsert(Item item, int qty)
        {
            Debug.Log("insert " + item.name + " qty " + qty);
            int remaining = qty;
            ArrayList sameItem = searchInInventory(item);

            if (sameItem.Count > 0)
            {

                foreach (StorageSlot e in sameItem)
                {
                    if (remaining > 0)
                    {
                        remaining = e.insert(item, remaining);
                        //Debug.Log("insert in" + e.getId());

                    }
                    else
                        break;
                }
            }
            StorageSlot nextEmptySlot;
            while (remaining > 0 && (nextEmptySlot = findFirstEmptySlot()) != null)
                remaining = nextEmptySlot.insert(item, remaining);
            if (remaining > 0)
                Debug.Log("inventory full, " + remaining + " can not insert " + item.name);
            return remaining;
        }

        /*
                public Item smartInsertCrafting(string itemCode, int qty, bool randomEquipQuality)
                {
                    Item item = null;
                    if (itemCode.StartsWith("E"))
                    {
                        int quality = 0;
                        if (randomEquipQuality)
                        {
                            quality = RPGUtil.getRandomIndexFrom(RPGParameter.CRAFT_EQUIP_QUAL_CHANCE, RPGUtil.calculateSum(RPGParameter.CRAFT_EQUIP_QUAL_CHANCE));

                        }
                        item = EquipmentDB.get(itemCode).create(quality);

                    }
                    else if (itemCode.StartsWith("I"))
                    {
                        item = ItemDB.get(itemCode).create();
                    }
                    smartInsert(item, qty);
                    return item;
                }
         */
        /// <summary>
        /// Remove a certain amount of item within the system, will start from the first slot
        /// </summary>
        public void smartDelete(Item item, int qty)
        {
            int remaining = qty;
            ArrayList sameItem = searchInInventory(item);

            if (sameItem.Count > 0)
            {
                foreach (StorageSlot e in sameItem)
                    if (remaining > 0)
                    {
                        if (remaining > e.getQty())
                        {
                            e.remove(e.getQty());
                            remaining -= e.getQty();
                        }
                        else{
                            e.remove(remaining);
                            remaining = 0;
                        }
                    }
                    else
                        break;
            }
        }

        /*
                public void smartDeleteCrafting(string itemCode, int qty)
                {
                    Item item = null;
                    if (itemCode.StartsWith("E"))
                    {
                        item = EquipmentDB.get(itemCode).create(0);
                    }
                    else if (itemCode.StartsWith("I"))
                    {
                        item = ItemDB.get(itemCode).create();
                    }
                    smartDelete(item, qty);
                }
         */

        /// <summary>
        /// Return first empty slot, return null if storage is full
        /// </summary>
        /// <returns>a StorageSlot, null if storage is full</returns>
        private StorageSlot findFirstEmptySlot()
        {
            foreach (StorageSlot slot in content)
            {
                if (slot.isEmpty())
                    return slot;
            }
            return null;
        }

        /// <summary>
        /// Find an item in the storage system
        /// </summary>
        /// <returns>a list of StorageSlot that contains the item</returns>
        public ArrayList searchInInventory(Item item)
        {
            ArrayList sameItem = new ArrayList();
            foreach (StorageSlot e in content)
            {
                if (e.getContainment() != null && item != null && e.getContainment().id == item.id && (e.getContainment().name.Equals(item.name)))
                    sameItem.Add(e);
            }
            return sameItem;
        }
        /* 
                public int searchForTotalQtyInInventory(string itemCode)
                {
                    int qty = 0;
                    int id = Int32.Parse(itemCode.Substring(1)) - 1;
                    if (itemCode.StartsWith("E"))
                    {
                        foreach (StorageSlot e in content)
                        {
                            if (e.getContainment() != null && e.getContainment().ID == id && e.getContainment() is Equipment)
                            {
                                qty = e.getQty();
                                break;
                            }
                        }
                    }
                    else if (itemCode.StartsWith("I"))
                    {
                        foreach (StorageSlot e in content)
                        {
                            if (e.getContainment() != null && e.getContainment().ID == id)
                            {
                                qty = e.getQty();
                                break;
                            }
                        }
                    }

                    return qty;
                }
        */
        /// <summary>
        /// Insert an item in a specific slot
        /// </summary>
        public void insert(Item storable, int qty, int index)
        {
            content[index].insert(storable, qty);
        }

        /// <summary>
        /// Remove an item in a specific slot
        /// </summary>
        public void remove(int index, int qty)
        {
            content[index].remove(qty);
        }

        /// <summary>
        /// Remove all items in a certain slot
        /// </summary>
        public void clear(int index)
        {
            content[index].remove(content[index].getQty());
        }

        /// <summary>
        /// Transfer a certain slot's item into another system
        /// </summary>
        public int transferTo(StorageSystem other, int index)
        {
            int remaining = other.smartInsert(content[index].getContainment(), content[index].getQty());
            if (remaining > 0)
                content[index].insert(content[index].getContainment(), remaining);
            else
                clear(index);
            return remaining;
        }



        // public virtual string onSave()
        // {
        //     string saveStr = "";
        //     foreach (StorageSlot slot in content)
        //         if (saveStr.Length > 0)
        //             saveStr = string.Concat(saveStr, ";", slot.onSave());
        //         else
        //             saveStr = string.Concat(saveStr, slot.onSave());
        //     return saveStr;
        // }

        // public virtual void onLoad(string save)
        // {
        //     //Debug.Log ("inv save str : "  +save);
        //     if(save == null) return;
        //     if (save.Length > 0)
        //     {
        //         string[] saveStr = save.Split(';');
        //         int i = 0;
        //         foreach (StorageSlot slot in content)
        //         {
        //             slot.onLoad(saveStr[i]);
        //             i++;
        //         }
        //     }
        //     else
        //     {// save data is reset
        //         for (int i = 0; i < size; i++)
        //         {
        //             content[i] = new StorageSlot();
        //             content[i].setId(i);
        //         }
        //     }

        // }

        public void onResetSave()
        {
            for (int i = 0; i < size; i++)
            {
                content[i] = new StorageSlot();
                content[i].setId(i);
            }
        }

        /// <summary>
        /// Upgrade storage system to a new size
        /// </summary>
        public void onUpgrade(int newsize)
        {
            this.size = newsize;
            StorageSlot[] newcontent = new StorageSlot[size];
            for (int i = 0; i < size; i++)
            {

                newcontent[i] = new StorageSlot();
                newcontent[i].setId(i);
                if (i < content.Length && content[i].getContainment() != null)
                    newcontent[i].insert(content[i].getContainment(), content[i].getQty());
            }
            content = newcontent;
        }

        /// <summary>
        /// Create virtual inventory of functional item for battle to consume
        /// </summary>
        /// <returns>a list of ItemAndQty object that contains the item and its qty in the slot</returns>
        public List<ItemAndQty> CreateVirtualItemInv()
        {
            List<ItemAndQty> list = new List<ItemAndQty>();
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] != null && content[i].getContainment() != null && content[i].getContainment() is FunctionalItem)
                {
                    bool itemInList = false;
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (list[j].item.id == content[i].getContainment().id)
                        {
                            list[j].qty += content[i].getQty();
                            itemInList = true;
                            break;
                        }
                    }
                    if (!itemInList)
                    {
                        list.Add(new ItemAndQty(content[i].getContainment(), content[i].getQty()));
                    }
                }
            }
            return list.OrderBy(o => o.item.id).ToList(); ;
        }

        /// <summary>
        /// Get the total amount of a certain item in the storage system
        /// </summary>
        /// <returns>The total amount of item in the system</returns>
        // public int searchTotalQtyOfItemInInventory(int itemId){
        //     int totalQty = 0;
        //     foreach (StorageSlot slot in content){
        //         if(slot.getContainment() != null && slot.getContainment().id == itemId && !(slot.getContainment() is Equipment)){
        //             totalQty += slot.getQty();
        //         }
        //     }
        //     return totalQty;
        // }

        /// <summary>
        /// Get the total amount of a certain equipment in the storage system
        /// </summary>
        /// <returns>The total amount of equipment in the system</returns>
        // public int searchTotalQtyOfEquipmentInInventory(int itemId){
        //     int totalQty = 0;
        //     foreach (StorageSlot slot in content){
        //         if(slot.getContainment() != null && slot.getContainment().id == itemId && slot.getContainment() is Equipment){
        //             totalQty += slot.getQty();
        //         }
        //     }
        //     return totalQty;
        // }

        /// <summary>
        /// Search the storage system for equipment that can be reinforce
        /// </summary>
        /// <returns>A list of storage slot the contains equipment that are available for reinforce</returns>
        // public List<StorageSlot> searchEquipmentForReinforce(){
        //     return content.Where(slot=>slot.getContainment() != null && 
        //         slot.getContainment() is Equipment && 
        //         (slot.getContainment() as Equipment).reinforceRecipe != null &&
        //         (slot.getContainment() as Equipment).canReinforce() &&
        //         (Param.unlockAllRecipe || (slot.getContainment() as Equipment).reqLv <= Game.craftSkillManager.reinforcingSkill.lv * 10)
        //     ).ToList();
        // }

        /// <summary>
        /// Search the storage system for equipment that can be enchant
        /// </summary>
        /// <returns>A list of storage slot the contains equipment that are available for enchant</returns>
        // public List<StorageSlot> searchEquipmentForEnchant(){
        //     return content.Where(slot => slot.getContainment() != null && 
        //         slot.getContainment() is Equipment && 
        //         (slot.getContainment() as Equipment).canEnchant() &&
        //         (Param.unlockAllRecipe || (slot.getContainment() as Equipment).reqLv <= Game.craftSkillManager.enchantingSkill.lv * 10)
        //     ).ToList();
        // }
    }
}


