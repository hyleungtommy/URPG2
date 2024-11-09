using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
public class BattleMemberList : MonoBehaviour
{
    public BasicBox[] boxes;
    BattleCharacter[] list;
    int selectedCharacterId;
    public BattleCharacter SelectedCharacter {get{ return list[selectedCharacterId];}}
    // Start is called before the first frame update
    void Start()
    {
        list = GameController.Instance.party.getAllUnlockedCharacter();
        render();
        //onClickBox(0);
        // if (scene != null && (scene is SkillScene || scene is EquipmentScene))
        // {
        //     onClickBox(Game.selectedCharacterInStatusScene);
        // }
        // else
        // {
        //     onClickBox(0);
        // }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void render()
    {
        list = GameController.Instance.party.getAllUnlockedCharacter();
        Debug.Log(boxes.Length + "=" + list.Length);
        for (int i = 0; i < boxes.Length; i++)
        {
            
            boxes[i].render(list[i]);
        }
    }

    public void onClickBox(int id)
    {
        selectedCharacterId = id;
        render();
    }
}
