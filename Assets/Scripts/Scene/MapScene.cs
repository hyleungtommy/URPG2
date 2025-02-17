﻿using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapScene : MonoBehaviour
{
    public Text mapName;
    public Image mapImg;
    public Text mapDesc;
    public Map map {get; set;}

    public void Awake(){
        map = SceneParameters.mapData;
        if(map != null){
            Render();
        }
    }

    public void Render(){
        mapName.text = map.name;
        mapImg.sprite = map.bgImg;
        mapDesc.text = "Recommand Lv." + map.reqLv + " ~ " + map.maxLv + "\n" +
                        map.desc + "\n\n" +
                        "Current Zone" + map.currZone + "/" + map.maxZone + "\n\n" +
                        "Not yet cleared";
    }

    public void OnClickExploreMode(){
        LoadBattleScene(Game.MapMode.Explore);
    }

    public void OnClickProgressiveMode(){
        LoadBattleScene(Game.MapMode.Progression);
    }

    private void LoadBattleScene(Game.MapMode mapMode){
        Game.currLoc = map;
        Game.currentMapMode = mapMode;
        Game.ChangeGameState(Game.State.Battle);
        UIController.Instance.HideAllUI();
        SceneManager.LoadScene("Battle");
    }
}
