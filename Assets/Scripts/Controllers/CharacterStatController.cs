using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class CharacterStatController : MonoBehaviour
{
    public BarCtrl hpBar;
    public BarCtrl mpBar;
    public BarCtrl atbBar;
    public Image player;
    //public BuffCtrl buffCtrl;

    EntityPlayer character;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setEntity(EntityPlayer character)
    {
        this.character = character;
    }

    public void render()
    {
        if (character != null)
        {
            gameObject.SetActive(true);
            player.sprite = character.img;
            hpBar.NoAnimationRender(character.stat.HP, character.currhp);
            mpBar.NoAnimationRender(character.stat.MP, character.currmp);
            atbBar.NoAnimationRender(100f, character.curratb);
            //buffCtrl.render(character.buffState);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
