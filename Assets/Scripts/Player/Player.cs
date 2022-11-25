using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : LivingEntity
{

    [SerializeField] GameObject text;
    /* ���� */

    #region �÷��̾� ��Ƽ���� ����
    [SerializeField] Material playerHitEffectMaterial;     // �ǰ� �� ��Ƽ����
    [SerializeField] Material orignalPlayerMaterial;       // �÷��̾� ���� ��Ƽ����
    private SpriteRenderer spriteRenderer;                 // SpriteRenderer ������Ʈ
    private WaitForSecondsRealtime seconds = new WaitForSecondsRealtime(0.5f);  // ��Ƽ���� ���� ������
    #endregion

    #region �÷��̾� ��Ʈ�ѷ� ����
    private PlayerController_ playerController;
    public PlayerController_ PlayerController { get { return playerController; } }
    #endregion

    #region �÷��̾� ��ȭ ����
    private int playerGold = 0;
    public int PlayerGold { get { return playerGold; } set { playerGold = value; } }
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
    public Dictionary<int, PlayerSkill> skillList = new Dictionary<int, PlayerSkill>();
    #endregion

    /* �Լ� */

    #region �÷��̾� �ʱ� ���� 
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayerInit();
        print("�÷��̾� �⺻ ���ݷ� : " + DefaultAttackDamage);
        #region ��ų �׽�Ʈ �� (����ȣ)
        /* �׽�Ʈ �뵵 */
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

        GameObject skillObject0 = Managers.Resource.GetPerfabGameObject("Player_Skill/HourGlass Skill");
        PassiveSkill ttSkill = Instantiate(skillObject0, this.transform).GetComponent<PassiveSkill>();
        ttSkill.Init(this);
        playerPassiveSkills.Add(0, ttSkill);

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
        TakeDamage(12);
    }
    #endregion

    #region �÷��̾� �ǰ� ��
    public override sealed void TakeDamage(int newDamage)
    {
        base.TakeDamage(newDamage);

        HitEvent?.Invoke(); // �ǰ� �� ���õ� �нú� ����� ȣ����
        StartCoroutine(SwitchMaterial()); // �ǰ� �� �÷��̾� ���� ���� �ڷ�ƾ


        GameObject floatingText = MemoryPoolManager.GetInstance().OutputGameObject(text
            ,Define.PrefabType.UI
            ,new Vector3(transform.position.x, transform.position.y)
            ,Quaternion.identity);

        floatingText.SetActive(true);
                                                        
        // TODO : Player UI ü�� ������ ����
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

    }
    #endregion


}
