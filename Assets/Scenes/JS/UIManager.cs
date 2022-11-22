using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : Managers
{
    public Slider playerSlider;
    public Slider bossSlider;
    public Text goldAmount;

    public void SetCanvas(GameObject gameObject, bool set)
    {

    }

    public void ClosePopUpUI()
    {

    }

    public void UpdatePlayerHpSlider(float currentHp, float maxHp)
    {
        playerSlider.value = currentHp / maxHp;
    }

    public void UpdateBossHpSlider(float currentHp, float maxHp)
    {
        bossSlider.value = currentHp / maxHp;
    }

    public void UpdateGoldText()
    {
        goldAmount.text = Managers.Player.PlayerGold.ToString();
    }

    public void temp()
    {

    }
}
