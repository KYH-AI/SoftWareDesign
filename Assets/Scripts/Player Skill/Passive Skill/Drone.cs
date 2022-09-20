using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Drone : PassiveSkill
{
    private LineRenderer droneBulletLine;
    private Light2D droneMuzzleFlash;

    private Coroutine droneAttackCheck;
    private Coroutine droneRotateCheck;
    private bool isDroneStop = false;

    private readonly float DRONE_MOVE_SPEED = 50f;      // 드론 이동 속도 (상수 값)
    private readonly float DRONE_RETARGET_TIME = 1f;     // 드론 타켓 변경 시간(상수 값)
    private readonly float DRONE_BULLET_LINE_DURATION = 0.1f;   // 드론 공격 이펙트 지속 시간(상수 값)

    private WaitForSeconds droneBulletLineDuration; // 드론 공격 이펙트 지속시간
    private WaitForSeconds droneRetargetDelay;     // 드론 타겟 변경 딜레이

    private float lastTime = 0f;        // 드론 마지막 사격 시간
    private float skillDelayTime = 5f;  // 드론 공격 대기시간 (쿨타임)  [ 초기 쿨타임 = 5초 예상 ]
    public float SkillDelayTime { get { return skillDelayTime; } set { skillDelayTime = value; } }

    private float circleR = 3f; //반지름
    private float deg; //각도

    private void Start()
    {
        droneBulletLineDuration = new WaitForSeconds(DRONE_BULLET_LINE_DURATION); // 캐싱
        droneRetargetDelay = new WaitForSeconds(DRONE_RETARGET_TIME);             // 캐싱

        droneBulletLine = GetComponent<LineRenderer>();
        droneMuzzleFlash = GetComponent<Light2D>(); 
        droneBulletLine.enabled = false; // 라인렌더러 컴포넌트 비활성화 
    }

    private void Update()  // 드론 사격 여부 확인
    {
        if(!isDroneStop && Time.time >= lastTime + skillDelayTime) // 마지막 공격 시간 + 공격 쿨타임
        {
            lastTime = Time.time;
            droneAttackCheck = StartCoroutine(DroneAttack());
        }
    }

    public override void OnActive()
    {
        DroneSkillActive();
    }

    private void DroneSkillActive() // 드론 이동 여부 확인
    {
        if (droneAttackCheck == null && droneRotateCheck == null)
        {
            isDroneStop = false;
            droneRotateCheck = StartCoroutine(DroneAroundRotate(playerObject.transform));
            
        }
    }

    /// <summary>
    /// 드론 회전 코루틴
    /// </summary>
    /// <param name="target">회전활 기준점이 될 오브젝트</param>
    /// <returns></returns>
    IEnumerator DroneAroundRotate(Transform target)
    {
        while (!isDroneStop)
        {
            // 소스 참고 : https://sharp2studio.tistory.com/4?category=884092 //
            deg += Time.deltaTime * DRONE_MOVE_SPEED;
            if (deg < 360)
            {
                var rad = Mathf.Deg2Rad * (deg);
                var x = circleR * Mathf.Sin(rad);
                var y = circleR * Mathf.Cos(rad);
                transform.position = target.position + new Vector3(x, y);
                transform.rotation = Quaternion.Euler(0, 0, deg); //가운데를 바라보게 각도 조절
            }
            else
            {
                deg = 0;
            }
            yield return null;
        }

        droneRotateCheck = null;
       // Debug.Log("드론 회전 종료");
    }


    IEnumerator DroneAttack()
    {
      //  Debug.Log("공격 시작");
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(playerObject.transform.position, 10f, enemyLayer); // 플레이어 기준 10범위에 적을 탐지

        if (enemyCollider.Length > 0) // 적 배열이 0보다 많으면
        {
          //  droneRetargetDelay = new WaitForSeconds(DRONE_RETARGET_TIME);  TODO : 드론 타겟변경 시간감소. (업그레이드에서 요소 중 하나)

            for (int i=0; i< enemyCollider.Length; i++)
            {
                if (enemyCollider[i] != null)
                {
                    Enemy enemy = enemyCollider[i].GetComponent<Enemy>();
                    StartCoroutine(DroneBulletEffect(enemy.transform.position));
                    enemy.TakeDamage(damage);

                    lastTime += DRONE_RETARGET_TIME;   // 공격 쿨타임에 타켓변경 시간까지 추가
                    yield return droneRetargetDelay;
                }
            }
        }
        droneAttackCheck = null;
    }

    /// <summary>
    /// 드론 공격 이펙트
    /// </summary>
    /// <param name="enemyPosition">공격 대상 위치</param>
    /// <returns>droneBulletLineDuration 만큼 기다린 후 라인 렌더러 제거</returns>
    IEnumerator DroneBulletEffect(Vector2 enemyPosition)
    {
        droneBulletLine.SetPosition(0, transform.position); // 라인 렌더러 시작 위치
        droneBulletLine.SetPosition(1, enemyPosition);      // 라인 렌더러 도착 위치

        droneMuzzleFlash.enabled = true; // 2D Light 컴포넌트 활성화
        droneBulletLine.enabled = true;  // 라인 렌더러 컴포넌트 활성화

        yield return droneBulletLineDuration;

        droneBulletLine.enabled = false; // 라인 렌더러 컴포넌트 비활성화
        droneMuzzleFlash.enabled = false; // 2D Light 컴포넌트 비활성화
    }
}
