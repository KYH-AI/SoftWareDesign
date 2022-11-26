using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;



public class Boss : Enemy
{


    #region ���� ���� ���� ����
    BossFSM bossFSM;            //������ �ൿ�� �����ϴ� BossFSM Ŭ������ �����ϴ� ����
    [SerializeField] [Range(0f, 50f)] float contactDistance;    //������ �����Ÿ� �ʱⰪ 10
    private float scaleX;       //������ scaleX��
    [SerializeField] GameObject finalBossSkull;
    [SerializeField] GameObject finalBossRuinStk;
    [SerializeField] GameObject finalBossDarkHeal;
    [SerializeField] GameObject finalBossDarkHealFailed;
    [SerializeField] GameObject finalBossBindEye;
    [SerializeField] GameObject finalBossBindVineFail;
    [SerializeField] GameObject finalBossBindVineSucess;

    readonly float BOSS_PROJECTILE_SKULL_SPEED = 10f;
    readonly float BOSS_PATTERN_DARK_HEAL_COUNT = 30f;
    readonly int BOSS_TEMP_HP = 300;

    GameObject darkHealA;
    GameObject darkHealB;
    GameObject bindEye;
    GameObject bindVineSucess;
    GameObject bindVineFail;



    [SerializeField] GameObject[] keyListObject;
    GameObject[] iconDestroy = new GameObject[6];
    GameObject[] qteGameObjectArray = new GameObject[6];
    char[] qteCharArray = new char[6];

    List<char> inputList = new List<char>(); 

    private float x = -6f;


    private float bossHpPercentage;
    private int ruinStrikeQty;
    Rigidbody2D boss;
    Animator boss_ani;
    private bool patternCheck = false;      // == isgod. true�̸� ������. 
    private bool qteCheck = true;
    private float patternCheckTimer = 0.0f;     //����ð� 
    private float darkHealCheckTimer = 0.0f;
    private float bindCheckTimer = 0.0f;
    private int darkHealTempHp;
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
        darkHealTempHp = BOSS_TEMP_HP;

