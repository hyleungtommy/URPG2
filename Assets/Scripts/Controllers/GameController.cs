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
        if(Game.state == Game.State.Battle){
            return;
        }
        //All Input keys should be assgined here to prevent key conflict
        if (Input.GetKeyDown(KeyCode.E))
        {
            UIController.Instance.ShowStatus();
            Game.DisablePlayerControl();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIController.Instance.HideAllPanel();
            Game.EnablePlayerControl();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Game.state == Game.State.Dialog)
            {
                UIController.Instance.dialog.HandleUpdate();
            }
            else if (Game.state == Game.State.FreeRoam)
            {
                FindObjectOfType<PlayerController>()?.Interact();
            }
        }
    }
}
