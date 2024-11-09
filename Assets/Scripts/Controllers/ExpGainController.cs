using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;


public class ExpGainController : MonoBehaviour
{
    public BasicBox basicBox;
    public BarCtrl bar;
    public Image levelup;
    BattleCharacter character;
    private bool lvup;

    public void SetCharacter(BattleCharacter character, bool lvup)
    {
        this.character = character;
        this.lvup = lvup;
    }

    public void Render()
    {
        basicBox.Render(character);
        bar.NoAnimationRender(character.expneed, character.currexp);
        levelup.gameObject.SetActive(lvup);
    }
}

