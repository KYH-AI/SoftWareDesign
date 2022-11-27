using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 메이플 전사 파워 슬래쉬 기술
/// </summary>
public class WindDash : ActiveSkill
{
    private TrailRenderer windDlashEffect;

    #region 스킬 기본 스텟 데이터
    /// <summary>
    /// 스킬 데미지
    /// </summary>
    private int skillDamgae = 6;
    /// <summary>
    /// 스킬 대쉬 최대길이
    /// </summary>
    private float skillDashDistacne = 5f;
    #endregion

    #region 스킬 스텟 프로퍼티
    /// <summary>
    /// 스킬 데미지 프로퍼티 ( set : 스킬 데미지 값 변경 )
    /// </summary>
    public int SkillDamgae { set { skillDamgae = value; } }
    /// <summary>
    /// 스킬 대쉬 최대길이 프로퍼티 ( set : 스킬 대쉬 최대길이 값 변경 )
    /// </summary>
    public float SkillDashDistacne { set { skillDashDistacne = value; } }   
    #endregion

    private void Start()
    {
        WindSlashInit();
    }

    private void WindSlashInit()
    {
        windDlashEffect = GetComponentInChildren<TrailRenderer>();
        windDlashEffect.enabled = true;
        windDlashEffect.enabled = false;
    }

    public override void OnActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            WindSlashSkillAttack();
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

    private void WindSlashSkillAttack()
    {

        playerObject.SwitchPlayerSprite(playerObject.PlayerController.LastDirection, false);
        playerObject.PlayerController.IsSilence = true;
        playerObject.PlayerController.IsMoveable = false;
        OnSkillEffect();  // 이펙트 활성화

        Vector3 firstPosition = playerObject.transform.position;
        float maxDistance = skillDashDistacne;

        // x, y 마지막 방향을 이용해 해당 방향으로 10m 날라간다.

        RaycastHit2D hitObject = Physics2D.Raycast(transform.position, playerObject.PlayerController.LastDirection, maxDistance, wallLayer);

        if (hitObject)  // RayCast가 벽에 충돌했다는 의미
        {
            maxDistance = hitObject.distance;     // 벽 충돌 위치까지만 Ray를 쏘기 위한 거리 측정
            playerObject.transform.position = hitObject.normal; // 충돌한 벽 앞 까지만 이동
        }

        else
        {
            playerObject.transform.Translate(playerObject.PlayerController.LastDirection.normalized * maxDistance); // 마지막으로 본 방향 + dashDistance 길이 만큼 이동
        }


        RaycastHit2D[] enemyObjects = Physics2D.RaycastAll(firstPosition, playerObject.PlayerController.LastDirection, maxDistance, enemyLayer);
        /*
        Debug.Log("스킬 충돌 거리 : " + dashDistance);
        Debug.Log("충돌한 적 : " + enemyObject.Length);
        */


        if (enemyObjects.Length > 0)
        {
            for (int enemyCount = 0; enemyCount < enemyObjects.Length; enemyCount++)
            {
                enemyObjects[enemyCount].transform.GetComponent<Enemy>().TakeDamage(skillDamgae);
            }
        }

        playerObject.PlayerController.IsSilence = false;
        playerObject.PlayerController.IsMoveable = true;

        Invoke(nameof(OnSkillEffect), 1.5f); //  이펙트 비활성화
        OnCoolTime();

    }

    private void OnSkillEffect()
    {
        windDlashEffect.enabled = !windDlashEffect.enabled;
    }
}