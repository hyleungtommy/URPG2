using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitController : MonoBehaviour
{
    [SerializeField] Scenes sceneName;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            SceneController.Instance.AnimatedTransit(sceneName);
        }
    }
}
