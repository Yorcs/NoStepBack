using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoneyUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI moneyText;

    public void SetMoney(int money) {
        moneyText.text = money.ToString();
    }
}
