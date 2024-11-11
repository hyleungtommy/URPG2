using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

namespace RPG
{
    public enum BuyPlace
    {
        None, Shop, Blacksmith
    }
    public enum Rarity
    {
        Ordinary=0, Rare=1, Epic=2, Legendary=3, Artifact=4
    }
    public enum Type
    {
        HPPotion, MPPotion, BuffItem, Resources, GlobalBuff, UPPT, Special
    }
    public abstract class Item : ScriptableObject, IDisplayable
    {
        public int id;
        public string itemName;
        public string desc;
        public Sprite img;
        public BuyPlace buyPlace;
        public int buyPrice;
        public int sellPrice;
        public Rarity rarity;
        public abstract Type Type { get; }
        public abstract int MaxStack {get;}
        public Sprite GetDisplayingImage()
        {
            return img;
        }

    }
}
