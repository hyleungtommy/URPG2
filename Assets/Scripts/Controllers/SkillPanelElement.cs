using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class SkillPanelElement : MonoBehaviour
{
    public Text TextSkillName;
    public Text TextSkillRequireMP;
    public Text TextSkillCurrentCooldown;
    public Image ImageSkillIcon;
    public int SkillId {get; set;}
    public Skill Skill {get; set;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Render(Skill skill, EntityPlayer player)
    {
        Skill = skill;
        //int requreMP = (int)(item.reqMp * ModifierFromBuffHelper.getMPUseModifierFromBuff(character) * ModifierFromBuffHelper.getMPModifierFromPassiveSkill(character));
        int requireMp = skill.reqMp;
        int currentCooldown = skill.currCooldown;

        TextSkillName.text = skill.skillName;
        if(player.currmp < requireMp)
        {
            TextSkillRequireMP.color = Color.red;
        }
        else
        {
            TextSkillRequireMP.color = Color.white;
        }
        TextSkillRequireMP.text = "MP: " + requireMp;
        if(currentCooldown > 0)
        {
            TextSkillCurrentCooldown.gameObject.SetActive(true);
            TextSkillCurrentCooldown.text = "Cooldown: " + currentCooldown;
        }
        else
        {
            TextSkillCurrentCooldown.gameObject.SetActive(false);
        }

        ImageSkillIcon.gameObject.SetActive(true);
        ImageSkillIcon.sprite = skill.img;

        GetComponent<Button>().enabled = currentCooldown <= 0 && player.currmp >= requireMp;
    }

    public void RenderEmpty()
    {
        TextSkillName.text = "";
        TextSkillRequireMP.text = "";
        TextSkillCurrentCooldown.text = "";
        ImageSkillIcon.gameObject.SetActive(false);
    }
}
