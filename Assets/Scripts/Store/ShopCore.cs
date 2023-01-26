using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ShopCore : MonoBehaviour
{
    [Header("���� ��� Ŭ����")]
    private RandomProcess randomProcess = new RandomProcess();
    
    [Header("���� �ֻ��� Ŭ����")]
    internal DiceProcess diceProcess = new DiceProcess();
    
    [Header("���� ������ Ŭ����")]
    [SerializeField] internal PotionProcess potionProcess = new PotionProcess();
    
    [Header("��ų ������ Ŭ����")]
    [SerializeField] internal SkillProcess skillProcess = new SkillProcess();

    [Header("���� UI")] [SerializeField] private ShopUI shopUI;
    
    private void Start()
    {
        shopUI = new ShopUI(this, this);
        InitPlayerStatUI();
        InitSkillList();
        InitRandomDice();
    }
    
    /// <summary>
    /// Player ���� ���� UI �ʱ�ȭ
    /// </summary>
    private void InitPlayerStatUI()
    {
        shopUI.InitPlayerStatUI(
            Managers.StageManager.Player.MoveSpeed,
            Managers.StageManager.Player.MaxHp,
            Managers.StageManager.Player.DefaultAttackDamage,
            Managers.StageManager.Player.Armor);
        
        shopUI.InitPlayerGoldUI(Managers.StageManager.Player.PlayerGold);
    }

    private void InitSkillList()
    {
        skillProcess.SetRandomSkillList();
        shopUI.InitRandomSKillList(skillProcess.SkillList);
    }
    
    private void InitRandomDice()
    {
         diceProcess.SetRandomDice();
      //  shopUI.InitRandomDiceUI(diceProcess.GetDice);
    }
    
}


[Serializable]
internal class ShopUI 
{
    #region �÷��̾� ���� UI
    [SerializeField] private Text playerMoveSpeedText;
    [SerializeField] private Text playerMaxHpText;
    [SerializeField] private Text playerDefaultAttackDmgText;
    [SerializeField] private Text playerArmorText;
    #endregion

    #region ���� ���� �ֻ��� UI
    [SerializeField] private Image buffImage;
    [SerializeField] private Text diceResultText;
    [SerializeField] private Button randomDiceButton;
    #endregion

    #region ���� ��ų UI
    [SerializeField] private Image[] skillImages = new Image[3];
    [SerializeField] private Text[] skillNames = new Text[3];
    #endregion

    #region ���� Ȯ�� UI
    [SerializeField] private GameObject itemConfirmationPanel;
    [SerializeField] private Text itemIntroductionText;
    [SerializeField] private Button itemBuyButton;
    [SerializeField] private Text buyItemResultText;
    #endregion
    
    #region ������ ������ ���� UI
    [SerializeField] private GameObject itemDetailPanel;
    [SerializeField] private Text itemDetailText; 
    #endregion

    #region  ������ ���� ��ư
    [SerializeField] private Button[] itemSelectButtons = new Button[5];
    #endregion

    #region �÷��̾� ��ȭ UI
    [SerializeField] private Text playerGoldText;
    #endregion

    #region ���� ��ư �̺�Ʈ
    private MonoBehaviour monoBehaviour;
    private ShopCore shopCore;
    #endregion
    

    public ShopUI(MonoBehaviour monoBehaviour, ShopCore shopCore)
    {
        this.monoBehaviour = monoBehaviour;
        this.shopCore = shopCore;
    }

    private void InitButtonEvent()
    {
        for (var i = 0; i < itemSelectButtons.Length; i++)
        {
            var index = i;         /* Closure Problem */
            itemSelectButtons[i].onClick.AddListener( () => ItemSelected(index));
        }
    }
    
    internal void InitPlayerStatUI(float playerMoveSpeed, int playerMaxHp, int playerDefaultAttack, int playerArmor)
    {
        playerMoveSpeedText.text = playerMoveSpeed.ToString();
        playerMaxHpText.text = playerMaxHp.ToString();
        playerDefaultAttackDmgText.text = playerDefaultAttack.ToString();
        playerArmorText.text = playerArmor.ToString();
    }

    internal void InitPlayerGoldUI(int playerGold)
    {
        playerGoldText.text = playerGold.ToString();
    }

    internal void InitRandomDiceUI(Image buffImage, string buffResult)
    {
        this.buffImage = buffImage; 
        diceResultText.text = buffResult;
    }

    internal void InitRandomSKillList(ItemStat[] skillstats)//Image[] skillImages, string[] skillNames)
    {
        for (var i = 0; i < this.skillImages.Length; i++)
        {
            this.skillImages[i].sprite = skillstats[i].ItemImage.sprite;
            this.skillNames[i].text = skillstats[i].ItemIntroduction;
        }
    }
    
    
    /// <summary>
    /// Buy ��ư Ŭ�� �Լ�
    /// </summary>
    public void BuyItemButtonEvent(Item buyEvent, int itemIndex)
    {
        int playerGold = Managers.StageManager.Player.PlayerGold;
        monoBehaviour.StartCoroutine(ViewResultText(buyEvent.BuyItem(itemIndex, ref playerGold), playerGold));
        Managers.StageManager.Player.PlayerGold = playerGold;
    }

