using System.Collections;
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

    public void Render(){
        mapName.text = map.name;
        mapImg.sprite = map.bgImg;
        mapDesc.text = "Recommand Lv." + map.reqLv + " ~ " + map.maxLv + "\n" +
                        map.desc + "\n\n" +
                        "Current Zone" + map.currZone + "/" + map.maxZone + "\n\n" +
                        "Not yet cleared";
    }

    public void OnClickExploreMode(){
        Game.currLoc = map;
        Game.currentMapMode = Constant.MapModeExplore;
        Game.state = Game.State.Battle;
        UIController.Instance.HideAllPanel();
        SceneController.Instance.DisableSceneTransitCanvas();
        SceneManager.LoadScene("Battle");
    }

    public void OnClickProgressiveMode(){
        Game.currLoc = map;
        Game.currentMapMode = Constant.MapModeProgressive;
        Game.state = Game.State.Battle;
        UIController.Instance.HideAllPanel();
        SceneController.Instance.DisableSceneTransitCanvas();
        SceneManager.LoadScene("Battle");
    }
}
