using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HourGlass : PassiveSkill
{
    [SerializeField] GameObject hourGlassEffectObject;
    [SerializeField] VolumeProfile hourGlassProfile;

    private Animator hourGlassAnimator;

    #region 스킬 기본 스텟 데이터
    /// <summary>
    /// 스킬 지속시간 
    /// </summary>
    private float skillDuration = 5f; // 초기 값 5초
    /// <summary>
    /// 스킬 지속시간 코루틴
    /// </summary>
    private WaitForSecondsRealtime skillDurationSec;
    #endregion

    #region 스킬 스텟 프로퍼티
    /// <summary>
    /// 스킬 지속시간 프로퍼티 ( set : 모래시계 지속시간 코루틴 WaitForSeconds 값 변경 )
    /// </summary>
    public float SkillDuration 
    { 
        set 
        {
            if (skillDuration != value)
            {
                skillDurationSec = new WaitForSecondsRealtime(value);
            }
            skillDuration = value;
        }
    } 
    #endregion


    private void Start()
    {
        HourGlassInit();
    }

    private void HourGlassInit()
    {
        hourGlassAnimator = hourGlassEffectObject.GetComponent<Animator>();
        hourGlassEffectObject.SetActive(false); 
        skillDurationSec = new WaitForSecondsRealtime(skillDuration);
    }

    public override void OnActive()
    {
        // 플레이어 현재 체력이 30보다 많을경우 무시
        if (playerObject.Hp < 30) return;

        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            StartCoroutine(HourGlassSkillProcess());
        }
        else
        {
            return;
        }
    }

    public override void Upgrade()
    {
       
    }

    private void HourGlassSkillActive()
    {
        Time.timeScale = 0f; // 시간 정지 
        OnSkillEffect();
        hourGlassEffectObject.SetActive(true); // Hour Glass Effect 오브젝트 활성화
    }

    private void HourGlassSkillDisable()
    {
        hourGlassAnimator.SetTrigger("ComeBack"); // 돌아오는 시계 애니메이션 재생 및 HourGlassEvent.cs 에서 연출 효과 및 시간 정지 해체 진행
        OnCoolTime();
    }

    private void OnSkillEffect()
    {
       Managers.SkillEffectVolume.ChagnePostProcessProfile(hourGlassProfile); // Hour Glass 스킬 포스트 프로세싱 효과 활성화
    }

    private IEnumerator HourGlassSkillProcess()
    {
        HourGlassSkillActive();

        yield return skillDurationSec;

        HourGlassSkillDisable();
    }


}
