using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "World/NPC")]
public class NPC : ScriptableObject
{
    public string NPCName;
    public Sprite faceImg;
    public Dialog dialog;
}
