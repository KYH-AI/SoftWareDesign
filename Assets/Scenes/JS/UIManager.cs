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
    public void ClosePopUpUI(Canvas canvas)     //� ĵ������ ������?
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
        //�÷��̾� ��ų ��ųʸ��� key�� �����ؼ� value�� ������ �̹��� ���.
    }

    public void ShowSkillCoolTime()
    {
        //��ų ���°� ��Ÿ���̸� ���� �� ǥ��
    }
}
