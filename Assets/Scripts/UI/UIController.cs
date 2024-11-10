using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
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

    public void ShowDialog(NPC npcdata){
        SceneParameters.npcdata = npcdata;
        Game.ChangeGameState(Game.State.Dialog);
        SceneLoader.LoadUIScene(Scenes.Dialog);
    }

    public void ShowStatus(){
        Game.ChangeGameState(Game.State.OpenUI);
        SceneLoader.LoadUIScene(Scenes.Status);
    }

    public void ShowMap(Map _map){
        SceneParameters.mapData = _map;
        Game.ChangeGameState(Game.State.OpenUI);
        SceneLoader.LoadUIScene(Scenes.Map);
    }

    public void HideAllUI(){
        SceneLoader.UnloadUIScene(Scenes.Status);
        SceneLoader.UnloadUIScene(Scenes.Map);
        Game.ChangeGameState(Game.State.FreeRoam);
    }
}
