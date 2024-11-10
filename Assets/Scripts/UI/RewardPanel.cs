using System.Collections;
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
    [SerializeField] Sprite YouWin;
    [SerializeField] Sprite YouLose;
    //List<ItemAndQty>drops;


    public void SetBattleCtrl(BattleCtrl battleCtrl)
    {
        this.battleCtrl = battleCtrl;
    }

    // public void setEnemyDrop(List<ItemAndQty>drops){
    //     this.drops = drops;
    // }

    public void Show()
    {
        gameObject.SetActive(true);
        if (battleCtrl.battleState == BattleCtrl.BattleState.PlayerWin)
        {
            title.sprite = YouWin;
            textMoney.text = battleCtrl.GetTotalMoneyGain().ToString();
            textEXP.text = battleCtrl.GetTotalEXPGain().ToString();
            BattleCharacter[] characters = GameController.Instance.party.GetAllBattleCharacter();
            for (int i = 0; i < 4; i++)
            {
                expGain[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < characters.Length; i++)
            {
                expGain[i].gameObject.SetActive(true);
                expGain[i].SetCharacter(characters[i], battleCtrl.levelUps[i]);
                expGain[i].Render();
            }
            // for(int i = 0; i < enemyDropDisplays.Length ; i++){
            //     if(i >= drops.Count){
            //         enemyDropDisplays[i].gameObject.SetActive(false);
            //     }else{
            //         enemyDropDisplays[i].gameObject.SetActive(true);
            //         enemyDropDisplays[i].render(drops[i].item);
            //     }
            // }
            if(Game.currentMapMode == Game.MapMode.Progression && Game.currLoc.currZone == Game.currLoc.maxZone){
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
        btnRebattle.gameObject.SetActive(Game.currentMapMode == Game.MapMode.Explore);
        btnNextArea.gameObject.SetActive((Game.currentMapMode == Game.MapMode.Progression || Game.currentMapMode == Game.MapMode.Dungeon) 
                                        && battleCtrl.battleState == BattleCtrl.BattleState.PlayerWin);

    }
}
