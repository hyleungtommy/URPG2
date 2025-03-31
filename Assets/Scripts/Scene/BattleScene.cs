using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
using UnityEngine.SceneManagement;
public class BattleScene : BasicScene
{
    public EnemyStatController[] enemyStats;
    public CharacterStatController[] playerStats;
    public EnemyStatController bossStat;
    public GameObject topBar;
    public Text topBarText;
    BattleCtrl battleCtrl;
    public Image battleBG;
    public GameObject actionBtnGrp;
    public RewardPanel rewardPanel;
    public GameObject areaPanel;
    public Text textAreaPanel;
    public ItemPanelController itemPanel;
    public SkillPanelController skillPanel;

    private bool showingRewardPanel;// prevent multi update of reward panel;
    // Start is called before the first frame update
    void Start()
    {
        PrepareBattle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("skip battle");
            battleCtrl.SkipBattle();
        }
    }

    void PrepareBattle()
    {
        // PlotData pd = PlotMatcher.matchPlotBattle(Game.currLoc.currZone, true);
        // if (pd != null)
        // {
        //     Debug.Log("Plot found, pt=" + pd.triggerPt + ", area=" + pd.triggerArea + ",before battle=" + pd.triggerBeforeBattle);
        //     PlotMatcher.nextPlot = pd;
        //     PlotMatcher.nextScene = SceneName.Battle;
        //     SceneManager.LoadScene(SceneName.Dialog);
        // }
        // else
        // {
        // if(Game.currentMapMode == Constant.MapModeDungeon){
        //     battleCtrl = new BattleCtrl(Game.currentDungeon.GenerateEnemy(), Game.party.createBattleParty(), this);
        //     battleBG.sprite = Game.currentDungeon.battleImg;
        // }else{
        battleCtrl = new BattleCtrl(Game.currLoc.GenerateEnemy(), GameController.Instance.party.CreateBattleParty(), this);
        battleBG.sprite = Game.currLoc.battleImg;
        // }

        if (Game.currentMapMode == Game.MapMode.Progression)
        {
            textAreaPanel.text = Game.currLoc.currZone + "/" + Game.currLoc.maxZone;
        }
        areaPanel.gameObject.SetActive(Game.currentMapMode == Game.MapMode.Progression);

        if (battleCtrl.bossFight || Game.rareEnemyAppeared)
        {
            bossStat.gameObject.SetActive(true);
            bossStat.setEntity(battleCtrl.enemyParty[0]);
            for (int i = 0; i < enemyStats.Length; i++)
            {
                enemyStats[i].gameObject.SetActive(false);
            }
        }
        else
        {
            bossStat.gameObject.SetActive(false);
            for (int i = 0; i < enemyStats.Length; i++)
            {
                if (i < battleCtrl.enemyParty.Length)
                {
                    enemyStats[i].gameObject.SetActive(true);
                    enemyStats[i].setEntity(battleCtrl.enemyParty[i]);
                }
                else
                {
                    enemyStats[i].gameObject.SetActive(false);
                }
            }
        }

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (i < battleCtrl.playerParty.Length)
            {
                playerStats[i].gameObject.SetActive(true);
                playerStats[i].setEntity(battleCtrl.playerParty[i]);
            }
            else
            {
                playerStats[i].gameObject.SetActive(false);
            }
        }
        showingRewardPanel = false;
        rewardPanel.gameObject.SetActive(false);
        itemPanel.gameObject.SetActive(false);
        skillPanel.gameObject.SetActive(false);
        StartCoroutine("tick");
        //}

    }

    private void RenderEntityStats()
    {
        if (battleCtrl.bossFight || Game.rareEnemyAppeared)
        {
            bossStat.Render();
        }
        else
        {
            RenderFrictionStats(BattleCtrl.Friction.Enemy);
        }
        RenderFrictionStats(BattleCtrl.Friction.Player);
    }

    IEnumerator tick()
    {
        while (battleCtrl.battleState == BattleCtrl.BattleState.BattleRunning || battleCtrl.battleState == BattleCtrl.BattleState.PlayerTurn)
        {
            if (battleCtrl.battleState == BattleCtrl.BattleState.BattleRunning)
                battleCtrl.Tick();
            RenderEntityStats();
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void SetTopBarData(string name, Sprite img)
    {
        topBarText.text = name + " : Please choose action";
    }

    public void OnPlayerTurn()
    {
        battleCtrl.selectionMode = BattleCtrl.Selection.None;
        actionBtnGrp.SetActive(true);
        topBar.gameObject.SetActive(true);
    }

    public void RemoveEnemyObject(int index)
    {
        if (battleCtrl.bossFight || Game.rareEnemyAppeared)
        {
            bossStat.gameObject.SetActive(false);
        }
        else
        {
            enemyStats[index].gameObject.SetActive(false);
        }

    }

    public void onSelectEnemy(int index)
    {
        if (battleCtrl.battleState == BattleCtrl.BattleState.PlayerTurn)
        {
            battleCtrl.SelectEnemy(index);
            if (battleCtrl.selectionMode == BattleCtrl.Selection.Attack)
            {
                battleCtrl.PlayerUseNormalAttack();
                if (battleCtrl.actionEntity != null && battleCtrl.actionEntity is EntityPlayer)
                {
                    //StartCoroutine(changeCharacterFace(battleCtrl.ActionEntity.Id));

                    if (battleCtrl.bossFight || Game.rareEnemyAppeared)
                    {
                        bossStat.Render();
                    }
                    else
                    {
                        RenderFrictionStats(BattleCtrl.Friction.Enemy);
                    }

                }
                actionBtnGrp.SetActive(false);
            }
            else if (battleCtrl.selectionMode == BattleCtrl.Selection.SkillUseOnOpponent)
            {

                battleCtrl.UseSelectedSpecial();
                if (battleCtrl.actionEntity != null && battleCtrl.actionEntity is EntityPlayer)
                {
                    //StartCoroutine (changeCharacterFace (battleCtrl.ActionEntity.Id));
                    RenderFrictionStats(BattleCtrl.Friction.Player);
                }
                actionBtnGrp.SetActive(false);

            }
        }
    }

    public void onSelectPlayer(int index)
    {
        // when a party member is selected (by either using Item or Skill on Player Party)
        if (battleCtrl.battleState == BattleCtrl.BattleState.PlayerTurn)
        {
            battleCtrl.SelectPlayer(index);
            if (battleCtrl.selectionMode == BattleCtrl.Selection.Item || battleCtrl.selectionMode == BattleCtrl.Selection.SkillUseOnPartner)
            {
                battleCtrl.UseSelectedSpecial();
            }
        }

    }

    public void OnSelectItem(int elementId)
    {
        int itemId = itemPanel.elements[elementId].itemId;
        if (itemId >= 0)
        {
            battleCtrl.SelectItem(itemId);
            itemPanel.gameObject.SetActive(false);
            topBarText.text = Constant.topBarSelectPlayer;
        }
    }

    public void OnSelectSkill(int elementId)
    {
        int skillId = skillPanel.elements[elementId].SkillId;
        Skill s = skillPanel.elements[elementId].Skill;
        if (skillId >= 0 && s != null)
        {
            if (battleCtrl.battleState == BattleCtrl.BattleState.PlayerTurn)
            {
                battleCtrl.selectedSkill = s;
                if (s.isAOE)
                {
                    if (s.useOn == UseOn.Opponent)
                    {
                        battleCtrl.selectionMode = BattleCtrl.Selection.SkillUseOnOpponent;
                        battleCtrl.UseSelectedSpecial();
                    }
                    else
                    {
                        battleCtrl.selectionMode = BattleCtrl.Selection.SkillUseOnPartner;
                        battleCtrl.UseSelectedSpecial();
                    }
                }
                else
                {
                    if (s.useOn == UseOn.Opponent)
                    {
                        battleCtrl.selectionMode = BattleCtrl.Selection.SkillUseOnOpponent;
                        topBarText.text = Constant.topBarSelectAnEnemy;
                    }
                    else if (s.useOn == UseOn.Self)
                    {
                        battleCtrl.selectionMode = BattleCtrl.Selection.SkillUseOnPartner;
                        battleCtrl.UseSelectedSpecial();
                    }
                    else
                    {
                        battleCtrl.selectionMode = BattleCtrl.Selection.SkillUseOnPartner;
                        topBarText.text = Constant.topBarSelectPlayer;
                    }
                }
            }
            skillPanel.gameObject.SetActive(false);
        }
    }

    public void onClickAttackButton()
    {
        if (battleCtrl.battleState == BattleCtrl.BattleState.PlayerTurn)
        {
            battleCtrl.selectionMode = BattleCtrl.Selection.Attack;
            //setEnemySelectionArrowActive(true);
            itemPanel.gameObject.SetActive(false);
            // skillPanel.gameObject.SetActive(false);
            topBar.gameObject.SetActive(true);
            topBarText.text = Constant.topBarSelectAnEnemy;
            //setPlayerSelectionArrowActive(true);
        }
    }

    public void OnClickItemButton()
    {
        if (battleCtrl.battleState == BattleCtrl.BattleState.PlayerTurn)
        {
            battleCtrl.selectionMode = BattleCtrl.Selection.Item;
            topBar.gameObject.SetActive(true);
            topBarText.text = Constant.topBarSelectAnItem;
            itemPanel.SetVirtualInventory(battleCtrl.virtualInventory);
            itemPanel.gameObject.SetActive(true);
            //skillPanel.gameObject.SetActive(false);
            itemPanel.Render();
        }
    }

    public void onClickSkillButton()
    {
        if (battleCtrl.battleState == BattleCtrl.BattleState.PlayerTurn)
        {
            battleCtrl.selectionMode = BattleCtrl.Selection.SkillPending;
            topBar.gameObject.SetActive(true);
            topBarText.text = Constant.topBarSelectASkill;
            skillPanel.SelectedPlayer = battleCtrl.actionEntity as EntityPlayer;
            skillPanel.gameObject.SetActive(true);
            itemPanel.gameObject.SetActive(false);
            skillPanel.Render();
        }
    }

    public void afterPlayerAction()
    {
        actionBtnGrp.gameObject.SetActive(false);
        //for (int j = 0; j < enemyImg.Length; j++) 
        //enemyImg [j].transform.GetChild (0).gameObject.SetActive (false);
        //setPlayerSelectionArrowActive(false);
        //setEnemySelectionArrowActive(false);
        topBar.gameObject.SetActive(false);
        if (battleCtrl.bossFight || Game.rareEnemyAppeared)
        {
            bossStat.Render();
        }
        else
        {
            RenderFrictionStats(BattleCtrl.Friction.Enemy);
        }
    }

    private void RenderFrictionStats(BattleCtrl.Friction friction)
    {
        if (friction == BattleCtrl.Friction.Player)
        {
            foreach (CharacterStatController characterStat in playerStats)
            {
                characterStat.render();
            }
        }
        else
        {
            foreach (EnemyStatController enemyStat in enemyStats)
            {
                enemyStat.Render();
            }
        }
    }

    public void showRewardPanel()//List<ItemAndQty>drops)
    {
        if (!showingRewardPanel)
        {
            showingRewardPanel = true;
            //rewardPanel.showRewardPanel(this, battleCtrl);
            rewardPanel.SetBattleCtrl(battleCtrl);
            //rewardPanel.setEnemyDrop(drops);
            rewardPanel.Show();
            //Game.SaveGame();
        }

    }

    public void onClickRewardPanelButton(int id)
    {
        // PlotData pd = PlotMatcher.matchPlotBattle(Game.currLoc.currZone, false);
        switch (id)
        {
            case 0: //Back to Main Menu
                Game.currLoc.ResetZoneStatus();
                Game.ChangeGameState(Game.State.FreeRoam);
                SceneManager.LoadScene("World");
                break;
            case 1: // Retry Battle
                // if (pd != null)
                // {
                //     Debug.Log("Plot found, pt=" + pd.triggerPt + ", area=" + pd.triggerArea + ",before battle=" + pd.triggerBeforeBattle);
                //     PlotMatcher.nextPlot = pd;
                //     PlotMatcher.nextScene = SceneName.Battle;
                //     SceneManager.LoadScene(SceneName.Dialog);
                // }
                // else
                // {
                //     prepareBattle();
                // }
                PrepareBattle();

                break;
            case 2:// Next Battle
                if (battleCtrl.bossFight)
                {
                    Game.currLoc.ResetZoneStatus();
                    // if (pd != null)
                    // {
                    //     Debug.Log("Plot found, pt=" + pd.triggerPt + ", area=" + pd.triggerArea + ",before battle=" + pd.triggerBeforeBattle);
                    //     PlotMatcher.nextPlot = pd;
                    //     PlotMatcher.nextScene = SceneName.MainMenu;
                    //     SceneManager.LoadScene(SceneName.Dialog);
                    // }
                    // else
                    // {
                    //     jumpToScene(SceneName.MainMenu);
                    // }
                    Game.ChangeGameState(Game.State.FreeRoam);
                    SceneManager.LoadScene("World");
                }
                // else if(Game.currentMapMode == Constant.MapModeDungeon){
                //     jumpToScene(SceneName.Dungeon);
                // }
                else
                {
                    Game.currLoc.ProgressZone();
                    // if (pd != null)
                    // {
                    //     Debug.Log("Plot found, pt=" + pd.triggerPt + ", area=" + pd.triggerArea + ",before battle=" + pd.triggerBeforeBattle);
                    //     PlotMatcher.nextPlot = pd;
                    //     PlotMatcher.nextScene = SceneName.Battle;
                    //     SceneManager.LoadScene(SceneName.Dialog);
                    // }
                    // else
                    // {
                    //     prepareBattle();
                    // }
                    PrepareBattle();
                }
                break;

                // }
        }
    }

    public void CreateBattleAnimation(List<BattleMessage> bundle)
    {
        Transform trans = null;
        if (bundle != null && bundle.Count > 0)
        {
            foreach (BattleMessage message in bundle)
            {
                bool isEnemyMessage = (message.sender is EntityPlayer && message.isAttackMessage()) || (message.sender is EntityEnemy && !message.isAttackMessage());
                if (isEnemyMessage)
                {
                    if (battleCtrl.bossFight || Game.rareEnemyAppeared)
                    {
                        trans = bossStat.transform;
                    }
                    else
                    {
                        trans = enemyStats[message.receiver.id].transform;
                    }

                }
                else
                {
                    trans = playerStats[message.receiver.id].transform;
                }
                GetComponent<BattleAnimator>().CreateDamageText(message, trans);
            }

            bool isEnemyAOEMessage = (bundle[0].AOE && bundle[0].sender is EntityPlayer && bundle[0].isAttackMessage()) || (bundle[0].AOE && bundle[0].sender is EntityEnemy && !bundle[0].isAttackMessage());
            bool isPlayerAOEMessage = (bundle[0].AOE && bundle[0].sender is EntityEnemy && bundle[0].isAttackMessage()) || (bundle[0].AOE && bundle[0].sender is EntityPlayer && !bundle[0].isAttackMessage());
            if (isEnemyAOEMessage)
            {
                if (battleCtrl.bossFight || Game.rareEnemyAppeared)
                {
                    GetComponent<BattleAnimator>().CreateSkillAnimation(bundle[0], bossStat.transform);
                }
                else
                {
                    for (int j = 0; j < enemyStats.Length; j++)
                    {
                        if (enemyStats[j].enemy.IsActive())
                            GetComponent<BattleAnimator>().CreateSkillAnimation(bundle[0], enemyStats[j].transform);
                    }
                }

            }
            else if (isPlayerAOEMessage)
            {
                for (int j = 0; j < playerStats.Length; j++)
                {
                    if (playerStats[j].player.IsActive())
                        GetComponent<BattleAnimator>().CreateSkillAnimation(bundle[0], playerStats[j].transform);
                }
            }

            // show skill name on enemyCTRL
            /*
			if (bundle [0].sender is EntityEnemy && bundle [0].type != BattleMessage.Type.NormalAttack) {
				Debug.Log ("show " + bundle [0].sender.Id);
				enemyInfo[bundle [0].sender.Id].GetComponent<BattleEnemyInfoCtrl> ().showSkillButton (bundle [0].SkillName);
			}
            */
            GetComponent<BattleAnimator>().CreateSkillAnimation(bundle[0], trans);

        }

    }




}

