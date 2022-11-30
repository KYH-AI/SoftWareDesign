using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : ActiveSkill
{
   [SerializeField] GameObject barrierEffectObject;
    private readonly string barrierOnSFX = "Player/Active Skill/Barrier On";

    private CircleCollider2D barrierCollider; 
    private Animator barrierAnimator;
    public Animator BarrierAnimator { get { return barrierAnimator; } }

    private Coroutine barrierCheck = null;


    #region 스킬 기본 스텟 데이터 
    /// <summary>
    /// 스킬 지속시간
    /// </summary>
    private float skillDuration = 5f;
    /// <summary>
    /// 스킬 지속시간 코루틴
    /// </summary>
    private WaitForSeconds skillDurationTimeSec;
    /// <summary>
    /// 스킬 데미지
    /// </summary>
    private int skillDamage = 2;
    /// <summary>
    /// 방벽 스킬 사이즈
    /// </summary>
    private Transform barrierSize;
    /// <summary>
    /// 방벽 공격간격 시간
    /// </summary>
    private float barrierAttackDelay = 1f;
    /// <summary>
    /// 방벽 공격간격 시간 코루틴
    /// </summary>
    private WaitForSeconds barrierAttackDelayTimeSec;
    #endregion

    #region 스킬 스텟 프로퍼티
    /// <summary>
    /// 스킬 지속시간 프로퍼티 ( set : 지속시간 코루틴 WaitForSeconds 값 변경 )
    /// </summary>
    public float SkillDuration
    {
        set
        {
            if(skillDuration != value)
            {
                skillDurationTimeSec = new WaitForSeconds(value);
            }
            skillDuration = value;
        }
    }
    /// <summary>
    /// 스킬 데미지 프로퍼티 ( get : 스킬 데미지 값, set : 스킬 데미지 값 변경 )
    /// </summary>
    public int SkillDamage 
    { 
        get { return skillDamage; }
        set { skillDamage = value; }
    } 
    /// <summary>
    /// 방벽 스킬 크기 프로퍼티 ( set : 방벽 스킬 Scale 값 변경 )
    /// </summary>
    public Vector3 BarrierSize { set { barrierSize.localScale = value; } }
    /// <summary>
    /// 방벽 스킬 공격간격 시간 프로퍼티 ( set : 공견간격 시간 코루틴 WaitForSeconds 값 변경 )
    /// </summary>
    public float BarrierAttackDelay
    {
        set
        {
            if (barrierAttackDelay != value)
            {
                skillDurationTimeSec = new WaitForSeconds(value);
            }
            barrierAttackDelay = value;
        }
    }
    #endregion

    private void Start()
    {
        BarrierInit();
    }

    private void BarrierInit()
    {
        skillDurationTimeSec = new WaitForSeconds(skillDuration); // 스킬 초기 지속시간 값 설정
        barrierAttackDelayTimeSec = new WaitForSeconds(barrierAttackDelay); // 방벽 초기 공격간격 시간 값 설정

        barrierAnimator = barrierEffectObject.GetComponent<Animator>();
        barrierCollider = barrierEffectObject.GetComponent<CircleCollider2D>();
        barrierSize = barrierEffectObject.GetComponent<Transform>();
    }
    
    public override void OnActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            barrierCheck = StartCoroutine(BarrierSkillProcess());
        }
        else
        {
            // TODO : UI에서 "아직 재사용 대기시간 입니다." 연출하기
            return;
        }
    }

    public override void Upgrade()
    {
        skillDamage += 1;
        SkillCoolTime -= 2f;
        barrierSize.localScale += new Vector3(0.5f, 0.5f, 1f);
        barrierAttackDelay -= 0.2f;
        skillDuration += 1f;
        //  barrierSize.localScale += new Vector3(0.5f, 0.5f, 1f);
        // TODO : 상점에서 업그레이드 방식이 정해지면 진행 하자 (09/28)
    }

    private void BarrierSkillActive()
    {
        barrierEffectObject.SetActive(true);
        barrierCollider.enabled = true;
        Managers.Sound.PlaySFXAudio(barrierOnSFX, null, 0.5f);
        StartCoroutine(BarrierHitBox());
        // TODO : barrier 오브젝트 콜라이더 활성화
    }

    private void BarrierSkillDisable()
    {
       if(barrierCheck != null)
       {
            StopCoroutine(barrierCheck);
       }

        StopCoroutine(BarrierHitBox());
        barrierCheck = null;
        barrierCollider.enabled = false;
        barrierEffectObject.SetActive(false);
        OnCoolTime();

        // TODO : barrier 오브젝트 콜라이더 비활성화
    }

    private IEnumerator BarrierSkillProcess()
    {
        BarrierSkillActive();

        yield return skillDurationTimeSec;

        BarrierSkillDisable();
    }

    // Barrier 콜라이더 On/Off
    private IEnumerator BarrierHitBox()
    {
        while (true)
        {
            barrierCollider.enabled = true;
            yield return barrierAttackDelayTimeSec;
            barrierCollider.enabled = false;
        }
    }

}
