using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class FirePillar : ActiveSkill
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
    private int skillProjectileCount = 15; // 초기 값 1개
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
    /// <summary>
    /// 불기둥 스킬 소환간격 시간 프로퍼티 ( set : 불기둥 소환간격 시간 코루틴 WaitForSeconds 값 변경 )
    /// </summary>
    public float SkillAttackDelay
    {
        set
        {
            if(skillAttackDelay != value)
            {
                skillAttackDelayTimeSec = new WaitForSeconds(value);
            }
            skillAttackDelay = value;
        }
    }
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

        float[,] locationCount = new float[4, 2]
        {
            {0f, 2f},
            {4f, 0f},
            {0f, -4f},
            {-4f, 0f},
         };

        OnSkillEffect();

        for (int i = 0; i < skillProjectileCount; i++)
        {
            //  GameObject projectile =  Instantiate(firePillarObject, RandomSpawnLocation(locationCount[i,0], locationCount[i,1]), Quaternion.identity);
            GameObject projectile = Instantiate(firePillarObject, RandomSpawnLocation(Random.Range(-4f, 4f), Random.Range(-4f, 4f)), Quaternion.identity);
            projectile.GetComponent<Projectile>().ProjectileInit(Vector2.zero, skillDamgae);
            yield return skillAttackDelayTimeSec; // 불기둥 소환간격 시간
        }

        Invoke(nameof(OffSkillEffect), 2f); //  2초뒤 이펙트 비활성화 ;
        OnCoolTime();
    }

    private void OnSkillEffect()
    {
        PlayerCamera.Instance.ChagnePostProcessProfile(firePillarProfile);
    }

    private void OffSkillEffect()
    {
        PlayerCamera.Instance.ChagnePostProcessProfile(null);
    }


    /*
    private void SpawnFirePillarObject(float angle)
    {
        Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector2.right;
        Vector2 spawnPosition = playerObject.transform.position + direction * 5;
        Debug.Log(direction);
        Debug.Log(spawnPosition);
        Instantiate(firePillarObject, spawnPosition, Quaternion.identity);
    }

    */
}
