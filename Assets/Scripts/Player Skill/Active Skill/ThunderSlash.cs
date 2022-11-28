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
            currentSkillState = ThunderSlashSkillAttack();
        }
        else
        {
            // TODO : UI에서 "아직 재사용 대기시간 입니다." 연출하기
            return;
        }
    }

    public override void Upgrade()
    {
        skillDamage += 10;
        SkillCoolTime -= 2;
        skillTotalTarget += 3;
        // TODO : 상점에서 업그레이드 방식이 정해지면 진행 하자 (09/28)
    }

    private Define.CurrentSkillState ThunderSlashSkillAttack()
    {
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(playerObject.transform.position, 10f, enemyLayer);

        if (enemyCollider.Length > 0)
        {
            playerObject.PlayerController.isAttackalble = false; // 침묵 효과
            playerObject.PlayerController.isMoveable = false; // 이동제어


            OnSkillEffect();
            ThunderSlashEffect(); // 번개 효과 이펙트 (Trail Renderer) 활성화
            Time.timeScale = 0.1f;
           // StageManager.GetInstance().Player.PlayerController.PlayerTimeScale = Time.timeScale;
            StartCoroutine(ThunderSlashSkillAttackSkillProcess(enemyCollider));
            return Define.CurrentSkillState.COOL_TIME;
        }
        return Define.CurrentSkillState.ACTIVE;
    }

    IEnumerator ThunderSlashSkillAttackSkillProcess(Collider2D[] enemyColliders)
    {
        GameObject[] lockOnEffect = new GameObject[skillTotalTarget];
        int targetCount = enemyColliders.Length > skillTotalTarget ? skillTotalTarget : enemyColliders.Length;

        /*
        // 적이 더 적은경우
        if (skillTotalTarget > enemyColliders.Length)
        {
            tempTotalTaregt = enemyColliders.Length;
        }
        */

        for(int enemyCount = 0; enemyCount < targetCount; enemyCount++)
        {
            if (enemyColliders[enemyCount] != null)
            {
                lockOnEffect[enemyCount] = Instantiate(thunderSlashLockOnEffect, enemyColliders[enemyCount].transform.position, Quaternion.identity);
                yield return new WaitForSecondsRealtime(1f);   // 캐싱 하자
            }
        }

        for (int enemyCount = 0; enemyCount < targetCount; enemyCount++)
        {
            playerObject.SwitchPlayerSprite((playerObject.transform.position - enemyColliders[enemyCount].transform.position).normalized, true);
            // GameManager.Instance.PlayerCameraMoveSpeed = 0f;
            playerObject.transform.position = enemyColliders[enemyCount].transform.position;


            // TODO : 데미지 처리 하기 (10/30)
            enemyColliders[enemyCount].GetComponent<Enemy>().TakeDamage(skillDamage);

             yield return new WaitForSecondsRealtime(1f);  // 캐싱 하자
          //  GameManager.Instance.PlayerCameraMoveSpeed = 10f;

          //  yield return new WaitForSecondsRealtime(1f);   // 캐싱 하자
            Destroy(lockOnEffect[enemyCount]);
        }
        OffSkillEffect();
        Time.timeScale = 1.0f;
       // StageManager.GetInstance().Player.PlayerController.PlayerTimeScale = Time.timeScale;
     
        playerObject.PlayerController.isAttackalble = true; // 침묵 효과
        playerObject.PlayerController.isMoveable = true; // 이동제어

        Invoke(nameof(ThunderSlashEffect), 1f); // 번개 효과 이펙트 (Trail Renderer) 비활성화
        OnCoolTime(); // 스킬 쿨타임 진행
    }

    private void OnSkillEffect()
    {
        Managers.SkillEffectVolume.ChagnePostProcessProfile(thunderClapProfile);
    }

    private void OffSkillEffect()
    {
        Managers.SkillEffectVolume.ChagnePostProcessProfile(null);
    }

    private void ThunderSlashEffect()
    {
        thunderclapEffect.enabled = !thunderclapEffect.enabled;
    }

}
