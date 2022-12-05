using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerRespawner : MonoBehaviour
{
    private float yOffset = 2.5f;
    [SerializeField] TextMeshProUGUI respawnText;
    [SerializeField] private RectTransform textRect;


    public void SetRespawn(float respawn)
    {
        respawnText.text = respawn.ToString();
    }
    public void SetPosition(Vector2 position)
    {
        position.y += yOffset;

        textRect.position = position;
    }
}
