using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG
{
    public class BattleCtrl
    {

        public EntityEnemy[] enemyParty { get; set; }
        public EntityPlayer[] playerParty { get; set; }
        private BattleScene scene;
        public BattleState battleState { get; set; }
        public enum BattleState{
            BattleRunning, EnemyTurn, PlayerTurn, PlayerWin, EnemyWin
        }
        public enum Friction{
            Player,Enemy
        }
        public enum Selection{
            None,Attack,Item,SkillUseOnOpponent,SkillUseOnPartner,SkillPending
        }
        public enum Action{
            Attack, Item, Skill
        }
        private Queue<Entity> actionQueue = new Queue<Entity>();
        public Entity actionEntity { get; set; }
        private Entity[] playerSelectedEntity;
        public Selection selectionMode { get; set; }
        public bool bossFight { get; set; }
        public bool[] levelUps { get; set; }
        //public List<ItemAndQty> virtualInventory { get; set; }
        public int selectedItemId { get; set; }
        //public Skill selectedSkill { get; set; }
        public BattleCtrl(EntityEnemy[] enemyParty, EntityPlayer[] playerParty, BattleScene scene)
        {
            this.enemyParty = enemyParty;
            this.playerParty = playerParty;
            this.scene = scene;
            for (int i = 0; i < enemyParty.Length; i++)
            {
                enemyParty[i].id = i;
                enemyParty[i].SetOpponent(playerParty);
                enemyParty[i].setupHateMeter(playerParty);
                enemyParty[i].scene = scene;
            }
            for (int i = 0; i < playerParty.Length; i++)
            {
                if (playerParty[i] != null)
                {
                    playerParty[i].id = i;
                    playerParty[i].SetOpponent(enemyParty);
                    playerParty[i].scene = scene;
                }

            }
            // if ((Game.currentMapMode == Constant.MapModeDungeon && Game.currentDungeon.IsBossFight()) || Game.currLoc.currZone == Game.currLoc.maxZone)
            // {
            //     bossFight = true;
            // }
            if (Game.currLoc.currZone == Game.currLoc.maxZone)
            {
                bossFight = true;
            }
            else
            {
                bossFight = false;
            }
            //virtualInventory = Game.inventory.CreateVirtualItemInv();
            battleState = BattleState.BattleRunning;
        }
        private Entity getFriction(Friction friction, int index)
        {
            if (friction == Friction.Enemy)
            {
                return enemyParty[index];
            }
            else
            {
                return playerParty[index];
            }
        }

        public float getOpponentAverageAGI(Friction friction)
        {
            float avgAGI = 0.0f;
            if (friction == Friction.Enemy)
            {
                for (int i = 0; i < enemyParty.Length; i++)
                    avgAGI += enemyParty[i].stat.AGI;
                return avgAGI / enemyParty.Length;
            }
            return getFriction(friction, 0).stat.AGI;
        }

        public void tick()
        {
            if (battleState == BattleState.BattleRunning)
            {
                if (actionQueue.Count > 0)
                {
                    actionEntity = actionQueue.Dequeue();
                    //Debug.Log("action Entity:" + actionEntity.Name);
                    actionEntity.PassRound();
                    if (actionEntity.currhp > 0)
                    {
                        // if (actionEntity.isStunned())
                        // {
                        //     actionEntity.curratb = 0;
                        // }
                        // else
                        // {
                            if (actionEntity is EntityPlayer)
                            {
                                battleState = BattleState.PlayerTurn;
                                scene.onPlayerTurn();
                                scene.SetTopBarData(actionEntity.name, actionEntity.img);
                            }
                            else
                            {
                                battleState = BattleState.EnemyTurn;
                                actionEntity.TakeAction(null);
                                battleState = BattleState.BattleRunning;
                            }
                        // }
                    }
                }
                else
                {
                    foreach (EntityPlayer player in playerParty)
                    {
                        if (player != null && player.Tick(getOpponentAverageAGI(Friction.Enemy)))
                            actionQueue.Enqueue(player);
                    }
                    foreach (EntityEnemy enemy in enemyParty)
                    {
                        if (enemy != null && enemy.Tick(getOpponentAverageAGI(Friction.Player)))
                            actionQueue.Enqueue(enemy);
                    }
                }
                updateBattleState();
            }
        }

        public void updateBattleState()
        {
            if (DoesPartyLost(Friction.Player))
            {
                battleState = BattleState.EnemyWin;
                PostBattleHandler();
            }
            else if (DoesPartyLost(Friction.Enemy))
            {
                battleState = BattleState.PlayerWin;
                PostBattleHandler();
            }
            if (playerSelectedEntity != null)
            {
                for (int i = 0; i < playerSelectedEntity.Length; i++)
                {
                    if (playerSelectedEntity[i].currhp <= 0 && playerSelectedEntity[i] is EntityEnemy){}
                        scene.onEnemyDefeated(playerSelectedEntity[i].id);
                }

                playerSelectedEntity = null;
            }
        }

        public void playerUseNormalAttack()
        {
            playerTakeAction(Action.Attack, null);
        }

        private void playerTakeAction(Action actionType, IFunctionable functionable)
        {

            if (battleState == BattleState.PlayerTurn)
            {

                if (playerSelectedEntity != null)
                {
                    actionEntity.SetOpponent(playerSelectedEntity);
                    if (actionType == Action.Attack)
                    {
                        List<BattleMessage> bundle = actionEntity.UseNormalAttack();
                        scene.CreateBattleAnimation(bundle);
                    }
                    else if (actionType == Action.Item || actionType == Action.Skill)
                    {
                        actionEntity.TakeAction(functionable);

                    }

                    scene.afterPlayerAction();
                    battleState = BattleState.BattleRunning;
                }
                else
                {
                    Debug.Log("ERORR: playerSelectedEntity IS NULL");
                }
            }
        }

        public void onSelectItem(int itemId)
        {
            selectedItemId = itemId;
            battleState = BattleState.PlayerTurn;
        }

        public void useSelectedSpecial()
        {
            // if (selectionMode == SELECTION_ITEM)
            // {
            //     FunctionalItem item = DB.QueryItem(selectedItemId) as FunctionalItem;
            //     for (int i = 0; i < virtualInventory.Count; i++)
            //     {
            //         ItemAndQty a = virtualInventory[i];
            //         if (a.item != null && a.item.id == item.id)
            //         {
            //             a.qty--;
            //             if (a.qty == 0) virtualInventory.Remove(a);
            //         }
            //     }
            //     Game.inventory.smartDelete(item, 1);
            //     playerTakeAction(ACTION_ITEM, item);
            //     selectionMode = SELECTION_NONE;
            // }
            // else if (selectionMode == SELECTION_SKILL_USE_ON_ENEMY || selectionMode == SELECTION_SKILL_USE_ON_PARTNER)
            // {
            //     if (selectedSkill.aoe)
            //     {
            //         if (actionEntity is EntityPlayer && selectedSkill.useOn == GeneralSkill.UseOn.Opponent ||
            //             actionEntity is EntityEnemy && selectedSkill.useOn == GeneralSkill.UseOn.Partner)
            //         {
            //             playerSelectedEntity = getAllLivingEnemy();
            //         }
            //         else if (actionEntity is EntityPlayer && selectedSkill.useOn == GeneralSkill.UseOn.Partner ||
            //                  actionEntity is EntityEnemy && selectedSkill.useOn == GeneralSkill.UseOn.Opponent)
            //         {
            //             playerSelectedEntity = getAllLivingPlayer();
            //         }
            //     }
            //     else if (selectedSkill.useOn == GeneralSkill.UseOn.Self)
            //     {
            //         //special handling for decoy skills: it uses on self but take action on all enemies
            //         if (selectedSkill is SkillDecoy)
            //         {
            //             playerSelectedEntity = getAllLivingEnemy();
            //         }
            //         else
            //         {
            //             playerSelectedEntity = new Entity[] { actionEntity };
            //         }

            //     }

            //     playerTakeAction(ACTION_SKILL, selectedSkill);
            //     selectionMode = SELECTION_NONE;
            // }
        }

        private bool DoesPartyLost(Friction friction)
        {
            int hp0Counter = 0;
            if (friction == Friction.Enemy)
            {
                foreach (EntityEnemy e in enemyParty)
                {
                    if (e != null && e.currhp <= 0 || e == null)
                    {
                        hp0Counter++;
                    }
                }

                return hp0Counter == enemyParty.Length;
            }
            else
            {
                foreach (EntityPlayer e in playerParty)
                {
                    if (e != null && e.currhp <= 0 || e == null)
                    {
                        hp0Counter++;
                    }
                }

                return hp0Counter == playerParty.Length;
            }
        }

        public void selectEnemy(int i)
        {
            playerSelectedEntity = new Entity[1];
            //if (bossFight && i == 5)
            //    playerSelectedEntity[0] = enemyParty[0];
            //else
            playerSelectedEntity[0] = enemyParty[i];
        }

        public void selectPlayer(int i)
        {
            playerSelectedEntity = new Entity[1];
            playerSelectedEntity[0] = playerParty[i];
        }


        private void PostBattleHandler()
        {
            Game.rareEnemyAppeared = false;
            //List<ItemAndQty> drops = new List<ItemAndQty>();
            if (battleState == BattleState.PlayerWin)
            {
                Game.money += getTotalMoneyGain();
                //RPGSystem.questManager.updateQuest(enemyParty);
                levelUps = new bool[] { false, false, false, false };
                int i = 0;
                foreach (BattleCharacter c in GameController.Instance.party.GetAllBattleCharacter())
                {
                    if (c != null)
                    {
                        levelUps[i] = c.assignEXP(getTotalEXPGain());
                    }
                    i++;
                }
                // drops = getEnemyDrop();
                //put into inventory
                // foreach (ItemAndQty item in drops)
                // {
                //     Game.inventory.smartInsert(item.item, item.qty);
                // }
                // if(Game.currentMapMode == Constant.MapModeProgressive && Game.currLoc.currZone == Game.currLoc.maxZone){
                //     Game.platinumCoin += Game.currLoc.platinumCoinGain;
                // }
                // Game.questManager.UpdateEnemyCount(enemyParty);
            }
            //Game.globalBuffManager.PassRound();
            scene.showRewardPanel();//(drops);


        }

        public int getTotalEXPGain()
        {

            int maxLvInBattleParty = 0;
            foreach (BattleCharacter c in GameController.Instance.party.GetAllBattleCharacter())
            {
                if (c != null && c.unlocked && c.lv > maxLvInBattleParty)
                {
                    maxLvInBattleParty = c.lv;
                }
            }
            float levelPanality = 1.0f;
            if (maxLvInBattleParty > Game.currLoc.maxLv)
            {
                levelPanality -= (maxLvInBattleParty - Game.currLoc.maxLv) * 0.1f;
            }
            if (levelPanality < 0.1f)
                levelPanality = 0.1f;
            //Debug.Log(levelPanality);
            int totalEXP = 0;
            for (int i = 0; i < enemyParty.Length; i++)
                totalEXP += Mathf.FloorToInt((enemyParty[i] as EntityEnemy).dropEXP * levelPanality * Param.expRatio * (1 + Game.currLoc.currZone * Param.areaRewardMultiplier));// * Game.globalBuffManager.GetModFromType("EXP"));
            //Debug.Log("getTotalEXPGain() =" + totalEXP);
            
            return totalEXP;
        }

        public int getTotalMoneyGain()
        {
            int totalMoney = 0;
            for (int i = 0; i < enemyParty.Length; i++)
                totalMoney += Mathf.FloorToInt((enemyParty[i] as EntityEnemy).dropMoney * (1 + Game.currLoc.currZone * Param.areaRewardMultiplier)); //* Game.globalBuffManager.GetModFromType("ItemDrop"));
            //Debug.Log ("getTotalMoneyGain() =" + totalMoney);
            return totalMoney;
        }

        // public List<ItemAndQty> getEnemyDrop()
        // {
        //     List<ItemAndQty> drops = new List<ItemAndQty>();
        //     //global drop
        //     if (Random.Range(0, 100) < Param.equipmentDropRate)
        //     {
        //         //restrict equipment level to be same level as the map
        //         List<int> equipDropList = DB.getEquipmentDropList(Game.currLoc.reqLv);
        //         if (equipDropList.Count > 0)
        //         {
        //             //generate equipment with random rein lv and enchantment
        //             int rndEquipId = Util.getRandomIndexFrom(equipDropList.ToArray());
        //             int rndRarity = Random.Range(0, 5);
        //             Equipment equip = DB.QueryEquipment(rndEquipId).toEquipment(rndRarity);
        //             if (equip.reinforceRecipe != null)
        //             {
        //                 int rndReinLv = Random.Range(0, equip.reinforceRecipe.maxReinforceLv + 1);
        //                 equip.reinforceRecipe.reinforceLv = rndReinLv;
        //             }
        //             if (equip.enchantment != null)
        //             {
        //                 equip.enchant();
        //             }
        //             drops.Add(new ItemAndQty(equip, 1));
        //         }

        //     }
        //     return drops;
        // }

        public void skipBattle()
        {
            battleState = BattleState.PlayerWin;
            PostBattleHandler();
        }

        public EntityPlayer[] getAllLivingPlayer()
        {
            return playerParty.Where(e => e != null && e.currhp > 0).ToArray();
        }

        public EntityEnemy[] getAllLivingEnemy()
        {
            return enemyParty.Where(e => e != null && e.currhp > 0).ToArray();
        }

    }
}