    private IEnumerator ViewResultText(string resultText, int playerGold)
    {
        buyItemResultText.text = resultText;
        playerGoldText.text = playerGold.ToString();
        
        buyItemResultText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        buyItemResultText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Buy ��ư�� ������ ��� ��ư Ŭ�� �Լ�
    /// </summary>
    private void ItemSelected(int itemIndex)
    {
        // ���� ��ư �̺�Ʈ �����ü
        itemBuyButton.onClick.RemoveAllListeners();
        
        // ������ �󼼼��� â Ȱ��ȭ
        itemConfirmationPanel.SetActive(true);
        itemIntroductionText.text = Item.ItemList[itemIndex].ItemIntroduction;
        /*
          1. ������ ���� â Ȱ��ȭ
          1-1. <"������ �̸�"> "������ ����"
        */

        // ���� Ȯ�� â Ȱ��ȭ
        itemDetailPanel.SetActive(true);
        itemDetailText.text = ItemConfirmationText(Item.ItemList[itemIndex].ItemName, 
                                                   Item.ItemList[itemIndex].ItemPrice = Item.MeasureItemPrice(Item.ItemList[itemIndex]) );
        /*
          2. ���� Ȯ�� â Ȱ��ȭ
          2-1. <"������ �̸�>" "���� : ? �����Ͻðڽ��ϱ�?
        */

        
        /* Item Type���� 3������ ������ switch�� Ȯ���� BuySkill, BuyStat, BuyPotion �� Ŭ������ �̺�Ʈ ��� */
        switch (Item.ItemList[itemIndex].ItemType)
        {
            case Define.ItemType.Skill : 
                    itemBuyButton.onClick.AddListener( () => BuyItemButtonEvent(shopCore.skillProcess, itemIndex) ); break;
            case Define.ItemType.Stat : 
                    itemBuyButton.onClick.AddListener( () => BuyItemButtonEvent(shopCore.diceProcess, itemIndex) ); break;
            case Define.ItemType.Potion : 
                    itemBuyButton.onClick.AddListener( () => BuyItemButtonEvent(shopCore.potionProcess, itemIndex) ); break;
            default: Debug.Log("������ Ÿ�� ����"); break;
        }
        
    }

    private string ItemConfirmationText(string itemName, int itemPrice)
    {
        return "<" + itemName + "> ����:" + itemPrice.ToString() + " �����Ͻðڽ��ϱ�?";
    }
}


[Serializable]
internal class DiceProcess : Item
{
    [SerializeField] private ItemStat playerStatItem; 
    private readonly int PLAYER_MAX_STAT_LIST = 4;

    internal void SetRandomDice()
    {
        
    }
    
    public override string BuyItem(int itemIndex, ref int playerGold)
    {
        return base.BuyItem(itemIndex, ref playerGold);
    }
    
    /*
    private <T> SetDeBuff(T debuffValue)
    {
        return T;
    }

    private SetBuff()
    {
        
    }
    */

    /*
       ü�� (int)
       ���� (int)
       ���ݷ� (int)
       �̵��ӵ� (float)
     */
}

[Serializable]
internal class SkillProcess : Item
{
    [SerializeField] private ItemStat[] allSkillList;
    public ItemStat[] SkillList { get; private set; } = new ItemStat[3];

    private GameObject skillObject;
    private readonly string DEFAULT_SKILL_PATH = "Player_Skill/";
    private readonly string SKILL_BUY_RESULIT_TEXT = "��ų ���� �Ϸ�";
    private readonly string SKILL_UPGRADE_RESULIT_TEXT = "��ų ��ȭ �Ϸ�";
    private readonly string SKILL_TYPE_ERROR_TEXT = "��ų Ÿ�� ����";
    
    internal void SetRandomSkillList()
    {
        for (var i = 0; i < SkillList.Length; i++)
        {
            SkillList[i] = allSkillList[RandomProcess.RandomSkillList(allSkillList.Length)];
            ItemList[i] = SkillList[i]; // 0 ~ 2 ���� ��ų ������ ����
        }
    }

