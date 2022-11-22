using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : Mangers
{
    public static UIEventHandler eventHandler = new UIEventHandler();

    public void SetCanvas(GameObject gameObject, bool set)
    {

    }

    public void ClosePopUpUI()
    {

    }

    public void UpdateHpBar()
    {
        // 슬라이더 value = Managers.player.현재체력 / Max 체력 
    }
}
