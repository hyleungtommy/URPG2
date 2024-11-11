using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

namespace RPG
{
    public class ItemAndQty
    {
        public Item item { get; set; }
        public int qty { get; set; }
        public ItemAndQty(Item item, int qty)
        {
            this.item = item;
            this.qty = qty;
        }

    }
}
