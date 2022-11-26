using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : Managers
{
    [SerializeField]
    private Slider playerSlider;
    private Slider bossSlider;
    private Text goldAmount;
    private Image[] skills = new Image[5];
    public void PopupUI(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
    }
    public void ClosePopUpUI(Canvas canvas)     //어떤 캔버스를 닫을래?
    {
        canvas.gameObject.SetActive(false);
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

    public void UpdateSkills()
    {
        //플레이어 스킬 딕셔너리에 key로 접근해서 value가 있으면 이미지 띄움.
    }

    public void ShowSkillCoolTime()
    {
        //스킬 상태가 쿨타임이면 남은 쿨 표시
    }
}
