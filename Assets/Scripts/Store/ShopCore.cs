using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ShopCore : MonoBehaviour
{
    [Header("랜덤 요소 클래스")]
    private RandomProcess randomProcess = new RandomProcess();
    
    [Header("랜덤 주사위 클래스")]
    internal DiceProcess diceProcess = new DiceProcess();
    
    [Header("포션 아이템 클래스")]
    [SerializeField] internal PotionProcess potionProcess = new PotionProcess();
    
    [Header("스킬 아이템 클래스")]
    [SerializeField] internal SkillProcess skillProcess = new SkillProcess();

    [Header("상점 UI")] [SerializeField] private ShopUI shopUI;
    
    private void Start()
    {
        shopUI = new ShopUI(this, this);
        InitPlayerStatUI();
        InitSkillList();
        InitRandomDice();
    }
    
    /// <summary>
    /// Player 현재 스텟 UI 초기화
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
    #region 플레이어 스텟 UI
    [SerializeField] private Text playerMoveSpeedText;
    [SerializeField] private Text playerMaxHpText;
    [SerializeField] private Text playerDefaultAttackDmgText;
    [SerializeField] private Text playerArmorText;
    #endregion

    #region 랜덤 버프 주사위 UI
    [SerializeField] private Image buffImage;
    [SerializeField] private Text diceResultText;
    [SerializeField] private Button randomDiceButton;
    #endregion

    #region 랜덤 스킬 UI
    [SerializeField] private Image[] skillImages = new Image[3];
    [SerializeField] private Text[] skillNames = new Text[3];
    #endregion

    #region 구매 확인 UI
    [SerializeField] private GameObject itemConfirmationPanel;
    [SerializeField] private Text itemIntroductionText;
    [SerializeField] private Button itemBuyButton;
    [SerializeField] private Text buyItemResultText;
    #endregion
    
    #region 아이템 디테일 설명 UI
    [SerializeField] private GameObject itemDetailPanel;
    [SerializeField] private Text itemDetailText; 
    #endregion

    #region  아이템 선택 버튼
    [SerializeField] private Button[] itemSelectButtons = new Button[5];
    #endregion

    #region 플레이어 재화 UI
    [SerializeField] private Text playerGoldText;
    #endregion

    #region 구매 버튼 이벤트
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
    /// Buy 버튼 클릭 함수
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
    /// Buy 버튼을 제외한 모든 버튼 클릭 함수
    /// </summary>
    private void ItemSelected(int itemIndex)
    {
        // 구매 버튼 이벤트 모두해체
        itemBuyButton.onClick.RemoveAllListeners();
        
        // 아이템 상세설명 창 활성화
        itemConfirmationPanel.SetActive(true);
        itemIntroductionText.text = Item.ItemList[itemIndex].ItemIntroduction;
        /*
          1. 아이템 설명 창 활성화
          1-1. <"아이템 이름"> "아이템 설명"
        */

        // 구매 확인 창 활성화
        itemDetailPanel.SetActive(true);
        itemDetailText.text = ItemConfirmationText(Item.ItemList[itemIndex].ItemName, 
                                                   Item.ItemList[itemIndex].ItemPrice = Item.MeasureItemPrice(Item.ItemList[itemIndex]) );
        /*
          2. 구매 확인 창 활성화
          2-1. <"아이템 이름>" "가격 : ? 구매하시겠습니까?
        */

        
        /* Item Type으로 3가지로 나누어 switch로 확인해 BuySkill, BuyStat, BuyPotion 각 클래스로 이벤트 등록 */
        switch (Item.ItemList[itemIndex].ItemType)
        {
            case Define.ItemType.Skill : 
                    itemBuyButton.onClick.AddListener( () => BuyItemButtonEvent(shopCore.skillProcess, itemIndex) ); break;
            case Define.ItemType.Stat : 
                    itemBuyButton.onClick.AddListener( () => BuyItemButtonEvent(shopCore.diceProcess, itemIndex) ); break;
            case Define.ItemType.Potion : 
                    itemBuyButton.onClick.AddListener( () => BuyItemButtonEvent(shopCore.potionProcess, itemIndex) ); break;
            default: Debug.Log("아이템 타입 오류"); break;
        }
        
    }

    private string ItemConfirmationText(string itemName, int itemPrice)
    {
        return "<" + itemName + "> 가격:" + itemPrice.ToString() + " 구매하시겠습니까?";
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
       체력 (int)
       방어력 (int)
       공격력 (int)
       이동속도 (float)
     */
}

[Serializable]
internal class SkillProcess : Item
{
    [SerializeField] private ItemStat[] allSkillList;
    public ItemStat[] SkillList { get; private set; } = new ItemStat[3];

    private GameObject skillObject;
    private readonly string DEFAULT_SKILL_PATH = "Player_Skill/";
    private readonly string SKILL_BUY_RESULIT_TEXT = "스킬 구매 완료";
    private readonly string SKILL_UPGRADE_RESULIT_TEXT = "스킬 강화 완료";
    private readonly string SKILL_TYPE_ERROR_TEXT = "스킬 타입 오류";
    
    internal void SetRandomSkillList()
    {
        for (var i = 0; i < SkillList.Length; i++)
        {
            SkillList[i] = allSkillList[RandomProcess.RandomSkillList(allSkillList.Length)];
            ItemList[i] = SkillList[i]; // 0 ~ 2 까지 스킬 아이템 전달
        }
    }

    public override string BuyItem(int itemIndex, ref int playerGold)
    {
        itemResultText = base.BuyItem(itemIndex, ref playerGold);
        
        if (itemResultText.Equals(string.Empty))
        {
            // 구매
            if (!Managers.StageManager.Player.skillList.TryGetValue(SkillList[itemIndex].ItemName, out PlayerSkill updateSkill))
            {
                skillObject = MakeSkillObject(SkillList[itemIndex].ItemName);
                GameObject newSkillObject = Object.Instantiate(skillObject, Managers.StageManager.Player.transform);
                

                if (newSkillObject.TryGetComponent(out ActiveSkill activeSkill))
                {
                    activeSkill.Init(Managers.StageManager.Player);
                    Managers.StageManager.Player.playerActiveSkills.Add(Managers.StageManager.Player.ActiveSkillSlot_Index++, activeSkill);
                    Managers.UI.UpdateActiveSkills(SkillList[itemIndex].ItemImage, activeSkill);
                   // print("스킬구매" + selectImage.ToString() + activeSkill.ToString());
                }
                else if(newSkillObject.TryGetComponent(out PassiveSkill passiveSkill))
                {
                    passiveSkill.Init(Managers.StageManager.Player);
                    passiveSkill.OnActive();
                    Managers.UI.UpdatePassiveSkills(SkillList[itemIndex].ItemImage, passiveSkill);
                }
                else
                {
                    Debug.Log("[스킬 구매] 스킬이 어떠한 Type을 가지고 있지 않음");
                    return itemResultText = SKILL_TYPE_ERROR_TEXT; // 오류로 바로 탈출
                }
                
                Managers.StageManager.Player.skillList.Add(skillObject.name, newSkillObject.GetComponent<PlayerSkill>());
                itemResultText = SKILL_BUY_RESULIT_TEXT;
            }

            else // 강화
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

    private readonly string POTION_APPLY_TEXT = "체력 회복 완료";
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
        isBuff = Convert.ToBoolean(UnityEngine.Random.Range(0, 2) == 0);  // 50% 확률로 업그레이드 또는 다운그레이드
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

    private readonly string PLAYER_GOLD_FAIL_STATE_TEXT = "돈이 부족하군, 자네!";

    public virtual string BuyItem(int itemIndex, ref int playerGold)
    {
        return CalculationItemPrice(ItemList[itemIndex], playerGold);
    }

    /// <summary>
    /// 플레이어 골드와 아이템 가격계산
    /// </summary>
    /// <param name="item">아이템</param>
    /// <param name="playerGold">플레이어 골드</param>
    /// <returns>구매결과 Text</returns>
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
    /// 아이템 가격 측정
    /// </summary>
    /// <param name="item">아이템</param>
    /// <returns>해당 아이템에 측정된 가격</returns>
    internal static int MeasureItemPrice(ItemStat item)
    {
        int verifiedItemPrice = 0;
        
        switch (item.ItemType)
        { 
            // TODO : 각 타입마다 가격측정 하는 메서드 작성필요 !
            case Define.ItemType.Skill : break;
            case Define.ItemType.Stat : break;
            case Define.ItemType.Potion : break;
        }
        return verifiedItemPrice;
    }
}

 