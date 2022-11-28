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
    public void UpdateSkills()      //상점에서 스킬 구매시 실행.
    {
        //플레이어 스킬 딕셔너리에 key로 접근해서 value가 있으면 이미지 띄움.
        if(Managers.StageManager.Player.playerActiveSkills != null)
        {
            //이미지 프리팹을 불러옴.
        }
    }

    public void ShowSkillCoolTime()
    {
        //스킬 상태가 쿨타임이면 남은 쿨 표시
    }

    public void InitBossSlider()
    {
        bossSlider.value = 1;
    }
}
