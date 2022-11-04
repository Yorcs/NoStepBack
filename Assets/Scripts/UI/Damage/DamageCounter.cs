using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageCounter : MonoBehaviour {
    private Vector2 velocity;
    private Color color;
    private TextMeshProUGUI textmeshProUGUI;


    public void SetVelocity(Vector2 velocity) {
        this.velocity = velocity;
    }

    public void SetColor(Color newColor) {
        color = newColor;
    }
}
