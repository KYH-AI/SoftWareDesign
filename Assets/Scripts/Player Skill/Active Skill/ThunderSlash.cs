using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 귀멸의 칼날 벽력일섬 N연속 기술
/// </summary>
public class ThunderSlash : ActiveSkill
{
    private TrailRenderer thunderclapEffect;
    [SerializeField] GameObject thunderSlashLockOnEffect;
    [SerializeField] VolumeProfile thunderClapProfile;

    #region 스킬 기본 스텟 데이터
    /// <summary>
    /// 스킬 데미지
    /// </summary>
    private int skillDamage = 25;
    /// <summary>
    /// 스킬 타겟 수
    /// </summary>
    private int skillTotalTarget = 2;
    #endregion

    /*  프로퍼티 (09/28) 아직 미정
    public int SkillDamage { set { skillDamage = value; } }
    public int SkillTotalTarget { set { skillTotalTarget = value; } }
    */

    private void Start()
    {
        ThunderSlashInit();
    }

    private void ThunderSlashInit()
    {
        thunderclapEffect = GetComponentInChildren<TrailRenderer>();
        thunderclapEffect.enabled = false;
    }

    public override void OnActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;  // 스킬 쿨타임 상태로 변경
            ThunderSlashSkillAttack();
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

    private void ThunderSlashSkillAttack()
    {
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(playerObject.transform.position, 10f, enemyLayer);

        if (enemyCollider.Length > 0)
        {
            OnSkillEffect();
            ThunderSlashEffect(); // 번개 효과 이펙트 (Trail Renderer) 활성화
            Time.timeScale = 0.1f;
            StartCoroutine(ThunderSlashSkillAttackSkillProcess(enemyCollider));
        }
    }

    IEnumerator ThunderSlashSkillAttackSkillProcess(Collider2D[] enemyColiders)
    {
       GameObject[] lockOnEffect = new GameObject[enemyColiders.Length];

        for(int enemyCount = 0; enemyCount < enemyColiders.Length; enemyCount++)
        {
            lockOnEffect[enemyCount] = Instantiate(thunderSlashLockOnEffect, enemyColiders[enemyCount].transform.position, Quaternion.identity);
            yield return new WaitForSecondsRealtime(1f);   // 캐싱 하자

        }

        for (int enemyCount = 0; enemyCount < enemyColiders.Length; enemyCount++)
        {
            GameManager.Instance.PlayerCameraMoveSpeed = 0f;
            playerObject.transform.position = enemyColiders[enemyCount].transform.position;

           // TODO : 데미지 처리 하기 (10/30)
           // Destroy(enemyColiders[enemyCount].gameObject, 0.5f);

            yield return new WaitForSecondsRealtime(0.2f);  // 캐싱 하자
            GameManager.Instance.PlayerCameraMoveSpeed = 10f;

            yield return new WaitForSecondsRealtime(1f);   // 캐싱 하자
            Destroy(lockOnEffect[enemyCount]);
        }
        OffSkillEffect();
        Time.timeScale = 1.0f;
 
        Invoke(nameof(ThunderSlashEffect), 1f); // 번개 효과 이펙트 (Trail Renderer) 비활성화
        OnCoolTime(); // 스킬 쿨타임 진행
    }

    private void OnSkillEffect()
    {
        PlayerCamera.Instance.ChagnePostProcessProfile(thunderClapProfile);
    }

    private void OffSkillEffect()
    {
        PlayerCamera.Instance.ChagnePostProcessProfile(null);
    }

    private void ThunderSlashEffect()
    {
        thunderclapEffect.enabled = !thunderclapEffect.enabled;
    }

}
