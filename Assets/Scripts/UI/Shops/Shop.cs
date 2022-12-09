using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private float yOffset = 6f;
    private VendingMachine vendingMachine;
    [SerializeField] private Image glowImage;
    [SerializeField] private Image popupImage;
    private RectTransform popupTransform;
    [SerializeField] private Image equipmentImage;
    private RectTransform equipmentTransform;
    [SerializeField] private TextMeshProUGUI priceText;
    private RectTransform priceTransform;

    public void Awake()
    {
        popupTransform = popupImage.gameObject.GetComponent<RectTransform>();
        equipmentTransform = equipmentImage.gameObject.GetComponent<RectTransform>();
        priceTransform = priceText.gameObject.GetComponent<RectTransform>();
    }

    public void SetPrice(int price)
    {
        priceText.text = price.ToString();
    }

    public void SetEquipmentSprite(Sprite equipmentSprite, Color color)
    {
        equipmentImage.sprite = equipmentSprite;
        glowImage.color = color;
    }

    public void SetPosition(Vector2 position)
    {
        position.y += yOffset;

        popupTransform.position = position;
    }
}
