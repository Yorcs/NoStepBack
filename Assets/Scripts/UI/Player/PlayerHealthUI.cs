using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour {
    
    [SerializeField] private List<Image> emptyHearts = new();
    [SerializeField] private List<Image> hearts = new();

    private int hpPerHeart = 100;

    public void SetHpPerHeart (int maxHitpoints) {
        hpPerHeart = maxHitpoints / hearts.Count;
    }

    public void SetHealth(int health) {
        float remainingHealth = health;
        foreach(Image heart in hearts) {
            heart.fillAmount = remainingHealth / hpPerHeart;
            remainingHealth -= 100;
            Mathf.Clamp(remainingHealth, 0, float.PositiveInfinity);
        }
    }

    public void SetHeartColor(Color color) {
        color.a = 1;
        foreach(Image heart in hearts) {
            heart.color = color;
        }
    }

}
