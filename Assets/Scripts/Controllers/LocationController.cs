using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

public class LocationController : MonoBehaviour, Interactable
{
    [SerializeField] Map map;

    public void Interact(){
        UIController.Instance.ShowMap(map);
    }

}
