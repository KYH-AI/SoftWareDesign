using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public Slider playerSlider;
    public Slider bossSlider;
    public Text killCount;
    public Text goldAmount;
    public Image[] skills = new Image[5];

    private void Update()
    {
        UpdateKillCounts();
        UpdateGoldText();
    }
    public void UpdatePlayerHpSlider(float currentHp, float maxHp)      //�÷��̾ ������ �޾��� �� ����.
    {
        playerSlider.value = currentHp / maxHp;
    }

    public void UpdateBossHpSlider(float currentHp, float maxHp)        //���� ���Ͱ� ������ �޾��� �� ����.
    {
        bossSlider.value = currentHp / maxHp;
    }
    public void UpdateGoldText()    //��� ȹ�� �� ����.
    {
        goldAmount.text = Managers.StageManager.Player.PlayerGold.ToString();
    }
    public void UpdateKillCounts()  //���� óġ �� ����.
    {
        killCount.text = ("" + Managers.StageManager.killCount);
    }
    public void UpdateSkills()      //�������� ��ų ���Ž� ����.
    {
        //�÷��̾� ��ų ��ųʸ��� key�� �����ؼ� value�� ������ �̹��� ���.
        if(Managers.StageManager.Player.playerActiveSkills != null)
        {
            //�̹��� �������� �ҷ���.
        }
    }

    public void ShowSkillCoolTime()
    {
        //��ų ���°� ��Ÿ���̸� ���� �� ǥ��
    }

    public void InitBossSlider()
    {
        bossSlider.value = 1;
    }
}
