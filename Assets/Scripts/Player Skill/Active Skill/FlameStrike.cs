using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class FlameStrike : ActiveSkill
{
    [SerializeField] GameObject firePillarObject;
    [SerializeField] VolumeProfile firePillarProfile;

    #region 스킬 기본 스텟 데이터
    /// <summary>
    /// 스킬 데미지
    /// </summary>
    private int skillDamgae = 60;
    /// <summary>
    /// 불기둥 스킬 소환 개수
    /// </summary>
    private int skillProjectileCount = 1; // 초기 값 1개
    /// <summary>
    /// 불기둥 소환 간격시간
    /// </summary>
    private float skillAttackDelay = 0.25f; // 초기 값 0초
    /// <summary>
    /// 불기둥 소환간격 시간 코루틴  
    /// </summary>
    private WaitForSeconds skillAttackDelayTimeSec;
    #endregion

    #region 스킬 스텟 프로퍼티
    /// <summary>
    /// 스킬 데미지 프로퍼티 ( set : 스킬 데미지 값 변경 )
    /// </summary>
    public int SkillDamgae { set { skillDamgae = value; } }
    /// <summary>
    /// 불기둥 스킬 소환 개수 프로퍼티 ( set : 불기둥 스킬 소환 개수 값 변경 )
    /// </summary>
    public float SkillProjectile { set { SkillProjectile = value; } }
    #endregion

    private void Start()
    {
        FlameStrikeInit();
    }

    private void FlameStrikeInit()
    {
        skillAttackDelayTimeSec = new WaitForSeconds(skillAttackDelay);
    }

    public override void OnActive()
    {

        Debug.Log("현재 스킬상태는 : " + currentSkillState);
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            FlameStrikeSkillAttack();
        }
        else
        {
            // TODO : UI에서 "아직 재사용 대기시간 입니다." 연출하기
            return;
        }
    }

    public override void Upgrade()
    {
        skillDamgae += 20;
        SkillCoolTime -= 2;
        skillProjectileCount += 2;
        // TODO : 상점에서 업그레이드 방식이 정해지면 진행 하자 (09/28)
    }

    private void FlameStrikeSkillAttack()
    {
        // TODO (09/29) : 불기둥 소환방식 랜덤으로 설정
        StartCoroutine(FlameStrikeSkillProcess());
    }
    private Vector2 RandomSpawnLocation (float xPos = 0f, float yPos = 0f)
    {
        return new Vector2(transform.position.x + xPos, transform.position.y + yPos);
    }

    IEnumerator FlameStrikeSkillProcess()
    {
        OnSkillEffect();

        for (int i = 0; i < skillProjectileCount; i++)
        { 
            GameObject projectile = MemoryPoolManager.GetInstance().OutputGameObject(firePillarObject, 
                                                                                     "Player_Skill/"+firePillarObject.name,
                                                                                     new Vector2(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f)),
                                                                                     Quaternion.identity); 
            projectile.GetComponent<Projectile>().ProjectileInit(Define.StringTag.Enemy, Vector2.zero, skillDamgae);
            projectile.SetActive(true);
            yield return skillAttackDelayTimeSec; // 불기둥 소환간격 시간
        }

       Invoke(nameof(OffSkillEffect), 2f); //  2초뒤 이펙트 비활성화 ;
        OnCoolTime();
    }

    private void OnSkillEffect()
    {
        Managers.SkillEffectVolume.ChagnePostProcessProfile(firePillarProfile);
    }

    private void OffSkillEffect()
    {
        Managers.SkillEffectVolume.ChagnePostProcessProfile(null);
    }
}
