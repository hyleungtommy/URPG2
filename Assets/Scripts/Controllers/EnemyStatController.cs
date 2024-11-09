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
    public void setEntity(EntityEnemy enemy)
    {
        this.entity = enemy;
    }

    public void Render()
    {
        if (entity != null)
        {
            enemy.sprite = entity.img;
            hpBar.NoAnimationRender(entity.stat.HP, entity.currhp);
            enemyName.text = entity.fullName;
            //buffCtrl.render(entity.buffState);
        }

    }

}
