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
        public enum BattleState
        {
            BattleRunning, EnemyTurn, PlayerTurn, PlayerWin, EnemyWin
        }
        public enum Friction
        {
            Player, Enemy
        }
        public enum Selection
        {
            None, Attack, Item, SkillUseOnOpponent, SkillUseOnPartner, SkillPending
        }
        public enum Action
        {
            Attack, Item, Skill
        }
        private Queue<Entity> actionQueue = new Queue<Entity>();
        public Entity actionEntity { get; set; }
        private Entity[] playerSelectedEntity;
        public Selection selectionMode { get; set; }
        public bool bossFight { get; set; }
        public bool[] levelUps { get; set; }
        public List<ItemAndQty> virtualInventory { get; set; }
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
                enemyParty[i].SetupHateMeter(playerParty);
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
            virtualInventory = Game.inventory.CreateVirtualItemInv();
            battleState = BattleState.BattleRunning;
        }
        private Entity GetFriction(Friction friction, int index)
        {
            return friction == Friction.Enemy ? enemyParty[index] as Entity : playerParty[index] as Entity;
        }

        public float GetOpponentAverageAGI(Friction friction)
        {
            float avgAGI = 0.0f;
            if (friction == Friction.Enemy)
            {
                for (int i = 0; i < enemyParty.Length; i++)
                    avgAGI += enemyParty[i].stat.AGI;
                return avgAGI / enemyParty.Length;
            }
            return GetFriction(friction, 0).stat.AGI;
        }

        public void Tick()
        {
            if (battleState == BattleState.BattleRunning)
            {
                if (actionQueue.Count > 0)
                {
                    actionEntity = actionQueue.Dequeue();
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
                            scene.OnPlayerTurn();
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
                        if (player != null && player.Tick(GetOpponentAverageAGI(Friction.Enemy)))
                            actionQueue.Enqueue(player);
                    }
                    foreach (EntityEnemy enemy in enemyParty)
                    {
                        if (enemy != null && enemy.Tick(GetOpponentAverageAGI(Friction.Player)))
                            actionQueue.Enqueue(enemy);
                    }
                }
                UpdateBattleState();
            }
        }

        public void UpdateBattleState()
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
                foreach (Entity enemy in playerSelectedEntity)
                {
                    if (enemy.currhp <= 0 && enemy is EntityEnemy)
                        scene.RemoveEnemyObject(enemy.id);
                }
                playerSelectedEntity = null;
            }
        }

        public void PlayerUseNormalAttack()
        {
            PlayerTakeAction(Action.Attack, null);
        }

        private void PlayerTakeAction(Action actionType, IFunctionable functionable)
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

        public void SelectItem(int itemId)
        {
            selectedItemId = itemId;
            battleState = BattleState.PlayerTurn;
        }

        public void UseSelectedSpecial()
        {
            if (selectionMode == Selection.Item)
            {
                FunctionalItem item = DBManager.Instance.Items[selectedItemId - 1] as FunctionalItem;
                for (int i = 0; i < virtualInventory.Count; i++)
                {
                    ItemAndQty a = virtualInventory[i];
                    if (a.item != null && a.item.id == item.id)
                    {
                        a.qty--;
                        if (a.qty == 0) virtualInventory.Remove(a);
                    }
                }
                Game.inventory.smartDelete(item, 1);
                PlayerTakeAction(Action.Item, item);
                selectionMode = Selection.None;
            }
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
            return friction == Friction.Enemy ? GetAllLivingEnemy().Length == 0: GetAllLivingPlayer().Length == 0;
        }

        public void SelectEnemy(int i)
        {
            playerSelectedEntity = new Entity[1] { enemyParty[i] };
        }

        public void SelectPlayer(int i)
        {
            playerSelectedEntity = new Entity[1] { playerParty[i] };
        }


        private void PostBattleHandler()
        {
            Game.rareEnemyAppeared = false;
            //List<ItemAndQty> drops = new List<ItemAndQty>();
            if (battleState == BattleState.PlayerWin)
            {
                Game.money += GetTotalMoneyGain();
                levelUps = new bool[] { false, false, false, false };
                int i = 0;
                foreach (BattleCharacter c in GameController.Instance.party.GetAllBattleCharacter())
                {
                    if (c != null)
                    {
                        levelUps[i] = c.assignEXP(GetTotalEXPGain());
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

        public int GetTotalEXPGain()
        {
            // level penality = (highest lv in party - map max lv) * 0.1
            const float miniumLevelPanality = 0.1f;
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
            if (levelPanality < miniumLevelPanality){
                levelPanality = miniumLevelPanality;
            }
            
            int totalEXP = 0;
            float zoneMultiplier = 1 + Game.currLoc.currZone * Param.areaRewardMultiplier;
            //float globalMultiplier = Game.globalBuffManager.GetModFromType("EXP");
            float finalMultiplier = zoneMultiplier * levelPanality * Param.expRatio;// * globalMultiplier
            foreach (EntityEnemy enemy in enemyParty){
                totalEXP += Mathf.FloorToInt(enemy.dropMoney * finalMultiplier);
            }

            return totalEXP;
        }

        public int GetTotalMoneyGain()
        {
            int totalMoney = 0;
            float zoneMultiplier = 1 + Game.currLoc.currZone * Param.areaRewardMultiplier;
            //float globalMultiplier = Game.globalBuffManager.GetModFromType("ItemDrop");
            float finalMultiplier = zoneMultiplier;// * globalBuffMultiplier;

            foreach (EntityEnemy enemy in enemyParty){
                totalMoney += Mathf.FloorToInt(enemy.dropMoney * finalMultiplier);
            }

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

        public void SkipBattle()
        {
            battleState = BattleState.PlayerWin;
            PostBattleHandler();
        }

        public EntityPlayer[] GetAllLivingPlayer()
        {
            return playerParty.Where(e => e != null && e.currhp > 0).ToArray();
        }

        public EntityEnemy[] GetAllLivingEnemy()
        {
            return enemyParty.Where(e => e != null && e.currhp > 0).ToArray();
        }

    }
}
