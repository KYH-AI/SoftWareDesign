using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 메이플 전사 파워 슬래쉬 기술
/// </summary>
public class PowerSlash : ActiveSkill
{
    private readonly float DASH_DISTANCE = 7f;  // 대쉬 범위
    private TrailRenderer powerSlashEffect;

    private void Start()
    {
        powerSlashEffect = GetComponentInChildren<TrailRenderer>();

        powerSlashEffect.enabled = false;
    }

    public override void OnActive()
    {

        PowerSlashSkillAttack();
    }


    private void PowerSlashSkillAttack()
    {
        AcitvePowerDashEffect();  // 이펙트 활성화

        Vector3 firstPosition = playerObject.transform.position;
        float maxDistance = DASH_DISTANCE;

        // x, y 마지막 방향을 이용해 해당 방향으로 10m 날라간다.

        RaycastHit2D hitObject = Physics2D.Raycast(transform.position, playerController.LastDir, maxDistance, LayerMask.GetMask("wallLayer"));

        if (hitObject)  // RayCast가 벽에 충돌했다는 의미
        {
            float Distance = hitObject.distance;     // 벽 충돌 위치까지만 Ray를 쏘기 위한 거리 측정
            //playerObject.transform.position = hitObject.normal; // 충돌한 벽 앞 까지만 이동
            //if(playerController.LastDir.normalized == new Vector3(0, -1, 0) { }
            playerObject.transform.Translate(playerController.LastDir.normalized * (Distance-0.5f));
        }

        else
        {
            playerObject.transform.Translate(playerController.LastDir.normalized * maxDistance); // 마지막으로 본 방향 + dashDistance 길이 만큼 이동
        }


        RaycastHit2D[] enemyObject = Physics2D.RaycastAll(firstPosition, playerController.LastDir, maxDistance, enemyLayer);
        /*
        Debug.Log("스킬 충돌 거리 : " + dashDistance);
        Debug.Log("충돌한 적 : " + enemyObject.Length);
        */


        if (enemyObject.Length > 0)
        {
            for (int i = 0; i < enemyObject.Length; i++)
            {
                Destroy(enemyObject[i].transform.gameObject, 0.5f);
            }
        }

        Invoke(nameof(AcitvePowerDashEffect), 1f); //  이펙트 비활성화
    }

    private void AcitvePowerDashEffect()
    {
        powerSlashEffect.enabled = !powerSlashEffect.enabled;
    }    
}