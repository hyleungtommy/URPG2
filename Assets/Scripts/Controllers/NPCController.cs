using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] NPC npcData;
    public void Interact(){
        UIController.Instance.ShowDialog(npcData);
    }
}
