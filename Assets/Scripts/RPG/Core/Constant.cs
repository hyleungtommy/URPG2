using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public static class Constant
    {
        public const int MapModeProgressive = 0;
        public const int MapModeExplore = 1;
        public const int MapModeDungeon = 3;
        public const string topBarSelectAnEnemy = "Select an enemy";
        public const string topBarSelectAnItem = "Select an item to use";
        public const string topBarSelectASkill = "Select a skill to use";
        public const string topBarSelectPlayer = "Select a player";
        public enum buyPlace
        {
            shop, none, blacksmith
        }
        public static Color32[] itemRarityColor = {
            new Color32(0,0,0,255),
            new Color32(72,209,55,255),
            new Color32(0,168,255,255),
            new Color32(142,68,173,255),
            new Color32(230,126,34,255)
        };

        public static string[] equipmentRarityPrefix = {
            "",
            "Good",
            "Elite",
            "Legendary",
            "Heroic"
        };

        public static float[] equipmentRarityPowerModifier = {
            1.0f,
            1.2f,
            1.5f,
            2f,
            3f
        };

        public static string[] enemyStrengthString = {
            "Very Weak ",
            "Weak ",
            "",
            "Strong ",
            "Very Strong "
        };

        public static float[] enemyStrengthModifier = {
            0.75f,
            0.9f,
            1.0f,
            1.1f,
            1.25f
        };

        public const string attackSkill = "Attack";
        public const string attackSkillAOE = "AttackAOE";
        public const string defenseSkill = "Defense";
        public const string magicSkill = "Magic";
        public const string magicSkillAOE = "MagicAOE";
        public const string healSkill = "Heal";
        public const string healSkillAOE = "HealAOE";
        public const string buffSkill = "Buff";
        public const string buffSkillAOE = "BuffAOE";
        public const string deuffSkill = "Debuff";
        public const string debuffSkillAOE = "DebuffAOE";
        public const string SpecialSkill = "Special";
        public const string PassiveSkill = "Passive";
        public const string buffSelfSkill = "BuffSelf";

        public enum WeaponHand
        {
            SingleHand, DoubleHand, Shield, Armor
        }

        public enum EquipmentType
        {
            Sword, Axe, Shield, Wand, Staff, MagicBook, Bow, Dagger, HeavyArmor, LightArmor, RobeArmor, Accessory
        }

        public static int[] memberUnlockAt = { 0, 0, 8, 17, 24, 32, 45 };
        public static int[] mapUnlockAt = { 0, 6, 15, 23, 30, 35, 42 };
        //Township
        public const int TownhallMaxLv = 50;
        public const int WarehouseResourceCapacityStart=1000;
        public const int WarehouseResourceCapacityIncrement=500;
        public const int WarehouseStorageSlotStart=50;
        public const int WarehouseStorageSlotIncrement=5;
        public static int[] BasicResourceGenerateRateStart = {20,20,20,10};
        public static int[] BasicResourceGenerateRateInc = {10,10,10,5};
        public const int PopulationStart = 20;
        public const int PopulationPerHouseLv = 10;
    }
}