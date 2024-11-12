using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

public class DoorController : MonoBehaviour, Interactable
{
    [SerializeField] Scenes scenes;
    
    public void Interact(){
        SceneController.Instance.AnimatedTransit(scenes);
    }
}
