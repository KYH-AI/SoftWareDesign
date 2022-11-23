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
    [SerializeField] GameObject bossProjectileRuinStk;
    readonly float BOSS_DEFAULT_ATTACK_SPEED = 1.5f;
    readonly float BOSS_PROJECTILE_SKULL_SPEED = 10f;
    readonly float BOSS_PATTERN_DARK_HEAL_COUNT = 90f;     

    private float bossHpPercentage;
    private int ruinStrikeQty;
    Rigidbody2D boss;
    Animator boss_ani;
    private bool patternCheck = false;      // == isgod. ture�̸� ������. 
    private float patternCheckTimer=0.0f;     //����ð� 
    private float darkHealCheckTimer = 0.0f;
    private int darkHealTempHp = 1000;
    #endregion

    #region �÷��̾�&���� ���� ���� ����
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
            Debug.Log(patternCheckTimer+"��");
            bossFSM.bossState = Define.BossState.PATTERN_DARKHEAL_STATE;
            patternCheckTimer = 0;

        }
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
        if(patternCheck)
        {
            return;
        }
        if(bossFSM.runDarkHeal)       //CASTING ���� �� �� 
        {
            darkHealTempHp -= newDamage;
        }
        StartCoroutine(SwitchMaterial());
        base.TakeDamage(newDamage);
        bossHpPercentage = (bossHpPercentage / (float)MaxHp * 100);
        RunPattern(bossHpPercentage);
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
    /// ������ ü�¿� ���� �ش��ϴ� ����� �����Ű�� �Լ�
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
    /// ���� ��ũ �� ���� �Լ�
    /// 1�� 30�ʸ��� �ݺ��ǰ� 8�ʵ��� �������� ����
    /// ���� �������� 1,000 �̻� �ް� �ȴٸ� ������ �ִ� �������� �����(���� �÷��̾� �ٽ� ������)
    /// ȸ������ ���� �ִ� ü���� 15%�� ȸ���� 
    /// </summary>
    public void Pattern_DarkHeal()
    {
        print("DarkHeal������");
        boss.velocity = Vector2.zero;
        SetPatternCheck();
        SetAnimationTrigger("RunDarkHealMotion");
        bossFSM.bossState = Define.BossState.CASTING_STATE;
        darkHealCheckTimer += Time.deltaTime;
        if(darkHealCheckTimer > 8.0f && darkHealTempHp < 1000)          //�ð� ī���Ͱ� 8�ʰ� ������ 1,000 ������ ���Ϸ� �޾��� ��
        {
            darkHealCheckTimer = 0;
            Hp += MaxHp * 15 / 100;     //��ü���� 15%��ŭ ����
            bossFSM.bossState = Define.BossState.MOVE_STATE;
            darkHealTempHp = 1000;
        }
        else if(darkHealCheckTimer < 8.0f && darkHealTempHp > 1000)     //�ð� ī���Ͱ� 8�ʰ� ������ �ʰ� 1,000������ �̻����� �޾��� ��
        {
            darkHealCheckTimer = 0;
            SetAnimationTrigger("");
        }
    }
    /// <summary>
    /// ���� ���� ��Ʈ����ũ ���� �Լ�
    /// ������ ü���� 40%, 25%, 5%�� �Ǿ��� ������ ���� 
    /// ������ ���ǿ� ���� 4��,6,��,9���� ����ü�� ������ ��ġ�� ������
    /// </summary>
    public void Pattern_RuinStk()
    {
        print("���� ����");
        boss.velocity = Vector2.zero;
        SetPatternCheck();
        RunRuinStk();
        bossFSM.bossState = Define.BossState.CASTING_STATE;
    }
    /// <summary>
    /// ���ν�Ʈ����ũ �ڷ�ƾ�� �����Ű�� �Լ�
    /// </summary>
    private void RunRuinStk()
    {
        StartCoroutine(RuinStrikeProcess(ruinStrikeQty));
    }
    IEnumerator RuinStrikeProcess(int qty)
    {
        print("���� ��Ʈ����ũ �ڷ�ƾ");
        for (int i = 0; i < qty; i++)
        {
            SetAnimationTrigger("RunRuinStk");
            print(i + "�� �߻�");
            yield return new WaitForSeconds(0.8f); 
        }
        bossFSM.bossState = Define.BossState.MOVE_STATE;
    }

    // ����ü�� �ݺ������� ������ ���� �Լ��� ���� ����
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

    #region �ִϸ��̼� ���� �Լ�
    /// <summary>
    /// �Ű������� �޴� ��Ʈ���� ���� ���� �̸� ������ �ִϸ��̼��� �����Ű�� �Լ�
    /// </summary>
    /// <param name="trigger"></param>
    public void SetAnimationTrigger(string trigger)
    {
        boss_ani.SetTrigger(trigger);
        //EnemyAnimator.SetTrigger(trigger);
    }
    /// <summary>
    /// ���� ���� �� ������ ������ ����� ���� ���� �Լ�
    /// </summary>
    private void SetPatternCheck()
    {
        patternCheck = patternCheck != true;
    }
  
    #endregion






}

