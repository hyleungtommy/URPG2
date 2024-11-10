using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScene : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int letterPerSecond;
    [SerializeField] GameObject dialogCanvas;
    [SerializeField] Image NPCFace;
    [SerializeField] Text NPCName;
    public event Action OnShowDialog;
    public event Action OnHideDialog;
    public static DialogScene Instance{get; private set;}

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
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();
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
            if(currentLine < dialog.Lines.Count){
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }else{
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
}
