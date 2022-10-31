using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeart : MonoBehaviour {

    [SerializeField] private Image heartImage;
    [SerializeField] private RectTransform heartRect;

    private float yOffset = 2.5f;


    public void SetHealthPercent(float healthPercent) {
        heartImage.fillAmount = healthPercent;
        switch(healthPercent) {
            case < 0.25f:
                heartImage.color = Color.red;
                break;
            case < 0.5f:
                heartImage.color = Color.yellow;
                break;
            default:
                break;
        }
    }

    public void SetPosition (Vector2 position) {
        position.y += yOffset;

        heartRect.position = position;
    }

}
