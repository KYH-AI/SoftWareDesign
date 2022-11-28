using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 수리검 N개 투척
/// </summary>
public class ThrowingKnife : ActiveSkill
{
    [SerializeField] GameObject knifeObject;

    #region 스킬 기본 스텟 데이터
    /// <summary>
    /// 스킬 데미지
    /// </summary>
    private int skillDamgae = 2;
    /// <summary>
    /// 표창 소환 개수
    /// </summary>
    private int skillProjectileCount = 2; 
    /// <summary>
    /// 표창 투척속도
    /// </summary>
    private float skillProjectileSpeed = 15f; // 초기 값은 15
    /// <summary>
    /// 표창 소환간격 (상수 값)
    /// </summary>
    private readonly WaitForSeconds SkillAttackDelay = new WaitForSeconds(0.25f);
    #endregion

    #region 스킬 스텟 프로퍼티
    /// <summary>
    /// 스킬 데미지 프로퍼티 ( set : 스킬 데미지 값 변경 )
    /// </summary>
    public int SkillDamgae { set { skillDamgae = value; } }
    /// <summary>
    /// 표창 투척속도 프로퍼티 ( set : 표창 투척속도 값 변경 )
    /// </summary>
    public float SkillProjectileSpeed { set { skillProjectileSpeed = value; } }
    /// <summary>
    /// 표창 소환 개수 프로퍼티 ( set : 표창 소환 개수 개수 값 변경 )
    /// </summary>
    public float SkillProjectile { set { SkillProjectile = value; } }
    #endregion

    public override void OnActive()
    {
        if(currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            ThrowingKnifeSkillAttack();
        }
        else
        {
            // TODO : UI에서 "아직 재사용 대기시간 입니다." 연출하기
            return;
        }
    }
    public override void Upgrade()
    {
        skillDamgae += 2;
        SkillCoolTime -= 0.5f;
        skillProjectileCount += 2;
        skillProjectileSpeed += 2f;
        // TODO : 상점에서 업그레이드 방식이 정해지면 진행 하자 (09/28)
    }

    private void ThrowingKnifeSkillAttack()
    {
        StartCoroutine(ThrowingKnifeSkillProcess());
    }

     private IEnumerator ThrowingKnifeSkillProcess()
    {
        for(int projectiletCount = 0; projectiletCount < skillProjectileCount; projectiletCount++)
        {
            CreateProjectile();
            yield return SkillAttackDelay;
        }
        OnCoolTime();
    }


    private void CreateProjectile()
    {
        GameObject projectile = MemoryPoolManager.GetInstance().OutputGameObject(knifeObject,
                                                                                 "Player_Skill/"+knifeObject.name,
                                                                                 (transform.position + new Vector3(playerObject.PlayerController.LastDirection.x, playerObject.PlayerController.LastDirection.y, 0f)) + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)),
                                                                                 Quaternion.Euler(0, 0, Mathf.Atan2(-playerObject.PlayerController.LastDirection.y, -playerObject.PlayerController.LastDirection.x) * Mathf.Rad2Deg));

        projectile.GetComponent<Projectile>().ProjectileInit(Define.StringTag.Enemy, 
                                                             playerObject.PlayerController.LastDirection, 
                                                             skillDamgae, 
                                                             skillProjectileSpeed);
        projectile.SetActive(true);
    }
}
