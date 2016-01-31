﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISummonPanel : MonoBehaviour
{
    public Demon demon;
    public Image summonImage;
    public Text sellButtonText;

    public void Activate(Demon demon)
    {
        this.demon = demon;
        summonImage.sprite = demon.icon;
        sellButtonText.text = string.Format("Sell {0} [${1}]", demon.demonName, demon.worth);
        gameObject.SetActive(true);
    }

    public void SellDemon()
    {
        GameManager.instance.ChangeTotalMoney(demon.worth);
        gameObject.SetActive(false);
    }
}
