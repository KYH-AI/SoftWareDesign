using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : LivingEntity
{
    /* 변수 */

    #region 플레이어 머티리얼 변수
    [SerializeField] Material playerHitEffectMaterial;     // 피격 시 머티리얼
    [SerializeField] Material orignalPlayerMaterial;       // 플레이어 원본 머티리얼
    private SpriteRenderer spriteRenderer;                 // SpriteRenderer 컴포넌트
    private WaitForSecondsRealtime seconds = new WaitForSecondsRealtime(0.5f);  // 머티리얼 변경 딜레이
    #endregion

    #region 플레이어 컨트롤러 변수
    private PlayerController_ playerController;
    public PlayerController_ PlayerController { get { return playerController; } }
    #endregion

    #region 플레이어 재화 변수
    private int playerGold = 0;
    public int PlayerGold { get { return playerGold; } set { playerGold = value; } }
    #endregion

    #region 플레이어 스킬 이벤트 변수
    /// <summary>
    /// 피격 받을 시 이벤트
    /// </summary>
    public event UnityAction HitEvent;
    /// <summary>
    /// ActiveSkill 작용시 이벤트
    /// </summary>
    public UnityAction OnActiveSkillEvent;
    /// <summary>
    /// 버프 해체 이벤트
    /// </summary>
    public UnityAction DisableBuffEvent;
    #endregion

    #region 플레이어 스킬 정보 변수 선언부
    public Dictionary<int, ActiveSkill> playerActiveSkills = new Dictionary<int, ActiveSkill>();
    public Dictionary<int, PassiveSkill> playerPassiveSkills = new Dictionary<int, PassiveSkill>();
    #endregion


    /* 함수 */

    #region 플레이어 초기 설정 
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayerInit();
        print("플레이어 기본 공격력 : " + DefaultAttackDamage);
        #region 스킬 테스트 중 (김윤호)
        /* 테스트 용도 */
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
    /// PlayerController 랑 Player 초기 스텟 초기화 
    /// </summary>
    private void PlayerInit()
    {
        BasicStatInit();
        playerController = GetComponent<PlayerController_>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
        playerController.PlayerControllerInit(this);
    }
    #endregion

    #region 플레이어 피격 시
    public override sealed void TakeDamage(int newDamage)
    {
        base.TakeDamage(newDamage);

        HitEvent?.Invoke(); // 피격 시 관련된 패시브 기술만 호출함
        StartCoroutine(SwitchMaterial()); // 피격 시 플레이어 색상 변경 코루틴

        // TODO : Player UI 체력 게이지 감소
        // TODO : 피격 효과음 재생
    }

    /// <summary>
    /// 플레이어 색상 변경 코루틴
    /// </summary>
    private IEnumerator SwitchMaterial()
    {
        spriteRenderer.material = playerHitEffectMaterial;
        yield return seconds;
        spriteRenderer.material = orignalPlayerMaterial;
    }
    #endregion

    #region 플레이어 사망 처리
    protected override void OnDead()
    {

    }
    #endregion

    #region 플레이어 기본 공격 히트박스
    
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.CompareTag(Define.StringTag.Enemy.ToString()))
        {
            print(target.name+" 에게 " + DefaultAttackDamage + " 부여 ");
            target.GetComponent<Enemy>().TakeDamage(DefaultAttackDamage);
        }
    }
    #endregion
}
