using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes{
    Battle, Dialog, Map, Status, World, ForestVillage, Inventory, Shop
}
public static class SceneLoader
{
    public static void LoadScene(Scenes sceneName){
        SceneManager.LoadScene(sceneName.ToString());
    }

    private static Scenes currentOpenScene;

    public static void LoadUIScene(Scenes sceneName){
        currentOpenScene = sceneName;
        SceneManager.LoadScene(sceneName.ToString(), LoadSceneMode.Additive);
    }

    public static void UnloadUIScene(){
        Scene scene = SceneManager.GetSceneByName(currentOpenScene.ToString());
        if (scene.isLoaded)
        {
            // Unload the scene if it’s loaded
            SceneManager.UnloadSceneAsync(currentOpenScene.ToString());
        }
    }

    public static void UnloadUIScene(Scenes sceneName){
        Scene scene = SceneManager.GetSceneByName(sceneName.ToString());
        if (scene.isLoaded)
        {
            // Unload the scene if it’s loaded
            SceneManager.UnloadSceneAsync(sceneName.ToString());
        }
    }

    public static GameObject GetSceneObject(Scenes sceneName){
        Scene scene = SceneManager.GetSceneByName(sceneName.ToString());
        if (scene.isLoaded)
        {
            return scene.GetRootGameObjects()[0];
        }
        return null;
    }
}
