using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [SerializeField] Animator sceneTransitAnimator;
    [SerializeField] GameObject sceneTransitCanvas;
    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //due to transit canvas blocking other canvas it needs to be disable when inside some scene such as battle
    public void DisableSceneTransitCanvas(){
        sceneTransitCanvas.gameObject.SetActive(false);
    }

    public void EnableSceneTransitCanvas(){
        sceneTransitCanvas.gameObject.SetActive(true);
    }

    public void Transit(string sceneName){
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void AnimatedTransit(string sceneName){
        
        StartCoroutine(TransitWithAnimation(sceneName));
    }
    
    IEnumerator TransitWithAnimation(string sceneName){
        sceneTransitAnimator.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(sceneName);
        sceneTransitAnimator.SetTrigger("Start");
    }
}
