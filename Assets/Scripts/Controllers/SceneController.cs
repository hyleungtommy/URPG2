﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [SerializeField] Animator sceneTransitAnimator;
    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public void Transit(string sceneName){
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void AnimatedTransit(SceneList.Map sceneName){
        StartCoroutine(TransitWithAnimation(sceneName));
    }
    
    IEnumerator TransitWithAnimation(SceneList.Map sceneName){
        sceneTransitAnimator.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        SceneLoader.LoadMapScene(sceneName);
        sceneTransitAnimator.SetTrigger("Start");
    }
}
