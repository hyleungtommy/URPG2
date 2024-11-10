using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public StatusScene statusScene;
    public MapScene mapScene;
    public DialogScene dialogScene;
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
        dialogScene.OnShowDialog += Game.DisablePlayerControl;
        dialogScene.OnHideDialog += Game.EnablePlayerControl;
    }

    void Destroy()
    {
        dialogScene.OnShowDialog -= Game.DisablePlayerControl;
        dialogScene.OnHideDialog -= Game.EnablePlayerControl;
    }

    public void ShowDialog(NPC npcdata){
        dialogScene.gameObject.SetActive(true);
        dialogScene.ShowDialog(npcdata);
    }

    public void ShowStatus(){
        statusScene.gameObject.SetActive(true);
    }

    public void ShowMap(Map _map){
        mapScene.gameObject.SetActive(true);
        mapScene.map = _map;
        mapScene.Render();
    }

    public void HideAllScene(){
        statusScene.gameObject.SetActive(false);
        mapScene.gameObject.SetActive(false);
    }
}
