using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public Text fpsText;
    private float deltaTime = 0.0f;
    void Awake(){
        if(Instance == null){
            Instance = this;
            StartCoroutine(UpdateFPS());
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    void Update(){
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    private IEnumerator UpdateFPS()
    {
        while (true)
        {
            float fps = 1.0f / deltaTime;
            fpsText.text = $"FPS: {fps:0.}";
            yield return new WaitForSeconds(0.5f); // Update every 0.5 seconds
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
