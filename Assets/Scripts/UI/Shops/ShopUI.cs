using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;
    private VendingMachine vendingMachine;
    [SerializeField] private GameObject popupUIPrefab;
    private RectTransform popupTransform;
    
    public void Awake()
    {
        if (instance == null) instance = this;
    }

    public Shop ShowPopup(){
        GameObject go = Instantiate(popupUIPrefab);
        go.transform.SetParent(transform, false);
        Shop purchasableItem = go.GetComponent<Shop>();
        Assert.IsNotNull(purchasableItem);

        return purchasableItem;
    }
}
