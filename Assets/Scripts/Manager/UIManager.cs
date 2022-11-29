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

    public void UpdatePlayerHpSlider(float currentHp, float maxHp)      //플레이어가 데미지 받았을 때 실행.
    {
        playerSlider.value = currentHp / maxHp;
    }

    public void UpdateBossHpSlider(float currentHp, float maxHp)        //보스 몬스터가 데미지 받았을 때 실행.
    {
        bossSlider.value = currentHp / maxHp;
    }
    public void UpdateGoldText()    //골드 획득 시 실행.
    {
        goldAmount.text = Managers.StageManager.Player.PlayerGold.ToString();
    }
    public void UpdateKillCounts()  //몬스터 처치 시 실행.
    {
        killCount.text = ("" + Managers.StageManager.killCount);
    }
    public void UpdateActiveSkills(Sprite sprite, ActiveSkill activeSkill)      //상점에서 스킬 구매시 실행.
    {
        //activeSkillDic.Add(sprite, activeSkill);
        //activeSkills[Managers.StageManager.index] = sprite;
    }
    public void UpdatePassiveSkills(Sprite sprite, PassiveSkill passiveSkill)      //상점에서 스킬 구매시 실행.
    {
        //passiveSkillDic.Add(sprite, passiveSkill);
        //passiveSkills[Managers.StageManager.index] = sprite;
    }

    public void TurnSkillUIOn(PlayerSkill playerSkill)
    {
        //if(activeSkillDic[0].currentSkillState == Define.CurrentSkillState.ACTIVE)
        //밝아진다.
        //else
        //어두워진다.
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
