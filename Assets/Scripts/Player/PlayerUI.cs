using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;


    private void Start()
    {
        Player.Instance.OnMoneyChanged += Instance_OnMoneyChanged;
    }

    private void Instance_OnMoneyChanged(object sender, System.EventArgs e)
    {
        string formattedMoney = Player.Instance.Money.ToString("N0").Replace(',', ' ');
        moneyText.text = formattedMoney + "$";
    }
}
