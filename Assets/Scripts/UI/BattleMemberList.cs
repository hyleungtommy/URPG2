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
        list = GameController.Instance.party.GetAllUnlockedCharacter();
        Render();
    }

    public void Render()
    {
        list = GameController.Instance.party.GetAllUnlockedCharacter();
        Debug.Log(boxes.Length + "=" + list.Length);
        for (int i = 0; i < boxes.Length; i++)
        {
            
            boxes[i].Render(list[i]);
        }
    }

    public void OnClickBox(int id)
    {
        selectedCharacterId = id;
        Render();
    }
}
