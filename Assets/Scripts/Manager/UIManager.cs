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
        goldAmount.text = StageManager.stageManager.Player.PlayerGold.ToString();
    }
    public void UpdateKillCounts()  //���� óġ �� ����.
    {
        killCount.text = ("KillCount Remain\n" + StageManager.stageManager.killCount);
    }
    public void UpdateSkills()      //�������� ��ų ���Ž� ����.
    {
        //�÷��̾� ��ų ��ųʸ��� key�� �����ؼ� value�� ������ �̹��� ���.
        StageManager.stageManager.Player.playerActiveSkills[0]
    }

    public void ShowSkillCoolTime()
    {
        //��ų ���°� ��Ÿ���̸� ���� �� ǥ��
    }
}
