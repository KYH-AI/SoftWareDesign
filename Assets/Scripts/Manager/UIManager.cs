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
    public Image[] activeSkills = new Image[5];
    public Image[] passiveSkills = new Image[5];

    Dictionary<ActiveSkill, Sprite> activeSkillDic = new Dictionary<ActiveSkill, Sprite>();
    Dictionary<Sprite, PassiveSkill> passiveSkillDic = new Dictionary<Sprite, PassiveSkill>();
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
    public void UpdateActiveSkills(Sprite sprite, ActiveSkill activeSkill)      //�������� ��ų ���Ž� ����.
    {
        //activeSkillDic.Add(sprite, activeSkill);
        //activeSkills[Managers.StageManager.index] = sprite;
    }
    public void UpdatePassiveSkills(Sprite sprite, PassiveSkill passiveSkill)      //�������� ��ų ���Ž� ����.
    {
        //passiveSkillDic.Add(sprite, passiveSkill);
        //passiveSkills[Managers.StageManager.index] = sprite;
    }

    public void TurnSkillUIOn(PlayerSkill playerSkill)
    {
        //if(activeSkillDic[0].currentSkillState == Define.CurrentSkillState.ACTIVE)
        //�������.
        //else
        //��ο�����.
    }
    public void TurnSkillUIOff()
    {

    }

    public void InitBossSlider()
    {
        bossSlider.value = 1;
        Managers.StageManager.isBossAlive = true;
    }
}
