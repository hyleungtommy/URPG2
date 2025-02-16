using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreePanel : MonoBehaviour
{
    [SerializeField] SkillBox[] skillBoxList;
    private ISkillScene skillScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(ISkillScene skillScene, Job job){
        foreach(SkillBox skillBox in skillBoxList){
            skillBox.GetComponent<Button>().onClick.AddListener(() => skillScene.OnSelectSkill(skillBox.skill));
        }
        this.skillScene = skillScene;
    }
}
