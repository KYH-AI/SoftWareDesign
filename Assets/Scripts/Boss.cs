using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : Enemy
{


    #region ���� ���� ���� ����
    BossFSM bossFSM;            //������ �ൿ�� �����ϴ� BossFSM Ŭ������ �����ϴ� ����
    [SerializeField] [Range(0f, 50f)] float contactDistance;    //������ �����Ÿ� �ʱⰪ 10
    private float scaleX;       //������ scaleX��
    [SerializeField] GameObject bossProjectileSkull;
    readonly float BOSS_DEFAULT_ATTACK_SPEED = 1.5f;
    readonly float BOSS_PROJECTILE_SKULL_SPEED = 10f;
    float bossHpPercentage; 

    #endregion
    #region �÷��̾�&���� ���� ���� ����
    [SerializeField] GameObject player;
    Transform target;
    private Vector2 dir;
    #endregion
    #region �������� �Ŵ����� �����ϱ� �� ���� �ӽ� Rigid, Animator ����
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

    #region ���� �̵� ���� �Լ�
    /// <summary>
    /// ������ �÷��̾��� ��ġ�� ���� �̵���Ű�� Move�Լ�
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
    ///  ����, ����ü�� ��������Ʈ �̹����� �÷��̾ �ִ� �������� �˸°� �ٲ��ִ� �Լ�
    /// </summary>
    /// <param name="target"></param>
    private void SwitchSpriteImageDir(Transform target)
    {
        if (dir.x > 0)
        {
            //�������� �Ŵ������� ������ ������ ����, �ƴϸ� boss������ ��ü ó������, 
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

    #region ���� ����, ���� �Լ�

    /// <summary>
    /// ������ ���ݻ����Ÿ��� ���� �÷��̾ �����ϰų� MOVE_STATE�� �����Ű�� �Լ�
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
    /// ������ �⺻���� �� �ذ� ����ü�� �����ϴ� �Լ�
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
    /// �̵� �� ���� ���� �� �ǰ�ȿ���� �����Ű�� �Լ�
    /// </summary>
    public override sealed void TakeDamage(int newDamage)
    {
        
        StartCoroutine(SwitchMaterial());
        base.TakeDamage(newDamage);
        bossHpPercentage = (bossHpPercentage / (float)MaxHp * 100);
        if(((float)Hp < ((float)MaxHp * 0.7f)) && ((float)Hp > ((float)MaxHp * 0.15f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_BIND_STATE;
            //SetAnimationTrigger("RunBindMotion");
        }
        else if (((float)Hp < ((float)MaxHp * 0.5f)) && ((float)Hp > ((float)MaxHp * 0.3f))) 
            {
            bossFSM.bossState = Define.BossState.PATTERN_SUMNSKELETON_STATE;
            //SetAnimationTrigger("RunSumnSkeleton"); 
        }
        else if (((float)Hp < ((float)MaxHp * 0.4f)) && ((float)Hp > ((float)MaxHp * 0.21f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_RUINSTK_STATE;
            //SetAnimationTrigger("RunRuinStk");
        } else if (((float)Hp < ((float)MaxHp * 0.2f)) && ((float)Hp > ((float)MaxHp * 0.06f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_RUINSTK_STATE;
            //SetAnimationTrigger("RunRuinStk");
        }
        else if (((float)Hp < ((float)MaxHp * 0.04f)) && ((float)Hp > ((float)MaxHp * 0.01f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_RUINSTK_STATE;
            //SetAnimationTrigger("RunRuinStk");
        }
        
        //bossHpPercentage������  ������ �����  �����Ű�� ū �޼ҵ�(Run_Pattern)�� ������. 


        //Dark heal = 1�� 30�ʸ��� �ݺ�,
        //ruin stk = ������ ü���� 40,20,5�� �� �� 
        //sumn skt = ������ ü���� 50%
        //bind = ���� ü�� 70%, 15% ,
        //�������� ü���� Hp�� �����ϸ��   

        //EnemyAnimator.SetTrigger("RunTakeDamaged");
    }
    public void RunPattern(float currentHp)
    {
        
    }
    


    /// <summary>
    /// �ǰ�ȿ���� �����ð� ������Ű�� �ڷ�ƾ-IEnumerator �Լ�
    /// </summary>
    IEnumerator SwitchMaterial()
    {
        bossSpriteRenderer.material = hurtMaterial;
        yield return new WaitForSeconds(0.1f);
        bossSpriteRenderer.material = originalMaterial;
    }


    /// <summary>
    /// ������ ����ü���� 0�̵Ǹ� �����Ű�� �Լ�
    /// </summary>
    protected override void OnDead()
    {
        bossFSM.bossState = Define.BossState.DEAD_STATE;
        SetAnimationTrigger("RunDead"); 
        //��� �ִϸ��̼ǰ� ������Ʈ ����, ������Ʈ �ı��� �̷������ UI�� ȣ���� 
    }

    private void Die()
    {
        
        Destroy(this);
    }
    
    /// <summary>
    /// ���� ��ũ �� ���� �Լ�
    /// 1�� 30�ʸ��� �ݺ��ǰ� 8�ʵ��� �������� ����
    /// ���� �������� 1,000 �̻� �ް� �ȴٸ� ������ �ִ� �������� �����(���� �÷��̾� �ٽ� ������)
    /// ȸ������ ���� �ִ� ü���� 15%�� ȸ���� 
    /// </summary>
    public void Pattern_DarkHeal()
    {
        SetAnimationTrigger("RunDarkHealMotion");
    }
    /// <summary>
    /// ���� ���� ��Ʈ����ũ ���� �Լ�
    /// ������ ü���� 40%, 25%, 5%�� �Ǿ��� ������ ���� 
    /// ������ ���ǿ� ���� 4��,6,��,9���� ����ü�� ������ ��ġ�� ������
    /// </summary>
    public void Pattern_RuinStk()
    {
        SetAnimationTrigger("RunRuinStk"); 
        
    }
    /// <summary>
    /// ���� ���̷��� ��ȯ ���� �Լ�
    /// ������ ü���� 50%�� �Ǿ��� �� �����Ǹ� ��ü���� ����. 
    /// ���̷����� ���ݷ��� 20, �⺻ ���� ���ݹۿ� ����. ü���� ���� ���� ����. 
    /// </summary>
    public void Pattern_SummonSkeleton()
    {
        SetAnimationTrigger("RunSumnSkeleton");
    }
    /// <summary>
    /// ���� ���ε� ���� �Լ�
    /// ü�� 70%, 15%�� �Ǿ��� �� �����. 
    /// Quick Time Event�� ���ÿ� ����Ǹ� �̸� �����ϸ� 5�� �ӹ�. 
    /// </summary>
    public void Pattern_Bind()
    {
        SetAnimationTrigger("RunBindMotion");
    }

    #endregion


    #region �ִϸ��̼� ��Ʈ�ѷ�{
    public void SetAnimationTrigger(string trigger)
    {
        boss_ani.SetTrigger(trigger);
        //EnemyAnimator.SetTrigger(trigger);
    }
    #endregion






}

