using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class CutsceneUI : MonoBehaviour
{
    [SerializeField] private Cutscene cutscene;

    public void Awake() {
        Assert.IsNotNull(cutscene);
        cutscene.gameObject.SetActive(false);
    }

    public void PlayCutscene(CutsceneSO cutsceneScript){
        cutscene.gameObject.SetActive(true);
        cutscene.SetScript(cutsceneScript);
    }

    public void AdvanceCutscene() {
        if(cutscene.gameObject.activeInHierarchy == false) return;

        if(cutscene.IsCutsceneOver()) {
            GameFlowManager.instance.StartLevel();
            cutscene.gameObject.SetActive(false);
            return;
        }

        cutscene.AdvanceCutscene();
    }
}
