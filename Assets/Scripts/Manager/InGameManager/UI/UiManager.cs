using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UiManager : MonoBehaviour
{
    private static UiManager instance;
    public static UiManager Instance {  get { return instance; } }

    private PlayerUI playerUI;
    public static PlayerUI PlayerUI { get { return Instance.playerUI; } }

    private InventoryUI inventoryUI;
    public static InventoryUI InventoryUI { get { return Instance.inventoryUI; } }

    private ShopUI shopUI;
    public static ShopUI ShopUI { get { return Instance.shopUI; } }

    private BossUI bossUI;
    public static BossUI BossUI { get { return Instance.bossUI; } }



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        playerUI = GetComponentInChildren<PlayerUI>();
        inventoryUI = GetComponentInChildren<InventoryUI>();
        shopUI = GetComponentInChildren<ShopUI>();
        bossUI = GetComponentInChildren<BossUI>();
    }

    public abstract void UpdateUI();
    public abstract void ActiveUI();
}
