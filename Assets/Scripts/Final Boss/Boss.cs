using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class Boss : Enemy
{
    #region ���� ��� ���
    readonly float BOSS_PROJECTILE_SKULL_SPEED = 10f;       //�⺻���� ����ü �ӵ�
    readonly float BOSS_PATTERN_DARK_HEAL_COUNT = 30f;      //��ũ�� ������ ���� ��Ÿ�� ���� ����
    readonly int BOSS_TEMP_HP = 300;                        //��ũ�� ���� �� ���Ǵ� �ӽ�ü��.
    #endregion             

    #region ���� ���� ��� �ø�������� �ʵ� 
    [SerializeField] [Range(0f, 50f)] float contactDistance;    //������ �����Ÿ� �ʱⰪ 10
    [SerializeField] GameObject finalBossSkull;             //�⺻���� ������
    [SerializeField] GameObject finalBossRuinStk;           //���ν�Ʈ����ũ ���� ������
    [SerializeField] GameObject finalBossDarkHeal;          //��ũ�� ���� ��¡ ������
    [SerializeField] GameObject finalBossDarkHealFailed;    //��ũ�� ���� ���� ������
    [SerializeField] GameObject finalBossBindEye;           //���ε� ���� �� �÷��̾� ���� ������
    [SerializeField] GameObject finalBossBindVineFail;      //���ε� ���� ���� ������ 
    [SerializeField] GameObject finalBossBindVineSucess;    //���ε� ���� ���� ������
    [SerializeField] GameObject[] keyListObject;            //���ε� ���� Ű ��� ��� ������
    [SerializeField] Material originalMaterial;
    [SerializeField] Material hurtMaterial;
    [SerializeField] GameObject player;         //�÷��̾� ������ 
    #endregion

    #region Destroyó���� ����� GameObject ����
    GameObject darkHealA;
    GameObject darkHealB;
    GameObject bindEye;
    GameObject bindVineSucess;
    GameObject bindVineFail;
    #endregion

    #region Pattern1_DefaultAttack_Flying Skull ���� ��������



    #endregion

    #region Pattern2_Dark Heal ���� ��������
    private float darkHealCheckTimer = 0.0f;    //��ũ�� �ӽ�ü�� �ı��ð��� �����ϴ� ���� 
    private int darkHealTempHp;                 //��ũ�� �ӽ�ü�� ������� �����ϴ� ����
    #endregion

    #region Pattern3_Ruin Strike ���� ��������
    private int ruinStrikeQty;                  //���ν�Ʈ����ũ ���� ���� ����
    #endregion

    #region Pattern4_Summon Skeleton ���� ��������

    #endregion

    #region Pattern5_Bind ���� ��������
    GameObject[] iconDestroy = new GameObject[6];           //Ű���� �������� �ѹ��� �����ϱ� ���� �������� 
    GameObject[] qteGameObjectArray = new GameObject[6];    //Ű���� ������ ������Ʈ�� ��� ���� ����
    char[] qteCharArray = new char[6];                      //Object�迭 ������ Char�������� ��ȯ�ϱ� ���� ����
    List<char> inputList = new List<char>();                //�÷��̾��� �Է� ���� �����ϱ� ���� ����
    private float x = -6f;                                  //Ű���� ������ ���� X��ǥ ���� �� ����. ��������
    private bool qteCheck = true;                          //QTE�� �÷��̾ �����Ѵٸ� true, ���и� false
    #endregion

    #region ��Ÿ ��������
    BossFSM bossFSM;
    Rigidbody2D bossRigidBody;
    Animator bossAnimator;
    SpriteRenderer bossSpriteRenderer;
    Transform targetIsPlayer;                   //�÷��̾��� ��ġ���� ����
    private float scaleX;
    private Vector2 dir;
    private float bossHpPercentage;             //������ ü���� �ۼ�Ƽ���� ��Ÿ�� �����ϴ� ����
    private float patternCheckTimer = 0.0f;     //�����ð��� ������ Ư�� ������ �����Ű�� ���� �� ���
    private bool patternCheck = false;          // == isgod. true�̸� ������. 
    #endregion

    /// <summary>
    /// Ŭ������ ���� ��Ű�� �� �ʿ��� ������ ��� ������
    /// </summary>
    void Start()
    {
        base.Start();
        bossFSM = new BossFSM(this);
        scaleX = transform.localScale.x;
        targetIsPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bossAnimator = GetComponent<Animator>();
        bossRigidBody = GetComponent<Rigidbody2D>();
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
        darkHealTempHp = BOSS_TEMP_HP;
        //bossFSM.bossState = Define.BossState.CASTING_STATE; //����ȿ�� ���
    }

    void Update()
    {
        SwitchSpriteImageDir(transform);
        if (bossFSM != null) bossFSM.Update();                      //������ STATE���� ���� ���� ������. 
        if (bossFSM.bossState != Define.BossState.CASTING_STATE)    //������ ���� ���� ���� ����üũŸ�̸Ӹ� �������� ����
        {
            patternCheckTimer += Time.deltaTime;
        }
        if (patternCheckTimer > BOSS_PATTERN_DARK_HEAL_COUNT)       //n�ʸ��� ��ũ�� ���� ����
        {
            bossFSM.bossState = Define.BossState.PATTERN_DARKHEAL_STATE;
            patternCheckTimer = 0;
        }
    }

    #region ���� �⺻ ������Ʈ, ���� ���� �Լ���
    /// <summary>
    /// ������ ���� �����Ÿ��� ���� �����ϰų� �̵���Ŵ
    /// </summary>
    public void Move()
    {
        //���� ������ �÷��̾��� �Ÿ����̰� �̸������� �����Ÿ����� ũ�ٸ� �÷��̾ ����, �ƴ϶�� ���� STATE�� ����
        if (Vector2.Distance(transform.position, targetIsPlayer.position) > contactDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetIsPlayer.position, MoveSpeed * Time.deltaTime);
            dir = (player.transform.position - transform.position).normalized;
        }
        else
        {
            bossFSM.bossState = Define.BossState.ATTACK_STATE;
        }
    }
    /// <summary>
    /// ������ ���ݻ����Ÿ��� ���� �÷��̾ �����ϰų� MOVE_STATE�� �����Ŵ
    /// </summary>
    public void Attack()
    {
        if (Vector2.Distance(transform.position, targetIsPlayer.position) > contactDistance)
        {
            bossFSM.bossState = Define.BossState.MOVE_STATE;
        }
        else if (Vector2.Distance(transform.position, targetIsPlayer.position) < contactDistance)
        {
            SetAnimationTrigger("DefaultAttack");
            bossRigidBody.velocity = Vector2.zero;
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
                                                                                Define.PrefabType.Final_Boss_Skill,
                                                                                transform.position,
                                                                                Quaternion.identity);
        SwitchSpriteImageDir(projectile.transform);
        projectile.GetComponent<Projectile>().ProjectileInit(Define.StringTag.Player, dir, DefaultAttackDamage, BOSS_PROJECTILE_SKULL_SPEED);
        projectile.SetActive(true);
    }
    /// <summary>
    /// �÷��̾�� �������� ���� �� ó���Ǵ� �Լ�
    /// </summary>
    public override sealed void TakeDamage(int newDamage)
    {
        if (patternCheck)              //���� ���� ���̶� ���� ó���� �ؾ��� ��
        {
            return;
        }
        if (bossFSM.runDarkHeal)       //��ũ�� ���� �������� ���� �÷��̾ ���ϴ� �������� �ӽ�ü�� �������� ���� �ϵ���.
        {
            darkHealTempHp -= newDamage;
            Hurt();
            return;
        }
        Hurt();
        base.TakeDamage(newDamage);
        bossHpPercentage = (bossHpPercentage / (float)MaxHp * 100);     //�÷��̾�� �������� ���� ������ �ۼ�Ƽ���� ����ؼ� ������
        RunPattern(bossHpPercentage);  //���� ���Ե� ������ ü���� ������ ���� ���Ѿ� �Ǵ� ü���̶�� �׿� �°� ������ ����.
        if(bossHpPercentage == 0f) 
        {
            OnDead();
        }
    }
    /// <summary>
    /// �ǰ�ȿ�� ��� �Լ� 
    /// </summary>
    public void Hurt()
    {
        StartCoroutine(SwitchMaterial());
    }
    /// <summary>
    /// �ǰ�ȿ���� �����ð� ������Ŵ. (��������Ʈ�� ������� ����)
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
    }
    public void Die()
    {
        Destroy(this, 1.4f);
    }
    /// <summary>
    /// ������ ü�¿� ���� �ش��ϴ� ����� �����Ű�� �Լ�
    /// </summary>
    /// <param name="hpPercentage"></param>
    public void RunPattern(float hpPercentage)
    {
        if ((hpPercentage <= 70f) && (hpPercentage > 15f))
        {
            bossFSM.bossState = Define.BossState.PATTERN_BIND_STATE;
            //SetAnimationTrigger("RunBindMotion");
        }
        else if (((hpPercentage <= 50f) && (hpPercentage > 30f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_SUMNSKELETON_STATE;
            //SetAnimationTrigger("RunSumnSkeleton"); 
        }
        else if (((hpPercentage <= 40f) && (hpPercentage > 20f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_RUINSTK_STATE;
            ruinStrikeQty = 6;
            //SetAnimationTrigger("RunRuinStk");
        }
        else if (((hpPercentage <= 20f) && (hpPercentage > 5f)))
        {
            bossFSM.bossState = Define.BossState.PATTERN_RUINSTK_STATE;
            ruinStrikeQty = 9;
            //SetAnimationTrigger("RunRuinStk");
        }
        else if ((hpPercentage <= 15f) && (hpPercentage > 0.1f))
        {
            bossFSM.bossState = Define.BossState.PATTERN_BIND_STATE;
            //SetAnimationTrigger("RunBindMotion");
        }
        else if (((hpPercentage <= 5f) && (hpPercentage > 0.1f)))
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
    /// ���� �ִ� ü���� 15%�� ȸ���� 
    /// </summary>
    public void Pattern_DarkHeal()
    {
        bossRigidBody.velocity = Vector2.zero;              //���� ��ǥ ����
        SetAnimationTrigger("RunDarkHealMotion");           //�ִϸ��̼� Ʈ���� ����
        bossFSM.bossState = Define.BossState.CASTING_STATE; //������Ʈ ����
        RunDarkHeal();                                      //�����Լ� ����
    }
    public void RunDarkHeal(){  StartCoroutine(DarkHealProcess());  }
    IEnumerator DarkHealProcess()
    {
        darkHealA = CreateSimpleAnimation(finalBossDarkHeal, this.gameObject, 0, 3);
        while (true)
        {
            if (darkHealCheckTimer >= 7.0f && darkHealTempHp > 0)          //�ð� ī���Ͱ� 8�ʰ� ������ 1,000 ������ ���Ϸ� �޾��� ��
            {
                Hp += MaxHp * 15 / 100;     //��ü���� 15%��ŭ ����
                break;
            }
            else if (darkHealCheckTimer < 7.0f && darkHealTempHp <= 0)     //�ð� ī���Ͱ� 8�ʰ� ������ �ʰ� 1,000������ �̻����� �޾��� ��
            {
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
        bossRigidBody.velocity = Vector2.zero;
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
                                                                                Define.PrefabType.Final_Boss_Skill,
                                                                                new Vector2(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f)),
                                                                                Quaternion.identity);
        projectile.GetComponent<Projectile>().ProjectileInit(Define.StringTag.Player, Vector2.zero, 400);
        projectile.SetActive(true);
    }
    #endregion

    #region ��Ŀ���̷��� ��ȯ���� �Լ���
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
    #endregion

    #region ���ε� ���� ���� �Լ���
    /// <summary>
    /// ���� ���ε� ���� �Լ�
    /// ü�� 70%, 15%�� �Ǿ��� �� �����. 
    /// 4�� ���� 6���� Ŀ�ǵ带 �ľ� ��. 
    /// Quick Time Event�� ���ÿ� ����Ǹ� �̸� �����ϸ� 5�� �ӹ�. 
    /// </summary>
    public void Pattern_Bind()
    {
        bossRigidBody.velocity = Vector2.zero;
        //�÷��̾� ħ�� ���� �Ǿߵ�. managers.stagemanager.etc
        SetAnimationTrigger("RunBindMotion");
        SetBossGodMode();                       //������ ���� ���·� ���� 
        bossFSM.bossState = Define.BossState.CASTING_STATE;
        RunBind();
    }
    public void RunBind()
    {
        StartCoroutine(BindProcess());
    }
    IEnumerator BindProcess()
    {
        bindEye = CreateSimpleAnimation(finalBossBindEye, player, 0, 3);        //������ ������ ����
        Destroy(bindEye, 2.0f);                         //2�ʵ��� �����ϰ� �ٷ� �ı�
        for (int i = 0; i < keyListObject.Length; i++) 
        {
            qteGameObjectArray[i] = keyListObject[Random.Range(0, 6)];      //Ű���� ������ ������ ���� ����
            iconDestroy[i] = CreateSimpleAnimation(qteGameObjectArray[i], player, x, 6);    //���� ����� �������� ȭ�鿡 �����ϰ� ���ÿ� ������ ������ ����
            x += 2.5f;      //�� �� ���� �� x��ǥ�� 2.5f��ŭ ����. (���������� �Űܰ���)
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
            yield return new WaitForSeconds(6.0f);  //6�ʵ��� �÷��̾��� Ű���� �Է��� ��ٸ�. 
        if (!inputList.Equals(null))        //�÷��̾ �ƹ��͵� �Է����� �ʾҴٸ� ����
        {
            qteCheck = false;
        }
        else { 
            for (int num = 0; num < 6; num++)   //Ű���� ������ ������ ������ŭ �ݺ�
            {
                if (qteCharArray[num] != inputList[num])    //���� ���� ������ �迭�� �Է¹��� ���� �ٸ��ٸ� ����
                {
                    qteCheck = false;
                    break;
                }
                else if (inputList[num].Equals(null))       //�˻� ���� �Է��� ���� �ʾҴٸ� ���� 
                {
                    qteCheck = false;
                }
                qteCheck = true;                            //�� ���ǵ��� ����ϸ�, ����! 
            }
        }
        if (qteCheck == false)          //6�ʰ� ������ qte�� �����ߴٸ� �ӹ��� ��ӵ�
        {
            bindVineFail = CreateSimpleAnimation(finalBossBindVineFail, player, 0, 1f);
            //managers.statemanager.etc  �÷��̾� 5�� ����      
        }
        else if (qteCheck == true)     //6�ʰ� ������ qte�� ���������� ���� �ִϸ��̼��� ����ϰ� �ӹ��� Ǯ����.
        {
            bindVineSucess = CreateSimpleAnimation(finalBossBindVineSucess, player, 0, 1f); 
            //managers.stagemanager.etc �� Ȱ��ȭ ������ 
        }
        for (int k = 0; k < keyListObject.Length; k++)  //������ Ű���� ������ ������ ���� 
        {
            Destroy(iconDestroy[k]);
        }
        if (bindVineSucess != null) Destroy(bindVineSucess, 1.0f);      //�����ߴ� ������ �ı�
        if (bindVineFail != null) Destroy(bindVineFail, 5.0f);          //�����ߴ� ������ �ı�
        SetBossGodMode();                               //������ ������ ������
        PatternReset();                                 //���Ͽ� ����� �������� �ʱ�ȭ
    }
    #endregion

    #region ��Ÿ �Լ���
    /// <summary>
    /// �Ű������� �޴� ��Ʈ���� ���� ���� �̸� ������ �ִϸ��̼��� �����Ű�� �Լ�
    /// </summary>
    /// <param name="trigger"></param>
    public void SetAnimationTrigger(string trigger)
    {
        bossAnimator.SetTrigger(trigger);
        //EnemyAnimator.SetTrigger(trigger);
    }
    /// <summary>
    /// ���� ���� �� ������ ������ ����� ���� ���� �Լ� true == ���� 
    /// �׻� �����Ǿ��ִ� ���� �ݴ�� �������. default�� false�� ���¿��� ����.
    /// </summary>
    private void SetBossGodMode(){ patternCheck = patternCheck != true; }//true�� ������ false��, false��� true�� �ٲ��ش�.
    /// <summary>
    /// ���� �������� ������ ������Ʈ�� ����üũ Ÿ�̸Ӹ� �ʱ�ȭ �����ִ� �Լ�
    /// </summary>
    public void PatternReset()
    {
        darkHealCheckTimer = 0;
        qteCheck = true;
        bossFSM.bossState = Define.BossState.MOVE_STATE;
    }
    /// <summary>
    /// �ܼ��� �����Ű�⸸ �ϸ� �Ǵ� �������� ������ �� ����ϴ� �Լ�.
    /// �ݶ��̴� ���� �ʿ��� �������� ��� �Ұ�. 
    /// </summary>
    /// <param name="runAnim">  �����ų ������ �ִϸ��̼� </param>        
    /// <param name="target"> ���� ������ �� ������Ʈ. ���� ���� this.gameObject�� ����ϸ� ��. </param>          
    /// <param name="distanceX"> �ִϸ��̼��� ����� x��ǥ </param>      
    /// <param name="distanceY"> �ִϸ��̼��� ����� y��ǥ </param>      
    /// <returns></returns>
    private GameObject CreateSimpleAnimation(GameObject runAnim, GameObject target, float distanceX, float distanceY)
    {
        GameObject projectile = Instantiate(runAnim, new Vector2(target.transform.position.x + distanceX, 
                                                                 target.transform.position.y + distanceY), Quaternion.identity);
        projectile.SetActive(true);
        return projectile;      //������ ������Ʈ ���� ��������. 
    }
    /// <summary>
    ///  ����, ����ü�� ��������Ʈ �̹����� �÷��̾ �ִ� �������� �˸°� �ٲ��ִ� �Լ�
    /// </summary>
    /// <param name="target"></param>  
    private void SwitchSpriteImageDir(Transform target)
    {
        if (dir.x > 0) {target.localScale = new Vector2(scaleX, transform.localScale.y);}
        else{target.transform.localScale = new Vector2(-scaleX, transform.localScale.y);}
        //EnemyRigidbody.transform.localScale = new Vector2(scaleX, transform.localScale.y);
        //EnemyRigidbody.transform.localScale = new Vector2(-scaleX, transform.localScale.y);
    }
    #endregion

    #region �÷��̾� ��ǲ �׼� �Լ� ����
    void OnNodeA()  {inputList.Add('A');}
    void OnNodeS()  {inputList.Add('S');}
    void OnNodeD()  {inputList.Add('D');}
    void OnNodeZ()  {inputList.Add('Z');}
    void OnNodeX()  {inputList.Add('X');}
    void OnNodeC()  {inputList.Add('C');}
    #endregion
}

