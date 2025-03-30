using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class SkillCenterScene : MonoBehaviour, ISkillScene
{
    [SerializeField] BattleMemberList battleMemberList;
    [SerializeField] SkillTreePanel[] skillTreeList;
    [SerializeField] Text textSkillPrice;
    [SerializeField] Text textSkillPtsAvailable;
    [SerializeField] Text textMoney;
    [SerializeField] Text textSkillName;
    [SerializeField] Text textSkillDesc;
    [SerializeField] Button buttonLearnSkill;
    [SerializeField] Text textErrorMessage;
    [SerializeField] GameObject RightPanel;
    // Start is called before the first frame update
    private Skill SelectedSkill;
    void Start()
    {
        Render();
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
            textSkillPrice.text = SelectedSkill.price.ToString();

            //Check if character can learn skill, if not, show error message
            string errorMsg = CanLearnSkill(SelectedSkill);
            if (errorMsg != "")
            {
                buttonLearnSkill.gameObject.SetActive(false);
                textErrorMessage.gameObject.SetActive(true);
                textErrorMessage.text = errorMsg;
            }
            else
            {
                buttonLearnSkill.gameObject.SetActive(true);
                textErrorMessage.gameObject.SetActive(false);
            }
    }

    private string CanLearnSkill(Skill skill)
    {

        if (!skill.CanLearn(battleMemberList.SelectedCharacter.job))
        {
            return "Need to learn prerequisite skills";
        }
        if (battleMemberList.SelectedCharacter.skillPtsAvailable < skill.skillPts)
        {
            return "Skill Points not enough";
        }
        if (Game.money < skill.price)
        {
            return "Money not enough";
        }
        if (battleMemberList.SelectedCharacter.lv < skill.reqLv)
        {
            return "Level not enough";
        }

        return "";
    }

    private string GetSkillDescription(Skill skill)
    {
        return skill.GetSkillType() +
        "\n" + "Require Lv." + skill.reqLv +
        "\n" + "Skill Points: " + skill.skillPts +
        "\n" + "Price: " + skill.price +
        "\n\n" + skill.desc.Replace("%mod%", (skill.modifier * 100).ToString() + "%");
    }

    public void OnLearnSkill()
    {
        if (SelectedSkill != null)
        {
            battleMemberList.SelectedCharacter.skillPtsSpent += SelectedSkill.skillPts;
            Game.money -= SelectedSkill.price;
            SelectedSkill.Learn();
            Render();
            RenderRightPanel();
            OnSelectCharacter(battleMemberList.SelectedCharacter.id, battleMemberList.SelectedCharacter, false);
        }
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
            textSkillPtsAvailable.text = character.skillPtsAvailable.ToString();

        }
    }

    private void Render()
    {
        textMoney.text = Game.money.ToString();
    }
}
