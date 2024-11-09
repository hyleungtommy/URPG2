using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] NPC npcData;
    public void Interact(){
        UIController.Instance.ShowDialog(npcData);
    }
}
