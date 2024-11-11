using System;

namespace RPG
{
    public class StorageSlot
    {
        private Item containment;
        private int qty;
        private int id;
        public StorageSlot()
        {
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }

        public Item getContainment()
        {
            return containment;
        }

        public int getQty()
        {
            return qty;
        }

        public bool slotIsFull()
        {
            if (qty == containment.MaxStack)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isEmpty()
        {
            return (qty <= 0);
        }

        public void remove(int qty)
        {
            this.qty -= qty;
            if (this.qty <= 0)
            {
                this.qty = 0;
                containment = null;
            }
        }

        public void clear()
        {
            this.qty = 0;
            containment = null;
        }

        public int insert(Item item, int qty)
        {
            if (this.containment == null)
                this.containment = item;
            this.qty += qty;
            if (this.qty > containment.MaxStack)
            {
                int remaining = this.qty - containment.MaxStack;
                this.qty = containment.MaxStack;
                return remaining;
            }
            else
                return 0;
        }


        public void exchangeBetween(StorageSlot other)
        {
            int cacheQty = other.getQty();
            Item cacheItem = other.getContainment();
            other.containment = containment;
            other.qty = qty;
            containment = cacheItem;
            qty = cacheQty;
        }


        // public string onSave()
        // {
        //     if (containment != null)
        //         return string.Concat(containment.onSave(), ",", qty.ToString());
        //     else
        //         return string.Concat(",0");
        // }

        // public void onLoad(string save)
        // {
        //     string[] saveStr = save.Split(',');
        //     if (saveStr[0].Length > 0)
        //     {
        //         if (saveStr[0].StartsWith("I"))
        //         {
        //             if (Int32.Parse(saveStr[0].Split('|')[1]) < 9999){
        //                 containment = DB.QueryItem(saveStr[0].Split('|')[1]);
        //             }
        //         }
        //         else
        //         {
        //             containment = DB.createEquipmentFormSaveStr(saveStr[0]);
        //         }
        //         qty = Int32.Parse(saveStr[1]);
        //     }
        // }

    }
}


