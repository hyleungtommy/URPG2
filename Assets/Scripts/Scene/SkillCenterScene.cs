using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class SkillCenterScene : MonoBehaviour, ISkillScene
{
    [SerializeField] BattleMemberList battleMemberList;
    [SerializeField] SkillTreePanel[] skillTreeList;
    [SerializeField] Text textSkillPts;
    [SerializeField] Text textMoney;
    [SerializeField] Text textSkillName;
    [SerializeField] Text textSkillDesc;
    [SerializeField] Button buttonLearnSkill;
    // Start is called before the first frame update
    void Start()
    {
        OnSelectCharacter(0, GameController.Instance.party.GetAllUnlockedCharacter()[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectSkill(Skill skill){
        textSkillName.text = skill.name;
        textSkillDesc.text = skill.desc;
        buttonLearnSkill.gameObject.SetActive(skill.CanLearn(battleMemberList.SelectedCharacter.job));
    }

    public void OnClickBox(int id){
        foreach(SkillTreePanel skillTree in skillTreeList){
            skillTree.gameObject.SetActive(false);
        }
        battleMemberList.OnClickBox(id);
        OnSelectCharacter(id, battleMemberList.SelectedCharacter);
    }

    private void OnSelectCharacter(int id, BattleCharacter character)
    {
        if (character != null)
        {
            int jobId = character.job.id;
            foreach(SkillTreePanel skillTree in skillTreeList){
                skillTree.gameObject.SetActive(false);
            }
            skillTreeList[jobId - 1].gameObject.SetActive(true);
            skillTreeList[jobId - 1].Init(this, character.job);
            textSkillPts.text = character.skillPtsAvailable.ToString();

        }
    }
}
