using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : Enemy
{
    public enum BossState{
        MOVE_STATE,
        ATTACK_STATE,
        HURT_STATE,
        DEAD_STATE,
        PATTERN_DARKHEAL_STATE,
        PATTERN_RUINSTK_STATE,
        PATTERN_SUMNSKELETON_STATE,
        PATTERN_BIND_STATE            
    };


    
    BossFSM bossFSM;
    [SerializeField] GameObject player;
    private Vector2 dir;
    Transform target;


    //[SerializeField] AnimationTriggers attack;

    Rigidbody2D boss;
    Animator boss_ani; 

    
    [SerializeField] [Range(0f, 50f)] float contactDistance;    //보스의 사정거리 초기값 10
    
    
    public bool follow = false;
    private float scaleX;

    // 보스 기본공격 투사체
    [SerializeField] GameObject bossProjectileSkull;
    readonly float BOSS_DEFAULT_ATTACK_SPEED = 1.5f;
    readonly float BOSS_PROJECTILE_SKULL_SPEED = 10f; 

    // Start is called before the first frame update
    void Start()
    {
        bossFSM = new BossFSM(this);
        scaleX = transform.localScale.x; 
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boss_ani = GetComponent<Animator>();
        boss = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDir();
       // bossFSM.bossState = BossState.MOVE_STATE;
        bossFSM.Update();
        
    }
    /// <summary>
    /// 플레이어 오브젝트의 위치에 따라서 보스 오브젝트의 스프라이트 좌우 반전을 컨트롤 하는 함수
    /// </summary>
    private void ChangeDir()
    {
        if (dir.x > 0)
        {
            //스테이지 매니져에서 보스를 만들어야 할지, 아니면 boss내에서 자체 처리할지, 

            boss.transform.localScale = new Vector2(scaleX, transform.localScale.y);
            //EnemyRigidbody.transform.localScale = new Vector2(scaleX, transform.localScale.y);
        }
        else
        {
            boss.transform.localScale = new Vector2(-scaleX, transform.localScale.y);
            //EnemyRigidbody.transform.localScale = new Vector2(-scaleX, transform.localScale.y);
        }
    }
  /// <summary>
  /// 플레이어와 보스의 위치차이가 contactDistance가 멀다면 dir에 플레이어가 있는 방향으로 설정, 보스의 좌표를 이동함. 
  /// 만약 contackDistance보다 짧은 위치에 플레이어와 보스가 있다면 보스는 State를 Attack으로 설정함. 
  /// </summary>
    public void Move()
    {
        
        Debug.Log("Move");
        if (Vector2.Distance(transform.position, target.position) > contactDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
            dir = (player.transform.position - transform.position).normalized;
        }
        else
        {
            bossFSM.bossState = BossState.ATTACK_STATE;
            //bossFSM.Update();
            
        }
    }

    bool check = true;

    /// <summary>
    /// 보스와 플레이어가 가깝다면 boss의 상태를 attack상태로 변환하고 보스의 veclocity를 zero로 해줌으로써 공격중에 이동하는 현상을 막는다.
    /// </summary>
    public void Attack()
    {
     
        if (Vector2.Distance(transform.position, target.position) > contactDistance)
        {
            bossFSM.bossState = BossState.MOVE_STATE;
        }
        else if(Vector2.Distance(transform.position, target.position) < contactDistance)
        {
            Debug.Log("Attack");
            boss_ani.SetTrigger("DefaultAttack");
            boss.velocity = Vector2.zero;
            CreateBossProjectileSkull();
            //EnemyAnimator.SetTrigger("DefaultAttack");
            //EnemyRigidbody.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// 애니메이션 이벤트 기본공격에서 호출
    /// </summary>
    private void CreateBossProjectileSkull()
    {
        GameObject projectile = MemoryPoolManager.GetInstance().OutputGameObject(bossProjectileSkull,
                                                                                Define.PrefabType.Boss_Skill,
                                                                                transform.position,
                                                                                Quaternion.identity);
        projectile.GetComponent<Projectile>().ProjectileInit(dir, DefaultAttackDamage, BOSS_PROJECTILE_SKULL_SPEED);
        projectile.SetActive(true);
    }

    public void Hurt()
    {
      
    }
    public void OnDead()
    {

    }
    public void Pattern_DarkHeal()
    {

    }
    public void Pattern_RuinStk()
    {

    }
    public void Pattern_SummonSkeleton()
    {

    }
    public void Pattern_Bind()
    {
       
    }



   private void ChangeCheck()
    {
        check = false;
    }

    private void StopMove()
    {
       
       
    }

    



}

