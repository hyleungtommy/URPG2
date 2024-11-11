using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
public class MerchantController : MonoBehaviour, Interactable
{
    public void Interact(){
        UIController.Instance.ShowShop();
    }
}
