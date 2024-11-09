using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
public class BasicScene : MonoBehaviour
{

    // public void jumpToScene(string sceneName)
    // {
    //     PlotData pd = null;
    //     if (this is BattleScene)
    //     {

    //     }
    //     else
    //     {
    //         Debug.Log("match plot, pt = " + Game.plotPt);
    //         pd = PlotMatcher.matchPlot(sceneName);
    //     }


    //     if (pd != null)
    //     {
    //         PlotMatcher.nextScene = sceneName;
    //         PlotMatcher.nextPlot = pd;
    //         SceneManager.LoadScene(SceneName.Dialog);
    //     }
    //     else
    //     {
    //         SceneManager.LoadScene(sceneName);
    //     }
    // }

    public virtual void onSelectCharacter(int id, BattleCharacter character)
    {

    }
}