        RunBind();

    }

    void Update()
    {

        SwitchSpriteImageDir(transform);
        if (bossFSM != null) bossFSM.Update();
        if (bossFSM.bossState != Define.BossState.CASTING_STATE)
        {
            patternCheckTimer += Time.deltaTime;
        }


        if (patternCheckTimer > BOSS_PATTERN_DARK_HEAL_COUNT)
        {
            Debug.Log(patternCheckTimer + "��");
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

    #region ���� �⺻ ������Ʈ, ���� ���� �Լ���

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
        GameObject projectile = MemoryPoolManager.GetInstance().OutputGameObject(finalBossSkull,
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
        if (patternCheck)
        {
            return;
        }
        if (bossFSM.runDarkHeal)       //CASTING ���� �� �� 
        {
            Debug.Log("�ӽ�ü�� ���� ���ǹ� ����");
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
            ruinStrikeQty = 6;
            //SetAnimationTrigger("RunRuinStk");
        }
        else if (((hpPercentage <= 20f) && (hpPercentage >= 6f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_RUINSTK_STATE;
            ruinStrikeQty = 9;
            //SetAnimationTrigger("RunRuinStk");
        }
        else if ((hpPercentage <= 15f) && (hpPercentage >= 1f))
        {
            bossFSM.bossState = Define.BossState.PATTERN_BIND_STATE;
            //SetAnimationTrigger("RunBindMotion");
        }
        else if (((hpPercentage <= 5f) && (hpPercentage >= 1f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_RUINSTK_STATE;
            ruinStrikeQty = 12;
            //SetAnimationTrigger("RunRuinStk");
        }
        else
        {
            return;
        }
    }
    #endregion


    #region ��ũ �� ���� ���� �Լ���
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
        darkHealA = CreateSimpleAnimation(finalBossDarkHeal, this.gameObject, 0, 3);

        while (true)
        {
            if (darkHealCheckTimer >= 7.0f && darkHealTempHp > 0)          //�ð� ī���Ͱ� 8�ʰ� ������ 1,000 ������ ���Ϸ� �޾��� ��
            {
                Hp += MaxHp * 15 / 100;     //��ü���� 15%��ŭ ����
                print(Hp + "�����Ȱ�");
                break;
            }
            else if (darkHealCheckTimer < 7.0f && darkHealTempHp <= 0)     //�ð� ī���Ͱ� 8�ʰ� ������ �ʰ� 1,000������ �̻����� �޾��� ��
            {
                print("��ȣ�� �ı���");
                darkHealB = CreateSimpleAnimation(finalBossDarkHealFailed, this.gameObject, 0, 3);
                break;
            }
            yield return new WaitForSeconds(1.0f);
            darkHealCheckTimer += 1.0f;
        }

        Destroy(darkHealA);
        if (darkHealB != null) Destroy(darkHealB, 1.0f);
        darkHealTempHp = BOSS_TEMP_HP;
        bossFSM.runDarkHeal = false;
        PatternReset();

    }


    #endregion

    #region ���� ��Ʈ����ũ ���� ���� �Լ���
    /// <summary>
    /// ���� ���� ��Ʈ����ũ ���� �Լ�
    /// ������ ü���� 40%, 25%, 5%�� �Ǿ��� ������ ���� 
    /// ������ ���ǿ� ���� 4��,6,��,9���� ����ü�� ������ ��ġ�� ������
    /// </summary>
    public void Pattern_RuinStk()
    {
        print("���ν�Ʈ����ũ");
        boss.velocity = Vector2.zero;
        SetBossGodMode();
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
        SetBossGodMode();
        PatternReset();
    }

    
    private void CreateBossProjectileRuinStk()
    {
        GameObject projectile = MemoryPoolManager.GetInstance().OutputGameObject(finalBossRuinStk,
                                                                                Define.PrefabType.Boss_Skill,
                                                                                new Vector2(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f)),
                                                                                Quaternion.identity);
        projectile.GetComponent<Projectile>().ProjectileInit(Define.StringTag.Player, Vector2.zero, 400);
        projectile.SetActive(true);
    }

    #endregion

    /// <summary>
    /// ���� ���̷��� ��ȯ ���� �Լ�
    /// ������ ü���� 50%�� �Ǿ��� �� �����Ǹ� ��ü���� ����. 
    /// ���̷����� ���ݷ��� 20, �⺻ ���� ���ݹۿ� ����. ü���� ���� ���� ����. 
    /// </summary>
    public void Pattern_SummonSkeleton()
    {
        SetAnimationTrigger("RunSumnSkeleton");
        PatternReset();
    }

    #region ���ε� ���� ���� �Լ���
    /// <summary>
    /// ���� ���ε� ���� �Լ�
    /// ü�� 70%, 15%�� �Ǿ��� �� �����. 
    /// 4�� ���� 7���� Ŀ�ǵ带 �ľ� ��. 
    /// Quick Time Event�� ���ÿ� ����Ǹ� �̸� �����ϸ� 5�� �ӹ�. 
    /// </summary>
    public void Pattern_Bind()
    {
        print("���ε� ���� ������");
        boss.velocity = Vector2.zero;
        //�÷��̾� ħ�� ���� �Ǿߵ�. managers.stagemanager.etc
        SetAnimationTrigger("RunBindMotion");
        SetBossGodMode();
        bossFSM.bossState = Define.BossState.CASTING_STATE;
        RunBind();
    }
    public void RunBind()
    {
        StartCoroutine(BindProcess());
    }
    IEnumerator BindProcess()
    {
        bindEye = CreateSimpleAnimation(finalBossBindEye, player, 0, 3);
        Destroy(bindEye, 2.0f);
        for (int i = 0; i < keyListObject.Length; i++)
        {
            qteGameObjectArray[i] = keyListObject[Random.Range(0, 6)];
            iconDestroy[i] = CreateSimpleAnimation(qteGameObjectArray[i], player, x, 6);
            x += 2.5f;

            switch (qteGameObjectArray[i].name)         //���ӿ�����Ʈ �迭�� �������� ����� �������� CHAR�� �迭�� �Ľ��ϴ� ����ġ��
            {
                case "BossIconA":
                    qteCharArray[i] = 'A'; break;
                case "BossIconS":
                    qteCharArray[i] = 'S'; break;
                case "BossIconD":
                    qteCharArray[i] = 'D'; break;
                case "BossIconZ":
                    qteCharArray[i] = 'Z'; break;
                case "BossIconX":
                    qteCharArray[i] = 'X'; break;
                case "BossIconC":
                    qteCharArray[i] = 'C'; break;
            }
        }
            yield return new WaitForSeconds(6.0f);
        if (inputList[0].Equals(null)) qteCheck = false;
            for (int num = 0; num < 6; num++)
            {
                if (qteCharArray[num] != inputList[num])
                {
                    qteCheck = false;
                    break;
                }else if(inputList[num].Equals(null))
                {
                    qteCheck = false;
                }
                qteCheck = true;
            }
    
        

        if (qteCheck == false)          //5�ʰ� ������ qte�� �����ϸ� 
        {
            print("�ӹ��� ���۵�");
            bindVineFail = CreateSimpleAnimation(finalBossBindVineFail, player, 0, 1f);
            //managers.statemanager.etc  �÷��̾� 5�� ����
                
        }
        else if (qteCheck == true)     //5�ʰ� ������ �ʾҰ� qte�� ���������� 
        {
            print("�ӹ��� �ı���");
            bindVineSucess = CreateSimpleAnimation(finalBossBindVineSucess, player, 0, 1f); 
            //managers.stagemanager.etc �� Ȱ��ȭ ������ 
               
        }

        for (int k = 0; k < keyListObject.Length; k++)  //������ ������Ʈ ���� 
        {
            Destroy(iconDestroy[k]);
        }
            
        if (bindVineSucess != null) Destroy(bindVineSucess, 1.0f);
        if (bindVineFail != null) Destroy(bindVineFail, 5.0f);
            SetBossGodMode();
            PatternReset();                                 //���ϸ���
            
    }
    #endregion

    #region �ִϸ��̼� ���� �Լ���
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
    private void SetBossGodMode()
    {
        patternCheck = patternCheck != true;
    }
    /// <summary>
    /// ���� �������� ������ ������Ʈ�� ����üũ Ÿ�̸Ӹ� �ʱ�ȭ �����ִ� �Լ�
    /// </summary>
    public void PatternReset()
    {
        darkHealCheckTimer = 0;
        bindCheckTimer = 0;
        qteCheck = true;
        bossFSM.bossState = Define.BossState.MOVE_STATE;
    }
    /// <summary>
    /// �ܼ��� �����Ű�⸸ �ϸ� �Ǵ� �ִϸ��̼��� ������ �� ����ϴ� �Լ�
    /// </summary>
    /// <param name="runAnim"></param>        �����ų ������ �ִϸ��̼�
    /// <param name="target"></param>         ���� ������ �� ������Ʈ. ���� ���� this.gameObject�� ����ϸ� ��. 
    /// <param name="distanceX"></param>      �ִϸ��̼��� ����� x��ǥ
    /// <param name="distanceY"></param>      �ִϸ��̼��� ����� y��ǥ
    /// <returns></returns>
    private GameObject CreateSimpleAnimation(GameObject runAnim, GameObject target, float distanceX, float distanceY)
    {
        GameObject projectile = Instantiate(runAnim,
                                            new Vector2(target.transform.position.x + distanceX, target.transform.position.y + distanceY),
                                            Quaternion.identity);

        projectile.SetActive(true);

        return projectile;
    }

    #endregion

    #region �÷��̾� ��ǲ �׼� �Լ� ����
    void OnNodeA()
    {
        print("A����");
        inputList.Add('A');
    }
    void OnNodeS()
    {
        inputList.Add('S');
    }
    void OnNodeD()
    {
        inputList.Add('D');
    }
    void OnNodeZ()
    {
        inputList.Add('Z');
    }
    void OnNodeX()
    {
        inputList.Add('X');
    }
    void OnNodeC()
    {
        inputList.Add('C');
    }
    #endregion




}

