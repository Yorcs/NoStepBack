using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CutsceneScriptableObject", order = 1)]
public class CutsceneSO : ScriptableObject {
    public Sprite characterImage;
    public List<string> text = new();
}
