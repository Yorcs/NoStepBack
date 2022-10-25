using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour {
    
    [SerializeField] private List<RawImage> emptyHearts = new();
    [SerializeField] private List<RawImage> hearts = new();

    public void SetHealth(int health) {
        for(int i = 0; i < hearts.Count; i++) {
            hearts[i].gameObject.SetActive(i < health);
        }
    }

}
