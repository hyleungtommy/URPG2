using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class SkillScene : MonoBehaviour, ISkillScene
{
    [SerializeField] BattleMemberList battleMemberList;
    [SerializeField] SkillTreePanel[] skillTreeList;
    [SerializeField] Text textSkillPtsAvailable;
    [SerializeField] Text textSkillName;
    [SerializeField] Text textSkillDesc;
    [SerializeField] GameObject RightPanel;
    // Start is called before the first frame update
    private Skill SelectedSkill;
    void Start()
    {
        RightPanel.SetActive(false);
        OnSelectCharacter(0, GameController.Instance.party.GetAllUnlockedCharacter()[0]);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSelectSkill(Skill skill)
    {
        RightPanel.SetActive(true);
        SelectedSkill = battleMemberList.SelectedCharacter.SkillList.Find(x => x.id == skill.id);
        if (SelectedSkill != null)
        {
            RenderRightPanel();
        }
    }

    private void RenderRightPanel(){
        textSkillName.text = SelectedSkill.skillName + " Lv." + SelectedSkill.skillLv;
        textSkillDesc.text = GetSkillDescription(SelectedSkill);
    }


    private string GetSkillDescription(Skill skill)
    {
        return skill.GetSkillType() +
        "\n" + "MP: " + skill.reqMp +
        "\n" + "Cooldown: " + skill.cooldown +
        "\n\n" + skill.desc.Replace("%mod%", (skill.modifier * 100).ToString() + "%");
    }

    public void OnClickBox(int id)
    {
        
        foreach (SkillTreePanel skillTree in skillTreeList)
        {
            skillTree.gameObject.SetActive(false);
        }
        battleMemberList.OnClickBox(id);
        OnSelectCharacter(id, battleMemberList.SelectedCharacter);
    }

    private void OnSelectCharacter(int id, BattleCharacter character, bool hideRightPanel = true)
    {
        
        if (character != null)
        {
            if (hideRightPanel)
            {
                RightPanel.SetActive(false);
            }
            int jobId = character.job.id;
            foreach (SkillTreePanel skillTree in skillTreeList)
            {
                skillTree.gameObject.SetActive(false);
            }
            skillTreeList[jobId - 1].gameObject.SetActive(true);
            skillTreeList[jobId - 1].Init(this, character.job);
            textSkillPtsAvailable.text = character.skillPtsAvailable.ToString()  + "/" + character.skillPtsEarned;

        }
    }
}
