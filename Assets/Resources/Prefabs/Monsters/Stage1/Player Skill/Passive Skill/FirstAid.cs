using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : PassiveSkill
{
    #region 스킬 초기 스텟 데이터
    /// <summary>
    /// 스킬 최대 체력의 회복 퍼센트
    /// </summary>
    private int buffHpRegenPercent = 8;
    /// <summary>
    /// 스킬 지속시간
    /// </summary>
    private float skillDuration = 3f;
    /// <summary>
    /// 스킬 딜레이 (고정 값) = 초당 회복력을 위한 1초 시간 코루틴
    /// </summary>
    private readonly WaitForSeconds PER_SECONDS = new WaitForSeconds(1f);
    #endregion

    #region 스킬 스텟 프로퍼티
    /// <summary>
    /// 스킬 이동속도 프로퍼티 (  set : 겁쟁이 이동속도 buffSpeed 값 변경 )
    /// </summary>
    public int BuffHpRegenPercent
    {
        set { buffHpRegenPercent = value; }
    }
    /// <summary>
    /// 스킬 지속시간 프로퍼티 (  set : 겁쟁이 지속시간 코루틴 WaitForSeconds 값 변경 )
    /// </summary>
    public float SkillDuration
    {
        set
        {
            skillDuration = value;
        }
    }
    #endregion

    private void Start()
    {
        FirstAidInit();
    }

    private void FirstAidInit()
    {
        //skillDurationSec = new WaitForSeconds(skillDuration);
    }

    /// <summary>
    ///  해당 스킬 피격 시 발동 되도록 이벤트 등록
    /// </summary>
    public override void OnActive()
    {
        playerObject.HitEvent += FirstAidSkillActive;
    }

    public override void Upgrade()
    {
        /*
         *  지속시간 증가
         *  속도 버프 증가
         *  쿨타임 감소
         */
    }

    private void FirstAidSkillActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            StartCoroutine(FirstAidSkillProcess(skillDuration, (playerObject.MaxHp * buffHpRegenPercent) / 100));
        }
        else
        {
            return;
        }

        /*  
        // playerController.MoveSpeed *= buffSpeed;

        // 버프 효과 시작
        // 전체값 X 퍼센트 ÷ 100 //
        playerController.MoveSpeed = (buffSpeed * buffStat) / 100;
        // 프로퍼티 접근이 아닌 다이렉트로 접근 (일시적으로 바꾸는 값)
        copyMoveSpeed = buffSpeed; // 현재 속도를 buffSpeed 로 변경

        // buffDuration 시작
        // 코루틴 


        // 버브 효과 종료
        buffSpeed = moveSpeed;      // 버프 값을 buffStat 만큼 다시 역계산해서 buffSpeed 초기 값으로 돌림
        copyMoveSpeed = buffSpeed;  // 현재 속도를 버프를 받기 전 buffSpeed 값으로 돌림 
        */
    }

    private IEnumerator FirstAidSkillProcess(float buffDuration, int addHP)
    {
        while(buffDuration > 0)
        {
            print("플레이어 체력 회복 중 (남은시간 : " + buffDuration);
            playerObject.Hp += addHP;
            buffDuration--;
            yield return PER_SECONDS;
        }

        OnCoolTime();
    }
}
