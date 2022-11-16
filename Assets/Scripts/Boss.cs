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

    
    [SerializeField] [Range(0f, 50f)] float contactDistance;    //������ �����Ÿ� �ʱⰪ 10
    
    
    public bool follow = false;
    private float scaleX;

    // ���� �⺻���� ����ü
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
    /// �÷��̾� ������Ʈ�� ��ġ�� ���� ���� ������Ʈ�� ��������Ʈ �¿� ������ ��Ʈ�� �ϴ� �Լ�
    /// </summary>
    private void ChangeDir()
    {
        if (dir.x > 0)
        {
            //�������� �Ŵ������� ������ ������ ����, �ƴϸ� boss������ ��ü ó������, 

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
  /// �÷��̾�� ������ ��ġ���̰� contactDistance�� �ִٸ� dir�� �÷��̾ �ִ� �������� ����, ������ ��ǥ�� �̵���. 
  /// ���� contackDistance���� ª�� ��ġ�� �÷��̾�� ������ �ִٸ� ������ State�� Attack���� ������. 
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
    /// ������ �÷��̾ �����ٸ� boss�� ���¸� attack���·� ��ȯ�ϰ� ������ veclocity�� zero�� �������ν� �����߿� �̵��ϴ� ������ ���´�.
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
    /// �ִϸ��̼� �̺�Ʈ �⺻���ݿ��� ȣ��
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

