using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    /// <summary>
    /// Contains all public sprite resources
    /// </summary>
    public static class SpriteManager
    {
        public static Sprite basicBoxSelected { set; get; }
        public static Sprite basicBoxNormal { set; get; }
        public static Sprite youWin { set; get; }
        public static Sprite youLose { set; get; }
        public static Sprite[] FloatingTextEnemyDamage { set; get; }
        public static Sprite[] FloatingTextPlayerDamage { set; get; }
        public static Sprite[] FloatingTextMpDamage { set; get; }
        public static Sprite[] FloatingTextHeal { set; get; }
        public static Sprite[] FloatingTextMpHeal { set; get; }
        public static Dictionary<string,Sprite> buffImgs {set; get;}
        public static Sprite enemyGenericImage {set; get;}
        public static Sprite[] dungeonSprite {set; get;}
        public static Sprite[] elementIcons {set; get;}
        static SpriteManager()
        {
            // basicBoxSelected = Resources.Load<Sprite>("UI/Frame/item_frame_selected");
            // basicBoxNormal = Resources.Load<Sprite>("UI/Frame/item_frame");
            // youWin = Resources.Load<Sprite>("UI/Text/you win");
            // youLose = Resources.Load<Sprite>("UI/Text/you lose");
            // enemyGenericImage = Resources.Load<Sprite>("UI/Icon/UI_Icon_Skull");
            //load floating numbers
            Sprite[] allFloatingNumber = Resources.LoadAll<Sprite>("UI/Text/num");
            FloatingTextEnemyDamage = new Sprite[10];
            FloatingTextPlayerDamage = new Sprite[10];
            FloatingTextMpDamage = new Sprite[10];
            FloatingTextHeal = new Sprite[10];
            FloatingTextMpHeal = new Sprite[10];
            for (int i = 0; i < 10; i++)
            {
                FloatingTextEnemyDamage[i] = allFloatingNumber[i];
                FloatingTextPlayerDamage[i] = allFloatingNumber[i + 10];
                FloatingTextMpDamage[i] = allFloatingNumber[i + 20];
                FloatingTextHeal[i] = allFloatingNumber[i + 30];
                FloatingTextMpHeal[i] = allFloatingNumber[i + 40];
            }
            //load buffs
            // buffImgs = new Dictionary<string, Sprite>();
            // foreach(BuffTemplate b in DB.buffs){
            //     buffImgs.Add(b.img,Resources.Load<Sprite>("Buff Icon/" + b.img));
            // }
            //load dungeon
            // dungeonSprite = new Sprite[8];
            // dungeonSprite[0] = Resources.Load<Sprite>("Background/Dungeon/before_boss_room");
            // dungeonSprite[1] = Resources.Load<Sprite>("Background/Dungeon/boss_room");
            // dungeonSprite[2] = Resources.Load<Sprite>("Background/Dungeon/empty_room");
            // dungeonSprite[3] = Resources.Load<Sprite>("Background/Dungeon/hallway");
            // dungeonSprite[4] = Resources.Load<Sprite>("Background/Dungeon/magic_room");
            // dungeonSprite[5] = Resources.Load<Sprite>("Background/Dungeon/monster_room");
            // dungeonSprite[6] = Resources.Load<Sprite>("Background/Dungeon/trap_room");
            // dungeonSprite[7] = Resources.Load<Sprite>("Background/Dungeon/treasure_room");
            //load element icons
            // elementIcons = new Sprite[7];
            // elementIcons[0] = Resources.Load<Sprite>("UI/Icon/element-fire");
            // elementIcons[1] = Resources.Load<Sprite>("UI/Icon/element-ice");
            // elementIcons[2] = Resources.Load<Sprite>("UI/Icon/element-lighting");
            // elementIcons[3] = Resources.Load<Sprite>("UI/Icon/element-earth");
            // elementIcons[4] = Resources.Load<Sprite>("UI/Icon/element-wind");
            // elementIcons[5] = Resources.Load<Sprite>("UI/Icon/element-light");
            // elementIcons[6] = Resources.Load<Sprite>("UI/Icon/element-dark");
        }
    }
}
