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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCharacter(BattleCharacter character, bool lvup)
    {
        this.character = character;
        this.lvup = lvup;
    }

    public void render()
    {
        basicBox.render(character);
        bar.noAnimationRender(character.expneed, character.currexp);
        levelup.gameObject.SetActive(lvup);
    }
}

