using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

public class DoorController : MonoBehaviour, Interactable
{
    [SerializeField] SceneList.Map scenes;
    
    public void Interact(){
        SceneController.Instance.AnimatedTransit(scenes);
    }
}