    public override string BuyItem(int itemIndex, ref int playerGold)
    {
        itemResultText = base.BuyItem(itemIndex, ref playerGold);
        
        if (itemResultText.Equals(string.Empty))
        {
            // ����
            if (!Managers.StageManager.Player.skillList.TryGetValue(SkillList[itemIndex].ItemName, out PlayerSkill updateSkill))
            {
                skillObject = MakeSkillObject(SkillList[itemIndex].ItemName);
                GameObject newSkillObject = Object.Instantiate(skillObject, Managers.StageManager.Player.transform);
                

                if (newSkillObject.TryGetComponent(out ActiveSkill activeSkill))
                {
                    activeSkill.Init(Managers.StageManager.Player);
                    Managers.StageManager.Player.playerActiveSkills.Add(Managers.StageManager.Player.ActiveSkillSlot_Index++, activeSkill);
                    Managers.UI.UpdateActiveSkills(SkillList[itemIndex].ItemImage, activeSkill);
                   // print("��ų����" + selectImage.ToString() + activeSkill.ToString());
                }
                else if(newSkillObject.TryGetComponent(out PassiveSkill passiveSkill))
                {
                    passiveSkill.Init(Managers.StageManager.Player);
                    passiveSkill.OnActive();
                    Managers.UI.UpdatePassiveSkills(SkillList[itemIndex].ItemImage, passiveSkill);
                }
                else
                {
                    Debug.Log("[��ų ����] ��ų�� ��� Type�� ������ ���� ����");
                    return itemResultText = SKILL_TYPE_ERROR_TEXT; // ������ �ٷ� Ż��
                }
                
                Managers.StageManager.Player.skillList.Add(skillObject.name, newSkillObject.GetComponent<PlayerSkill>());
                itemResultText = SKILL_BUY_RESULIT_TEXT;
            }

            else // ��ȭ
            {
                if (updateSkill.GetComponent<ActiveSkill>() != null)
                {
                    updateSkill.GetComponent<ActiveSkill>().Upgrade();
                }
                else
                {
                    updateSkill.GetComponent<PassiveSkill>().Upgrade();
                }
                updateSkill.SkillLevel++;
                itemResultText = SKILL_UPGRADE_RESULIT_TEXT;
            }
            //selectImage = soldout;
        }
        return itemResultText;
    }
    
    private GameObject MakeSkillObject(string skillObjectName)
    {
        skillObject = Managers.Resource.GetPerfabGameObject(DEFAULT_SKILL_PATH + skillObjectName);
        return skillObject;
    }

}

[Serializable]
internal class PotionProcess : Item
{
    [SerializeField] private ItemStat positionStat;

    private readonly string POTION_APPLY_TEXT = "ü�� ȸ�� �Ϸ�";
    private readonly int POTION_VALUE = 10, BASE_POTION_PRICE = 100;
    
    public override string BuyItem(int itemIndex, ref int playerGold)
    {
        ItemList[itemIndex].ItemPrice = PotionPrice(Managers.StageManager.Player.Hp,
                                                    Managers.StageManager.Player.MaxHp,
                                                    BASE_POTION_PRICE);
        itemResultText = base.BuyItem(itemIndex, ref playerGold);

        if (itemResultText.Equals(string.Empty))
        {
            HealingPlayerHP(POTION_VALUE);
            itemResultText = POTION_APPLY_TEXT;
        }
        
        return itemResultText;
    }

    private void HealingPlayerHP(int healValue)
    {
        int playerHP = Managers.StageManager.Player.Hp + healValue;
        Managers.StageManager.Player.Hp = Math.Min(playerHP, Managers.StageManager.Player.MaxHp);
    }
    
    private int PotionPrice(int playerCurrentHealth, int playerMaxHealth, int basePrice) 
    {
        int healthPercent = playerCurrentHealth / playerMaxHealth;
        int normalizedCost = basePrice * (1 - healthPercent);
        return normalizedCost;
    }
}

internal class RandomProcess
{
    internal static void RandomDice(int maxDiceRange, out int selectRandomSkill, out bool isBuff)
    { 
        selectRandomSkill =  UnityEngine.Random.Range(0, maxDiceRange);
        isBuff = Convert.ToBoolean(UnityEngine.Random.Range(0, 2) == 0);  // 50% Ȯ���� ���׷��̵� �Ǵ� �ٿ�׷��̵�
    }

    internal static int RandomSkillList(int maxSkillList)
    {
       return UnityEngine.Random.Range(0, maxSkillList);
    }
}

public class Item
{
    internal static ItemStat[] ItemList = new ItemStat[5];
    protected string itemResultText;

    private readonly string PLAYER_GOLD_FAIL_STATE_TEXT = "���� �����ϱ�, �ڳ�!";

    public virtual string BuyItem(int itemIndex, ref int playerGold)
    {
        return CalculationItemPrice(ItemList[itemIndex], playerGold);
    }

    /// <summary>
    /// �÷��̾� ���� ������ ���ݰ��
    /// </summary>
    /// <param name="item">������</param>
    /// <param name="playerGold">�÷��̾� ���</param>
    /// <returns>���Ű�� Text</returns>
    private string CalculationItemPrice(ItemStat item, int playerGold)
    {
        if (item.ItemPrice <= playerGold)
        {
            playerGold -= item.ItemPrice;
            return string.Empty;
        }
        else
        {
            return  PLAYER_GOLD_FAIL_STATE_TEXT;
        }
    }

    /// <summary>
    /// ������ ���� ����
    /// </summary>
    /// <param name="item">������</param>
    /// <returns>�ش� �����ۿ� ������ ����</returns>
    internal static int MeasureItemPrice(ItemStat item)
    {
        int verifiedItemPrice = 0;
        
        switch (item.ItemType)
        { 
            // TODO : �� Ÿ�Ը��� �������� �ϴ� �޼��� �ۼ��ʿ� !
            case Define.ItemType.Skill : break;
            case Define.ItemType.Stat : break;
            case Define.ItemType.Potion : break;
        }
        return verifiedItemPrice;
    }
}

 