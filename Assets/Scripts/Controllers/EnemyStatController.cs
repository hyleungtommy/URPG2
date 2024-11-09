using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class EnemyStatController : MonoBehaviour
{
    public BarCtrl hpBar;
    public Image enemy;
    public Text enemyName;
    EntityEnemy entity;
    //public BuffCtrl buffCtrl;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setEntity(EntityEnemy enemy)
    {
        this.entity = enemy;
    }

    public void render()
    {
        if (entity != null)
        {
            enemy.sprite = entity.img;
            hpBar.noAnimationRender(entity.stat.HP, entity.currhp);
            enemyName.text = entity.fullName;
            //buffCtrl.render(entity.buffState);
        }

    }

}
