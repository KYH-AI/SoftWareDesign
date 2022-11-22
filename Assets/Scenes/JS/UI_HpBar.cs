using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_HpBar : MonoBehaviour
{
    Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = transform.GetComponent<Slider>();
        UIManager.eventHandler.AddListener(UIEventHandler.UIEventType.UpdatePlayerHpBar, OnUpdateHpBar);
        UIManager.eventHandler.AddListener(UIEventHandler.UIEventType.UpdateEnemyHpBar, OnUpdateHpBar);
    }

    public void OnUpdateHpBar(UIEventHandler.UIEventType eventType, Component sender, object param = null)
    {
        //Mangers.UImanager.UpdateHpBar();
    }
}
