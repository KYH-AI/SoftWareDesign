using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LivingEntity, IBasicMovement
{
    private PlayerCamera playerCamera;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;

    float x;
    float y;
    
    Vector2 lastDir = Vector2.zero; 
    public Vector2 LastDir { get { return lastDir; } }

    private int enemyLayer = 1 << 11;
    private int wallLayer = 1 << 12;

    [SerializeField] Transform attackDir;
    public Transform AttackDir { get { return attackDir; } }

   
    public Dictionary<int, ActiveSkill> playerActiveSkills = new Dictionary<int, ActiveSkill>();
    public Dictionary<int, PassiveSkill> playerPassiveSkills = new Dictionary<int, PassiveSkill>();
    
    

    private void Start()
    {
       
        lastDir = Vector2.up;
 
       GameObject skillObject = Mangers.Resource.GetPerfabGameObject("Player Skill/PowerSlash Skill");
       ActiveSkill pdSkill = Instantiate(skillObject, this.transform).GetComponent<ActiveSkill>();
       pdSkill.Init(this.gameObject);
        playerActiveSkills.Add(0, pdSkill);
       

         GameObject skillObject2 = Mangers.Resource.GetPerfabGameObject("Player Skill/ThunderClap Skill");
        ActiveSkill tdSkill = Instantiate(skillObject2, this.transform).GetComponent<ActiveSkill>();
        tdSkill.Init(this.gameObject);
        playerActiveSkills.Add(1, tdSkill);
   

        GameObject skillObject3 = Mangers.Resource.GetPerfabGameObject("Player Skill/FirePillar Skill");
        ActiveSkill fpSkill = Instantiate(skillObject3, this.transform).GetComponent<ActiveSkill>();
        fpSkill.Init(this.gameObject);
        playerActiveSkills.Add(2, fpSkill);
     

        GameObject skillObject4 = Mangers.Resource.GetPerfabGameObject("Player Skill/Barrier Skill");
        ActiveSkill brSkill = Instantiate(skillObject4, this.transform).GetComponent<ActiveSkill>();
        brSkill.Init(this.gameObject);
        playerActiveSkills.Add(3, brSkill);
        

        GameObject skillObject5 = Mangers.Resource.GetPerfabGameObject("Player Skill/ThrowingKnife Skill");
        ActiveSkill tkSkill = Instantiate(skillObject5, this.transform).GetComponent<ActiveSkill>();
        tkSkill.Init(this.gameObject);
        playerActiveSkills.Add(4, tkSkill);

        GameObject passvieSkillObject = Mangers.Resource.GetPerfabGameObject("Player Skill/HourGlass Skill");
        PassiveSkill hgSkill = Instantiate(passvieSkillObject, this.transform).GetComponent<PassiveSkill>();
        hgSkill.Init(this.gameObject);
        playerPassiveSkills.Add(0, hgSkill);

        GameObject passvieSkillObject2 = Mangers.Resource.GetPerfabGameObject("Player Skill/Drone Skill");
        PassiveSkill drSkill = Instantiate(passvieSkillObject2, this.transform).GetComponent<PassiveSkill>();
        drSkill.Init(this.gameObject);
        playerPassiveSkills.Add(1, drSkill);
        
        Init();

        playerCamera = GetComponent<PlayerCamera>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        playerPassiveSkills[1].OnActive();
    }



    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        playerAnimator.SetFloat("X", x);
        playerAnimator.SetFloat("Y", y);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerActiveSkills[0].OnActive();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerActiveSkills[1].OnActive();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerActiveSkills[2].OnActive();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerActiveSkills[3].OnActive();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerActiveSkills[4].OnActive();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            playerPassiveSkills[0].OnActive();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        playerRigidbody.velocity = new Vector2(x, y) * MoveSpeed; //* Time.deltaTime;
        //  playerRigidbody.AddForce(new Vector2 (x, y) * MoveSpeed * Time.deltaTime, ForceMode2D.Force);


        if (playerRigidbody.velocity == Vector2.zero)
        {
            playerAnimator.SetBool("isMove", false);
            return;
        }
        else
        {
            playerAnimator.SetBool("isMove", true);
        }

        // X > 0  크면 오른쪽 (East)
        // X < 0 작으면 왼쪽 (West)
        // Y > 0 크면 위쪽 (North)
        // Y < 0 작으면 아래쪽 (South)

        playerAnimator.SetFloat("LastX", x);
        playerAnimator.SetFloat("LastY", y);

        lastDir.x = x;
        lastDir.y = y;

        attackDir.localPosition = lastDir;
    }

    public void DefaultAttack()
    {
       
    }

    protected override sealed void OnDead()
    {
        // TODO : GameOver UI 연출
    }

    public override sealed void TakeDamage(int newDamage)
    {
         base.TakeDamage(newDamage);

        // TODO : Player UI 체력 게이지 감소
    }
}
