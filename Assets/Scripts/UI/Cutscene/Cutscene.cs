using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cutsceneText;
    [SerializeField] private RectTransform textRect;
    [SerializeField] private Image cutsceneImage;
    [SerializeField] private RectTransform imageRect;

    public void Start(){
        textRect = cutsceneText.gameObject.GetComponent<RectTransform>();
        imageRect = cutsceneImage.gameObject.GetComponent<RectTransform>();
    }

    public void SetText(string text){
        cutsceneText.text = text;
    }

    public void SetSpriteImage(Sprite cutsceneSprite){
        cutsceneImage.sprite = cutsceneSprite;
    }
}
