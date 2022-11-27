using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coward : PassiveSkill
{
    [SerializeField] GameObject cowardEffect;

    #region 스킬 초기 스텟 데이터
    /// <summary>
    /// 스킬 이동속도
    /// </summary>
    private int buffSpeed = 3;  //초기 데이터 1
    /// <summary>
    /// 스킬 지속시간
    /// </summary>
    private float skillDuration = 1.5f; // 초기 데이터 1.5f 
    /// <summary>
    /// 스킬 지속시간 코루틴
    /// </summary>
    private WaitForSeconds skillDurationSec;
    #endregion

    #region 스킬 스텟 프로퍼티
    /// <summary>
    /// 스킬 이동속도 프로퍼티 (  set : 겁쟁이 이동속도 buffSpeed 값 변경 )
    /// </summary>
    public int BuffSpeed
    {
        set { buffSpeed = value; }
    }
    /// <summary>
    /// 스킬 지속시간 프로퍼티 (  set : 겁쟁이 지속시간 코루틴 WaitForSeconds 값 변경 )
    /// </summary>
    public float SkillDuration 
    { 
        set 
        { 
            if(skillDuration != value)
            {
                skillDurationSec = new WaitForSeconds(value);
            }
            skillDuration = value; 
        }     
    }
    #endregion

    private void Start()
    {
        CowardInit();
    }

    private void CowardInit()
    {
        skillDurationSec = new WaitForSeconds(skillDuration);
    }

    /// <summary>
    ///  해당 스킬 피격 시 발동 되도록 이벤트 등록
    /// </summary>
    public override void OnActive()
    {
        playerObject.HitEvent += CowardSkillActive;
    }

    public override void Upgrade()
    {
        /*
         *  지속시간 증가
         *  속도 버프 증가
         *  쿨타임 감소
         */
    }

    private void CowardSkillActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            Debug.Log("겁쟁이 패시브 작동");
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            StartCoroutine(CowardSkillProcess());
        }
        else
        {
            return;
        }
        #region  버프 효과가 %일 경우 아래 코드를 이용
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
        #endregion
    }

    private IEnumerator CowardSkillProcess()
    {
        cowardEffect.SetActive(true);
        playerObject.MoveSpeed += buffSpeed; // 속도 버프 적용
        yield return skillDurationSec;
        playerObject.MoveSpeed -= buffSpeed; // 속도 버프 해체
        cowardEffect.SetActive(false);
        OnCoolTime();
    }
}
