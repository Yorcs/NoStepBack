using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System;

public class Cutscene : MonoBehaviour
{
    private CutsceneSO script;
    private int scriptPosition;
    [SerializeField] TextMeshProUGUI cutsceneText;
    [SerializeField] private RectTransform textRect;
    [SerializeField] private Image cutsceneImage;
    [SerializeField] private RectTransform imageRect;

    public void Start(){
        textRect = cutsceneText.gameObject.GetComponent<RectTransform>();
        imageRect = cutsceneImage.gameObject.GetComponent<RectTransform>();
    }

    public void AdvanceCutscene() {
        if(scriptPosition + 1 < script.text.Count) {
            scriptPosition++;
            SetText(script.text[scriptPosition]);
        }
    }

    public bool IsCutsceneOver() {
        return scriptPosition + 1 >= script.text.Count;
    }

    public void SetText(string text){
        cutsceneText.text = text;
    }

    public void SetSpriteImage(Sprite cutsceneSprite){
        cutsceneImage.sprite = cutsceneSprite;
    }

    internal void SetScript(CutsceneSO cutsceneScript)
    {
        script = cutsceneScript;
        SetSpriteImage(script.characterImage);
        scriptPosition = 0;
        SetText(script.text[0]);
    }
}
