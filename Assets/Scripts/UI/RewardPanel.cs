﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class RewardPanel : MonoBehaviour
{
    public Image title;
    public Text textMoney;
    public Text textEXP;
    public Button btnRebattle;
    public Button btnNextArea;
    public ExpGainController[] expGain;
    //public ItemBox[] enemyDropDisplays;
    public GameObject platinumCoinGain;
    public Text platinumCoinGainText;
    BattleCtrl battleCtrl;
    BattleScene scene;
    [SerializeField] Sprite YouWin;
    [SerializeField] Sprite YouLose;
    //List<ItemAndQty>drops;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setBattleCtrl(BattleScene scene, BattleCtrl battleCtrl)
    {
        this.battleCtrl = battleCtrl;
        this.scene = scene;
    }

    // public void setEnemyDrop(List<ItemAndQty>drops){
    //     this.drops = drops;
    // }

    public void show()
    {
        gameObject.SetActive(true);
        if (battleCtrl.battleState == BattleCtrl.PLAYER_WIN)
        {
            title.sprite = YouWin;
            textMoney.text = battleCtrl.getTotalMoneyGain().ToString();
            textEXP.text = battleCtrl.getTotalEXPGain().ToString();
            BattleCharacter[] characters = GameController.Instance.party.getAllBattleCharacter();
            for (int i = 0; i < 4; i++)
            {
                expGain[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < characters.Length; i++)
            {
                expGain[i].gameObject.SetActive(true);
                expGain[i].setCharacter(characters[i], battleCtrl.levelUps[i]);
                expGain[i].render();
            }
            // for(int i = 0; i < enemyDropDisplays.Length ; i++){
            //     if(i >= drops.Count){
            //         enemyDropDisplays[i].gameObject.SetActive(false);
            //     }else{
            //         enemyDropDisplays[i].gameObject.SetActive(true);
            //         enemyDropDisplays[i].render(drops[i].item);
            //     }
            // }
            if(Game.currentMapMode == Constant.MapModeProgressive && Game.currLoc.currZone == Game.currLoc.maxZone){
                platinumCoinGain.gameObject.SetActive(true);
                platinumCoinGainText.text = Game.currLoc.platinumCoinGain.ToString();
            }else{
                platinumCoinGain.gameObject.SetActive(false);
            }
        }
        else
        {
            title.sprite = YouLose;
            textMoney.text = "0";
            textEXP.text = "0";
            for (int i = 0; i < expGain.Length; i++)
            {
                expGain[i].gameObject.SetActive(false);
            }
            // for(int i = 0; i < enemyDropDisplays.Length ; i++){
            //     enemyDropDisplays[i].gameObject.SetActive(false);
            // }
        }
        btnRebattle.gameObject.SetActive(Game.currentMapMode == Constant.MapModeExplore);
        btnNextArea.gameObject.SetActive((Game.currentMapMode == Constant.MapModeProgressive || Game.currentMapMode == Constant.MapModeDungeon) 
                                        && battleCtrl.battleState == BattleCtrl.PLAYER_WIN);

    }

    public void onClickRewardPanelButton(int id)
    {
        scene.onClickRewardPanelButton(id);
    }
}