using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int letterPerSecond;
    [SerializeField] GameObject dialogCanvas;
    [SerializeField] Image NPCFace;
    [SerializeField] Text NPCName;
    public event Action OnShowDialog;
    public event Action OnHideDialog;
    public static DialogController Instance{get; private set;}

    private void Awake(){
        Instance = this;
    }

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;

    public void ShowDialog(NPC npc){
        StartCoroutine(DisplayDialog(npc));
    }

    public IEnumerator DisplayDialog(NPC npc){
        Debug.Log("show dialog");
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();
        Debug.Log(npc.faceImg);
        Debug.Log(NPCFace.sprite);
        NPCFace.sprite = npc.faceImg;
        NPCName.text = npc.NPCName;
        this.dialog = npc.dialog;
        dialogCanvas.SetActive(true);
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    
    public void HandleUpdate()
    {
        if(!isTyping){
            ++currentLine;
            Debug.Log("current line=" + currentLine);
            if(currentLine < dialog.Lines.Count){
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }else{
                Debug.Log("end dialog");
                dialogBox.SetActive(false);
                dialogCanvas.SetActive(false);
                currentLine = 0;
                OnHideDialog?.Invoke();
            }
        }
    }

    public IEnumerator TypeDialog(string line){
        isTyping = true;
        dialogText.text = "";
        foreach(var letter in line.ToCharArray()){
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
        isTyping = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
