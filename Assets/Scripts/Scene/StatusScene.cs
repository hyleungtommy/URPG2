using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.UI;

public class StatusScene: MonoBehaviour
{
    [SerializeField] BattleMemberList battleMemberList;
    [SerializeField] Text memberName;
    [SerializeField] Text memberStatRight;
    [SerializeField] Text memberStatBottom;
    [SerializeField] Text experienceText;
    [SerializeField] Image bodyImg;

    public void OnClickBox(int id){
        battleMemberList.OnClickBox(id);
        OnSelectCharacter(id, battleMemberList.SelectedCharacter);
    }

    private void OnSelectCharacter(int id, BattleCharacter character)
    {
        if (character != null)
        {
            //expbar.noAnimationRender(character.expneed, character.currexp);
            bodyImg.sprite = character.bodyImg;
            memberName.text = character.name + "  Lv." + character.lv + " " + character.job.name;
            BasicStat stat = character.stat.toBasicStat();
            memberStatRight.text = stat.HP + "\n" + stat.MP + "\n" + stat.ATK + "\n" + stat.DEF + "\n" + stat.MATK + "\n" + stat.MDEF + "\n" + stat.AGI + "\n" + stat.DEX;
            memberStatBottom.text = character.uppt.ToString() + "\n" + character.skillPtsSpent + "/" + character.skillPtsEarned;
            experienceText.text = character.currexp + "/" + character.expneed;
            //Game.selectedCharacterInStatusScene = id;
        }
    }
}
