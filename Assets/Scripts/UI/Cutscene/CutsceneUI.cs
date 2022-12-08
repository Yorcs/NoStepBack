using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class CutsceneUI : MonoBehaviour
{
    [SerializeField] private GameObject cutsceneUIPrefab;

    public Cutscene MakeCutscene(int gameState, int textState){
        GameObject go = Instantiate(cutsceneUIPrefab);
        go.transform.SetParent(transform, false);
        Cutscene cutscene = go.GetComponent<Cutscene>();
        switch(gameState){
            case 0:
            switch(textState){
                case 0:
                    //cutscene.SetSpriteImage();
                    //cutscene.SetText();
                    break;
                case 1:
                    //cutscene.SetSpriteImage();
                    //cutscene.SetText();
                    break;
                case 2:
                    break;
            }
                break;
            case 1:
            switch(textState){
                case 0:
                    // cutscene.SetSpriteImage();
                    // cutscene.SetText();
                    break;
                case 1:
                    //cutscene.SetSpriteImage();
                    //cutscene.SetText();
                    break;
                case 2:
                    break;
            }
                break;
            case 2:
            switch(textState){
                case 0:
                    // cutscene.SetSpriteImage();
                    // cutscene.SetText();
                    break;
                case 1:
                    // cutscene.SetSpriteImage();
                    // cutscene.SetText();
                    break;
                case 2:
                    break;
            }
                break;
        }
        Assert.IsNotNull(cutscene);
        return cutscene;
    }
}
