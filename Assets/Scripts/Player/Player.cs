using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : LivingEntity
{
    /* ���� */

    #region �÷��̾� �̹��� ����
    [SerializeField] Sprite[] attackSprites;
    #endregion

    #region �÷��̾� ��Ƽ���� ����
    [SerializeField] Material playerHitEffectMaterial;     // �ǰ� �� ��Ƽ����
    [SerializeField] Material orignalPlayerMaterial;       // �÷��̾� ���� ��Ƽ����
    private SpriteRenderer spriteRenderer;                 // SpriteRenderer ������Ʈ
    private WaitForSecondsRealtime seconds = new WaitForSecondsRealtime(0.25f);  // ��Ƽ���� ���� ������
    private WaitForSecondsRealtime spriteSeconds = new WaitForSecondsRealtime(1f); // ��������Ʈ ���� ������
    #endregion

    #region �÷��̾� ��Ʈ�ѷ� ����
    private PlayerController_ playerController;
    public PlayerController_ PlayerController { get { return playerController; } }
    #endregion

    #region �÷��̾� ��ȭ ����
    private int playerGold = 0;
    public int PlayerGold 
    { 
        get { return playerGold; }
        set 
        { 
            playerGold = value; 
            Managers.UI.UpdateGoldText();
        } 
    }
    #endregion

    #region �÷��̾� ��ų �̺�Ʈ ����
    /// <summary>
    /// �ǰ� ���� �� �̺�Ʈ
    /// </summary>
    public event UnityAction HitEvent;
    /// <summary>
    /// ActiveSkill �ۿ�� �̺�Ʈ
    /// </summary>
    public UnityAction OnActiveSkillEvent;
    /// <summary>
    /// ���� ��ü �̺�Ʈ
    /// </summary>
    public UnityAction DisableBuffEvent;
    #endregion

    #region �÷��̾� ��ų Ȱ��ȭ ����
    public Dictionary<int, ActiveSkill> playerActiveSkills = new Dictionary<int, ActiveSkill>();
    public Dictionary<int, PassiveSkill> playerPassiveSkills = new Dictionary<int, PassiveSkill>();
    #endregion

    #region �������� ����ϴ� ��ų���� ����
    public Dictionary<string, PlayerSkill> skillList = new Dictionary<string, PlayerSkill>();
    
    // �÷��̾� ��Ƽ�� ��ų ����
    private int activeSkillSlot_Index = 0;
    public int ActiveSkillSlot_Index
    {
        get { return activeSkillSlot_Index;}
        set { activeSkillSlot_Index = value; }
    }
    #endregion

    /* �Լ� */

    #region �÷��̾� �ʱ� ���� 
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayerInit();
        #region ��ų �׽�Ʈ �� (����ȣ)
        /* �׽�Ʈ �뵵 */

        /*
        GameObject skillObject3 = Managers.Resource.GetPerfabGameObject("Player_Skill/FlameStrike Skill");
        ActiveSkill fpSkill = Instantiate(skillObject3, this.transform).GetComponent<ActiveSkill>();
        fpSkill.Init(this);
        playerActiveSkills.Add(0, fpSkill);

        GameObject skillObject2 = Managers.Resource.GetPerfabGameObject("Player_Skill/ThrowingKnife Skill");
        ActiveSkill tkSkill = Instantiate(skillObject2, this.transform).GetComponent<ActiveSkill>();
        tkSkill.Init(this);
        playerActiveSkills.Add(1, tkSkill);

        GameObject skillObject1 = Managers.Resource.GetPerfabGameObject("Player_Skill/ThunderSlash Skill");
        ActiveSkill tdSkill = Instantiate(skillObject1, this.transform).GetComponent<ActiveSkill>();
        tdSkill.Init(this);
        playerActiveSkills.Add(2, tdSkill);

        GameObject barrierSkill = Managers.Resource.GetPerfabGameObject("Player_Skill/Barrier Skill");
        ActiveSkill skill = Instantiate(barrierSkill, this.transform).GetComponent<ActiveSkill>();
        skill.Init(this);
        playerActiveSkills.Add(3, skill);

        GameObject windSlashSkill = Managers.Resource.GetPerfabGameObject("Player_Skill/WindDash Skill");
        ActiveSkill wdskill = Instantiate(windSlashSkill, this.transform).GetComponent<ActiveSkill>();
        wdskill.Init(this);
        playerActiveSkills.Add(4, wdskill);

        GameObject skillObject0 = Managers.Resource.GetPerfabGameObject("Player_Skill/HourGlass Skill");
        PassiveSkill ttSkill = Instantiate(skillObject0, this.transform).GetComponent<PassiveSkill>();
        ttSkill.Init(this);
        ttSkill.OnActive();

        GameObject cowardSkill = Managers.Resource.GetPerfabGameObject("Player_Skill/Coward Skill");
        PassiveSkill mvSkill = Instantiate(cowardSkill, this.transform).GetComponent<PassiveSkill>();
        mvSkill.Init(this);
        mvSkill.OnActive();

        GameObject firstAidSkill = Managers.Resource.GetPerfabGameObject("Player_Skill/FirstAid Skill");
        PassiveSkill hellSkill = Instantiate(firstAidSkill, this.transform).GetComponent<PassiveSkill>();
        hellSkill.Init(this);
        hellSkill.OnActive();

        GameObject spellBladeSkill = Managers.Resource.GetPerfabGameObject("Player_Skill/SpellBlade Skill");
        PassiveSkill damageSkill = Instantiate(spellBladeSkill, this.transform).GetComponent<PassiveSkill>();
        damageSkill.Init(this);
        damageSkill.OnActive();

        GameObject droneSkill = Managers.Resource.GetPerfabGameObject("Player_Skill/Drone Skill");
        PassiveSkill drSkill = Instantiate(droneSkill, this.transform).GetComponent<PassiveSkill>();
        drSkill.Init(this);
        drSkill.OnActive();
        

        */

        #endregion
    }

    /// <summary>
    /// PlayerController �� Player �ʱ� ���� �ʱ�ȭ 
    /// </summary>
    private void PlayerInit()
    {
        BasicStatInit();
        playerController = GetComponent<PlayerController_>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
        playerController.PlayerControllerInit(this);
    }
    #endregion

    #region �÷��̾� �ǰ� ��
    public override sealed void TakeDamage(int newDamage)
    {
        base.TakeDamage(newDamage);

      //  print("�÷��̾ ������ ���� " + newDamage);

        HitEvent?.Invoke(); // �ǰ� �� ���õ� �нú� ����� ȣ����
        StartCoroutine(SwitchMaterial()); // �ǰ� �� �÷��̾� ���� ���� �ڷ�ƾ


        GameObject floatingText = MemoryPoolManager.GetInstance().OutputGameObject
            (Managers.Resource.GetPerfabGameObject("UI/DamageText")
            ,"UI/DamageText"
            ,new Vector3(transform.position.x, transform.position.y)
            ,Quaternion.identity);

        floatingText.GetComponent<FloatingText>().DamageText = newDamage.ToString();
        floatingText.SetActive(true);


        Managers.UI.UpdatePlayerHpSlider(Hp, MaxHp);
        // TODO : �ǰ� ȿ���� ���
    }

    /// <summary>
    /// �÷��̾� ���� ���� �ڷ�ƾ
    /// </summary>
    private IEnumerator SwitchMaterial()
    {
        spriteRenderer.material = playerHitEffectMaterial;
        yield return seconds;
        spriteRenderer.material = orignalPlayerMaterial;
    }
    #endregion

    #region �÷��̾� ��� ó��
    protected override void OnDead()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
    #endregion

    #region �÷��̾� �̹��� ������ ���� �Լ�

    /// <summary>
    /// ���� ��ų�̿�� �̹����� ����
    /// </summary>
    /// <param name="dir">�÷��̾� ����</param>
    public void SwitchPlayerSprite(Vector2 dir, bool needDelay)
    {
        playerController.Anim.enabled = false;
        StartCoroutine(SwitchSprite(dir.normalized, needDelay));
    }

    private IEnumerator SwitchSprite(Vector2 dir, bool isDelay)
    {
        /*
         *  �� = 1, 0
         *  �� = -1, 0
         *  �� = 0, -1
         *  �� = 0, 1
         */

        if (dir.y > 0) spriteRenderer.sprite = attackSprites[0];
        else if (dir.y < 0) spriteRenderer.sprite = attackSprites[1];
        else spriteRenderer.sprite = attackSprites[2];
        /*
        else if (dir.x > 0) spriteRenderer.sprite = attackSprites[2];
        else if (dir.x < 0)
        {
          //  spriteRenderer.flipX = true;
            spriteRenderer.sprite = attackSprites[2];
        }
       */

        if (isDelay)
            yield return spriteSeconds;
        else
            yield return seconds;

        playerController.Anim.enabled = true;
    }
    #endregion


}
