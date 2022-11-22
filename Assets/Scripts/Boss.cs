using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : Enemy
{


    #region 보스 관련 변수 선언
    BossFSM bossFSM;            //보스의 행동을 제어하는 BossFSM 클래스로 접근하는 변수
    [SerializeField] [Range(0f, 50f)] float contactDistance;    //보스의 사정거리 초기값 10
    private float scaleX;       //보스의 scaleX값
    [SerializeField] GameObject bossProjectileSkull;
    readonly float BOSS_DEFAULT_ATTACK_SPEED = 1.5f;
    readonly float BOSS_PROJECTILE_SKULL_SPEED = 10f;

    #endregion
    #region 플레이어&방향 관련 변수 선언
    [SerializeField] GameObject player;
    Transform target;
    private Vector2 dir;
    #endregion
    #region 스테이지 매니져와 연동하기 전 쓰는 임시 Rigid, Animator 변수
    Rigidbody2D boss;
    Animator boss_ani;
    #endregion

    [SerializeField] Material originalMaterial;
    [SerializeField] Material hurtMaterial;
    SpriteRenderer bossSpriteRenderer;
    void Start()
    {
        base.Start();
        bossFSM = new BossFSM(this);
        scaleX = transform.localScale.x; 
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boss_ani = GetComponent<Animator>();
        boss = GetComponent<Rigidbody2D>();
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        SwitchSpriteImageDir(transform);
        if(bossFSM != null) bossFSM.Update();
    }

    #region 보스 이동 관련 함수
    /// <summary>
    /// 보스와 플레이어의 위치에 따라 이동시키는 Move함수
    /// </summary>
    public void Move()
    {

        
        if (Vector2.Distance(transform.position, target.position) > contactDistance)
        {
            print(MoveSpeed);
            transform.position = Vector2.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
            dir = (player.transform.position - transform.position).normalized;

        }
        else
        {
            bossFSM.bossState = Define.BossState.ATTACK_STATE;
        }
    }
    /// <summary>
    ///  보스, 투사체의 스프라이트 이미지를 플레이어가 있는 방향으로 알맞게 바꿔주는 함수
    /// </summary>
    /// <param name="target"></param>
    private void SwitchSpriteImageDir(Transform target)
    {
        if (dir.x > 0)
        {
            //스테이지 매니져에서 보스를 만들어야 할지, 아니면 boss내에서 자체 처리할지, 
            target.localScale = new Vector2(scaleX, transform.localScale.y);
            //EnemyRigidbody.transform.localScale = new Vector2(scaleX, transform.localScale.y);
        }
        else
        {
            target.transform.localScale = new Vector2(-scaleX, transform.localScale.y);
            //EnemyRigidbody.transform.localScale = new Vector2(-scaleX, transform.localScale.y);
        }
    }
    #endregion

    #region 보스 공격, 패턴 함수

    /// <summary>
    /// 보스의 공격사정거리에 따라 플레이어를 공격하거나 MOVE_STATE를 실행시키는 함수
    /// </summary>
    public void Attack()
    {

        if (Vector2.Distance(transform.position, target.position) > contactDistance)
        {
            bossFSM.bossState = Define.BossState.MOVE_STATE;
        }
        else if (Vector2.Distance(transform.position, target.position) < contactDistance)
        {
            SetAnimationTrigger("DefaultAttack");
            boss.velocity = Vector2.zero;
            //EnemyRigidbody.velocity = Vector2.zero;
        }
    }
    /// <summary>
    /// 보스의 기본공격 중 해골 투사체를 생성하는 함수
    /// </summary>
    private void CreateBossProjectileSkull()
    {
        dir = (player.transform.position - transform.position).normalized;
        GameObject projectile = MemoryPoolManager.GetInstance().OutputGameObject(bossProjectileSkull,
                                                                                Define.PrefabType.Boss_Skill,
                                                                                transform.position,
                                                                                Quaternion.identity);
        SwitchSpriteImageDir(projectile.transform);
        projectile.GetComponent<Projectile>().ProjectileInit(Define.StringTag.Player, dir, DefaultAttackDamage, BOSS_PROJECTILE_SKULL_SPEED);
        projectile.SetActive(true);
    }
    /// <summary>
    /// 이동 중 공격 받을 시 피격효과를 실행시키는 함수
    /// </summary>
    public override sealed void TakeDamage(int newDamage)
    {
        //  boss_ani.SetTrigger("RunTakeDamaged");
        StartCoroutine(SwitchMaterial());
        base.TakeDamage(newDamage);
       

        //EnemyAnimator.SetTrigger("RunTakeDamaged");
    }
    /// <summary>
    /// 피격효과를 일정시간 유지시키는 코루틴-IEnumerator 함수
    /// </summary>
    IEnumerator SwitchMaterial()
    {
        bossSpriteRenderer.material = hurtMaterial;
        yield return new WaitForSeconds(0.1f);
        bossSpriteRenderer.material = originalMaterial;
    }


    /// <summary>
    /// 보스의 현재체력이 0이되면 실행시키는 함수
    /// </summary>
    protected override void OnDead()
    {
        
        bossFSM.bossState = Define.BossState.DEAD_STATE;
        SetAnimationTrigger("RunDead"); // 그냥 이벤트로 DIE함수를 실행시키게 해봤음. 
             
        //오브젝트 파괴 함수/코드 삽입

        //모든 애니메이션과 스테이트 정리, 오브젝트 파괴가 이루어지면 UI를 호출함 
    }

    public void Die()
    {
        Destroy(this);
    }
    
    /// <summary>
    /// 패턴 다크 힐 실행 함수
    /// </summary>
    public void Pattern_DarkHeal()
    {

    }
    /// <summary>
    /// 패턴 루인 스트라이크 실행 함수
    /// </summary>
    public void Pattern_RuinStk()
    {

    }
    /// <summary>
    /// 패턴 스켈레톤 소환 실행 함수
    /// </summary>
    public void Pattern_SummonSkeleton()
    {

    }
    /// <summary>
    /// 패턴 바인드 실행 함수
    /// </summary>
    public void Pattern_Bind()
    {

    }

    #endregion


    #region 애니메이션 컨트롤러{
    public void SetAnimationTrigger(string trigger)
    {
        boss_ani.SetTrigger(trigger);
        //EnemyAnimator.SetTrigger(trigger);
    }
    #endregion






}

