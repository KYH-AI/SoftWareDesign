using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBlade : PassiveSkill
{
    #region 스킬 초기 스텟 데이터
    /// <summary>
    /// 스킬 기본 공격력 버프 퍼센트
    /// </summary>
    private int buffDamagePercent = 50;
    /// <summary>
    /// 스킬 버프 받은 후 마지막 데미지
    /// </summary>
    private int lastBuffDamage;
    /// <summary>
    /// 플레이어 마지막 기본 공격력
    /// </summary>
    private int lastDefaultAttackDamage;
    /// <summary>
    /// 스킬 지속시간
    /// </summary>
    private float skillDuration = 5f;
    /// <summary>
    /// 스킬 딜레이 (고정 값) = 초당 회복력을 위한 1초 시간 코루틴
    /// </summary>
    private WaitForSeconds skillDurationSec;
    #endregion

    #region 스킬 스텟 프로퍼티
    /// <summary>
    /// 스킬 이동속도 프로퍼티 (  set : 겁쟁이 이동속도 buffSpeed 값 변경 )
    /// </summary>
    public int BuffDamagePercent
    {
        set 
        {
            buffDamagePercent = value; 
            lastBuffDamage = (playerObject.DefaultAttackDamage * buffDamagePercent) / 100;
        }
    }
    /// <summary>
    /// 스킬 지속시간 프로퍼티 (  set : 겁쟁이 지속시간 코루틴 WaitForSeconds 값 변경 )
    /// </summary>
    public float SkillDuration
    {
        set
        {
            if (skillDuration != value)
            {
                skillDurationSec = new WaitForSeconds(value);
            }
            skillDuration = value;
        }
    }
    #endregion

    private void Start()
    {
        SpellBladeInit();
    }

    private void SpellBladeInit()
    {
        skillDurationSec = new WaitForSeconds(skillDuration);
        lastDefaultAttackDamage = playerObject.DefaultAttackDamage;
        lastBuffDamage = (playerObject.DefaultAttackDamage * buffDamagePercent) / 100;
    }

    public override void OnActive()
    {
        // TODO : 플레이어에서 Active 스킬을 쓰는 구간에 AddListener 이벤트 등록
        playerObject.BuffEvent.AddListener(SpellBladeActive);
    }

    public override void Upgrade()
    {
        
    }

    private void SpellBladeActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            StartCoroutine(SpellBladeSkillProcess());
        }
        else
        {
            return;
        }
    }

    private IEnumerator SpellBladeSkillProcess()
    {
        if(playerObject.DefaultAttackDamage != lastDefaultAttackDamage)
        {
            lastBuffDamage = (playerObject.DefaultAttackDamage * buffDamagePercent) / 100;
        }

        playerObject.DefaultAttackDamage = lastBuffDamage;
        yield return skillDurationSec;
        StopSkillProcess();
    }

    /// <summary>
    /// SpellBlade 스킬 코루틴 종료
    /// </summary>
    public void StopSkillProcess()
    {
        OnCoolTime();
        StopCoroutine(SpellBladeSkillProcess());
    }
}


