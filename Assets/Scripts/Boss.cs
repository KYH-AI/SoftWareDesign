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
        //  boss_ani.SetTrigger("RunTakeDamaged");
        StartCoroutine(SwitchMaterial());
        base.TakeDamage(newDamage);
       

        //EnemyAnimator.SetTrigger("RunTakeDamaged");
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
        SetAnimationTrigger("RunDead"); // �׳� �̺�Ʈ�� DIE�Լ��� �����Ű�� �غ���. 
             
        //������Ʈ �ı� �Լ�/�ڵ� ����

        //��� �ִϸ��̼ǰ� ������Ʈ ����, ������Ʈ �ı��� �̷������ UI�� ȣ���� 
    }

    public void Die()
    {
        Destroy(this);
    }
    
    /// <summary>
    /// ���� ��ũ �� ���� �Լ�
    /// </summary>
    public void Pattern_DarkHeal()
    {

    }
    /// <summary>
    /// ���� ���� ��Ʈ����ũ ���� �Լ�
    /// </summary>
    public void Pattern_RuinStk()
    {

    }
    /// <summary>
    /// ���� ���̷��� ��ȯ ���� �Լ�
    /// </summary>
    public void Pattern_SummonSkeleton()
    {

    }
    /// <summary>
    /// ���� ���ε� ���� �Լ�
    /// </summary>
    public void Pattern_Bind()
    {

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

