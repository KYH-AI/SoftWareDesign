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
    [SerializeField] GameObject bossProjectileRuinStk;
    [SerializeField] GameObject bossProjectileDarkHeal;
    [SerializeField] GameObject bossProjectileDarkHealFailed;
    readonly float BOSS_DEFAULT_ATTACK_SPEED = 1.5f;
    readonly float BOSS_PROJECTILE_SKULL_SPEED = 10f;
    readonly float BOSS_PATTERN_DARK_HEAL_COUNT = 90f;     

    private float bossHpPercentage;
    private int ruinStrikeQty;
    Rigidbody2D boss;
    Animator boss_ani;
    private bool patternCheck = false;      // == isgod. true이면 무적임. 
    private float patternCheckTimer=0.0f;     //경과시간 
    private float darkHealCheckTimer = 0.0f;
    private int darkHealTempHp = 1000;
    #endregion

    #region 플레이어&방향 관련 변수 선언
    [SerializeField] GameObject player;
    Transform target;
    private Vector2 dir;
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
        patternCheckTimer += Time.deltaTime;

        if(patternCheckTimer > BOSS_PATTERN_DARK_HEAL_COUNT)
        {
            Debug.Log(patternCheckTimer+"초");
            bossFSM.bossState = Define.BossState.PATTERN_DARKHEAL_STATE;
            patternCheckTimer = 0;

        }
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
        if(patternCheck)
        {
            return;
        }
        if(bossFSM.runDarkHeal)       //CASTING 상태 일 때 
        {
            darkHealTempHp -= newDamage;
            StartCoroutine(SwitchMaterial());
            return;
        }
        StartCoroutine(SwitchMaterial());
        base.TakeDamage(newDamage);
        bossHpPercentage = (bossHpPercentage / (float)MaxHp * 100);
        RunPattern(bossHpPercentage);
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
        SetAnimationTrigger("RunDead"); 
        //모든 애니메이션과 스테이트 정리, 오브젝트 파괴가 이루어지면 UI를 호출함 
    }
    private void Die()
    {
        Destroy(this);
    }
    /// <summary>
    /// 보스의 체력에 따라 해당하는 기능을 실행시키는 함수
    /// </summary>
    /// <param name="hpPercentage"></param>
    public void RunPattern(float hpPercentage)
    {
        if ((hpPercentage <= 70f) && (hpPercentage >= 15f))
        {
            bossFSM.bossState = Define.BossState.PATTERN_BIND_STATE;
            //SetAnimationTrigger("RunBindMotion");
        }
        else if (((hpPercentage <= 50f) && (hpPercentage >= 30.1f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_SUMNSKELETON_STATE;
            //SetAnimationTrigger("RunSumnSkeleton"); 
        }
        else if (((hpPercentage <= 40f) && (hpPercentage >= 20f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_RUINSTK_STATE;
            ruinStrikeQty = 4;
            //SetAnimationTrigger("RunRuinStk");
        }
        else if (((hpPercentage <= 20f) && (hpPercentage >= 6f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_RUINSTK_STATE;
            ruinStrikeQty = 6;
            //SetAnimationTrigger("RunRuinStk");
        }
        else if (((hpPercentage <= 5f) && (hpPercentage >= 1f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_RUINSTK_STATE;
            ruinStrikeQty = 9;
            //SetAnimationTrigger("RunRuinStk");
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 패턴 다크 힐 실행 함수
    /// 1분 30초마다 반복되고 8초동안 에너지를 모음
    /// 만약 데미지를 1,000 이상 받게 된다면 모으고 있던 에너지는 흩어짐(이후 플레이어 다시 재추적)
    /// 회복량으 보스 최대 체력의 15%를 회복함 
    /// </summary>
    public void Pattern_DarkHeal()
    {
        print("DarkHeal시작함");
        boss.velocity = Vector2.zero;
        SetAnimationTrigger("RunDarkHealMotion");
        bossFSM.bossState = Define.BossState.CASTING_STATE;
        RunDarkHeal();
       
    }
 
    public void RunDarkHeal()
    {
        StartCoroutine(DarkHealProcess());
    }
    IEnumerator DarkHealProcess()
    {
        print("다크힐 코루틴");

        GameObject darkHeal = CreateBossProjectileDarkHeal();

        while (true)
        {
            
            darkHealCheckTimer += Time.deltaTime;
            if (darkHealCheckTimer >= 8.0f && darkHealTempHp > 0)          //시간 카운터가 8초가 지나고 1,000 데미지 이하로 받았을 때
            {
                Hp += MaxHp * 15 / 100;     //전체값의 15%만큼 증가
                break;
            }
            else if (darkHealCheckTimer < 8.0f && darkHealTempHp <= 0)     //시간 카운터가 8초가 지나지 않고 1,000데미지 이상으로 받았을 때
            {
                CreateBossProjectileDarkHealFailed();
                break;
            }
        }
        Destroy(darkHeal);
        darkHealCheckTimer = 0;
        darkHealTempHp = 1000;
        bossFSM.runDarkHeal = false;
        bossFSM.bossState = Define.BossState.MOVE_STATE;
        yield return new WaitForSeconds(8.0f);
    }
  
    private GameObject CreateBossProjectileDarkHeal()
    {
        GameObject projectile = Instantiate(bossProjectileDarkHeal, 
                                            new Vector2(transform.position.x, transform.position.y + 15),
                                            Quaternion.identity);
        projectile.SetActive(true);
        return projectile;
    }
    private void CreateBossProjectileDarkHealFailed()
    {
        GameObject projectile = Instantiate(bossProjectileDarkHealFailed,
                                           new Vector2(transform.position.x, transform.position.y + 15),
                                           Quaternion.identity);
        projectile.SetActive(true);
        
    }

    /// <summary>
    /// 패턴 루인 스트라이크 실행 함수
    /// 보스의 체력이 40%, 25%, 5%가 되었을 때마다 실행 
    /// 들어오는 조건에 따라 4개,6,개,9개의 투사체가 랜덤한 위치에 생성됨
    /// </summary>
    public void Pattern_RuinStk()
    {
        print("패턴 시작");
        boss.velocity = Vector2.zero;
        SetPatternCheck();
        RunRuinStk();
        bossFSM.bossState = Define.BossState.CASTING_STATE;
    }
    /// <summary>
    /// 루인스트라이크 코루틴을 실행시키는 함수
    /// </summary>
    private void RunRuinStk()
    {
        StartCoroutine(RuinStrikeProcess(ruinStrikeQty));
    }
    IEnumerator RuinStrikeProcess(int qty)
    {
        print("루인 스트라이크 코루틴");
        for (int i = 0; i < qty; i++)
        {
            SetAnimationTrigger("RunRuinStk");
            print(i + "번 발사");
            yield return new WaitForSeconds(0.8f); 
        }
        bossFSM.bossState = Define.BossState.MOVE_STATE;
    }

    // 투사체를 반복문으로 만들지 말고 함수를 따로 빼자. 어케하노..ㅠ 
    private void CreateBossProjectileRuinStk()
    {
        GameObject projectile = MemoryPoolManager.GetInstance().OutputGameObject(bossProjectileRuinStk,
                                                                                Define.PrefabType.Boss_Skill,
                                                                                new Vector2(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f)),
                                                                                Quaternion.identity);
        projectile.GetComponent<Projectile>().ProjectileInit(Define.StringTag.Player, Vector2.zero, 400);
        projectile.SetActive(true);
    }

    /// <summary>
    /// 패턴 스켈레톤 소환 실행 함수
    /// 보스의 체력이 50%가 되었을 때 시전되며 개체수는 미정. 
    /// 스켈레톤의 공격력은 20, 기본 근접 공격밖에 없음. 체력은 조금 높게 설정. 
    /// </summary>
    public void Pattern_SummonSkeleton()
    {
        SetAnimationTrigger("RunSumnSkeleton");
    }
    /// <summary>
    /// 패턴 바인드 실행 함수
    /// 체력 70%, 15%가 되었을 때 실행됨. 
    /// Quick Time Event가 동시에 실행되며 이를 실패하면 5초 속박. 
    /// </summary>
    public void Pattern_Bind()
    {
        SetAnimationTrigger("RunBindMotion");
    }

    #endregion

    #region 애니메이션 관련 함수
    /// <summary>
    /// 매개변수로 받는 스트링의 값에 따라 미리 설정된 애니메이션을 실행시키는 함수
    /// </summary>
    /// <param name="trigger"></param>
    public void SetAnimationTrigger(string trigger)
    {
        boss_ani.SetTrigger(trigger);
        //EnemyAnimator.SetTrigger(trigger);
    }
    /// <summary>
    /// 패턴 시작 시 보스를 무적을 만들기 위해 쓰는 함수
    /// </summary>
    private void SetPatternCheck()
    {
        patternCheck = patternCheck != true;
    }
  
    #endregion






}

