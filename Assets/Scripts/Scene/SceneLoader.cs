using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class SceneLoader
{
    public static SceneList.UI CurrentOpenedUIScene{
        private set; get;
    }

    public static SceneList.Map CurrentOpenedMapScene{
        private set; get;
    }
    
    public static void LoadMapScene(SceneList.Map sceneName){
        CurrentOpenedMapScene = sceneName;
        SceneManager.LoadScene(sceneName.ToString());
    }

    public static void LoadUIScene(SceneList.UI sceneName){
        CurrentOpenedUIScene = sceneName;
        SceneManager.LoadScene(sceneName.ToString(), LoadSceneMode.Additive);
    }

    public static void UnloadUIScene(){
        Scene scene = SceneManager.GetSceneByName(CurrentOpenedUIScene.ToString());
        if (scene.isLoaded)
        {
            // Unload the scene if it’s loaded
            SceneManager.UnloadSceneAsync(CurrentOpenedUIScene.ToString());
        }
    }

    public static void UnloadUIScene(SceneList.UI sceneName){
        Scene scene = SceneManager.GetSceneByName(sceneName.ToString());
        if (scene.isLoaded)
        {
            // Unload the scene if it’s loaded
            SceneManager.UnloadSceneAsync(sceneName.ToString());
        }
    }

    public static GameObject GetSceneObject(SceneList.UI sceneName){
        Scene scene = SceneManager.GetSceneByName(sceneName.ToString());
        if (scene.isLoaded)
        {
            return scene.GetRootGameObjects()[0];
        }
        return null;
    }

    public static GameObject GetSceneObject(SceneList.Map sceneName){
        Scene scene = SceneManager.GetSceneByName(sceneName.ToString());
        if (scene.isLoaded)
        {
            return scene.GetRootGameObjects()[0];
        }
        return null;
    }
}
