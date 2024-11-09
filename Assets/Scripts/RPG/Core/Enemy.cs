using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Map/Enemy")]
    public class Enemy : ScriptableObject
    {
        public int id;
        public string enemyName;
        public Sprite img;
        public int HP;
        public int MP;
        public int ATK;
        public int DEF;
        public int MATK;
        public int MDEF;
        public int AGI;
        public int DEX;
        public int DropEXP;
        public int DropMoney;

        public EntityEnemy toEntity()
        {
            BasicStat stat = new BasicStat(HP, MP, ATK, DEF, MATK, MDEF, AGI, DEX);
            stat = stat.multiply(Param.difficultyModifier[Game.difficulty]);
            EntityEnemy entity = new EntityEnemy(name, stat, img, DropEXP, DropMoney);
            entity.strengthLv = 2;
            // if (elementResistance != null)
            // {
            //     entity.elementResistance = elementResistance;
            // }
            return entity;
        }

        public EntityEnemy toEntity(int strengthLv, float mapAreaStrengthModifier)
        {
            BasicStat stat = new BasicStat(HP, MP, ATK, DEF, MATK, MDEF, AGI, DEX);
            stat = stat.multiply(Constant.enemyStrengthModifier[strengthLv]);
            stat = stat.multiply(1 + mapAreaStrengthModifier);
            stat = stat.multiply(Param.difficultyModifier[Game.difficulty]);
            EntityEnemy entity = new EntityEnemy(name, stat, img, DropEXP, DropMoney);
            entity.strengthLv = strengthLv;
            // if (elementResistance != null)
            // {
            //     entity.elementResistance = elementResistance;
            // }
            return entity;
        }
    }
}

