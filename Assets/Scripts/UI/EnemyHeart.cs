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
    }

    public void SetPosition (Vector2 position) {
        position.y += yOffset;

        heartRect.position = position;
    }

}
