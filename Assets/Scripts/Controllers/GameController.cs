using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Party party;
    public Map defaultMap;
    // Start is called before the first frame update


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        party.Init();
        Game.currLoc = defaultMap;
    }

    // Update is called once per frame
    void Update()
    {
        //All Input keys should be assgined here to prevent key conflict
        if (Game.state == Game.State.Battle) //All keys are disabled on battle
        {
            return;
        }
        else if (Game.state == Game.State.Dialog)// Only dialog keys can be press during a dialog
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneLoader.GetSceneObject(SceneList.UI.Dialog)?.GetComponent<DialogScene>().HandleUpdate();
            }
        }
        else if (Game.state == Game.State.OpenUI)// Can only press escape when opened a UI
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIController.Instance.HideAllUI();
            }
        }
        else if (Game.state == Game.State.FreeRoam)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                UIController.Instance.ShowStatus();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<PlayerController>()?.Interact();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                UIController.Instance.ShowInventory();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                UIController.Instance.ShowSkillScene();
            }
            if(Game.devMode){
                if(Input.GetKeyDown(KeyCode.J)){
                    UIController.Instance.ShowShop();
                }else if(Input.GetKeyDown(KeyCode.K)){
                    UIController.Instance.ShowSkillCenter();
                }
            }
        }
    }
}
