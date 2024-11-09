using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public GameObject status;
    public MapScene map;
    public DialogController dialog;
    public static UIController Instance;
    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dialog.OnShowDialog += Game.DisablePlayerControl;
        dialog.OnHideDialog += Game.EnablePlayerControl;
    }

    void Destroy()
    {
        dialog.OnShowDialog -= Game.DisablePlayerControl;
        dialog.OnHideDialog -= Game.EnablePlayerControl;
    }

    public void ShowDialog(NPC npcdata){
        dialog.gameObject.SetActive(true);
        dialog.ShowDialog(npcdata);
    }

    public void ShowStatus(){
        status.gameObject.SetActive(true);
    }

    public void ShowMap(Map _map){
        map.gameObject.SetActive(true);
        map.map = _map;
        map.Render();
    }

    public void HideAllPanel(){
        status.gameObject.SetActive(false);
        map.gameObject.SetActive(false);
    }
}
