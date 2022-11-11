using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : MonoBehaviour
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
    Rigidbody2D bossRigidBody;
    Animator bossAttack;

    [SerializeField] [Range(1f, 20f)] float moveSpeed = 5f;
    [SerializeField] [Range(0f, 50f)] float contactDistance = 100f;
    [SerializeField] [Range(10, 5000)] int currentHP = 100; 
    
    public bool follow = false;
    private float scaleX;
  
    // Start is called before the first frame update
    void Start()
    {
        bossFSM = new BossFSM(this);
        scaleX = transform.localScale.x; 
        bossRigidBody = GetComponent<Rigidbody2D>();
        bossAttack = GetComponent<Animator>();


        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        
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
            bossRigidBody.transform.localScale = new Vector2(scaleX, transform.localScale.y);
        }
        else
        {
            bossRigidBody.transform.localScale = new Vector2(-scaleX, transform.localScale.y);
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
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
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
        Debug.Log("Attack");
        bossAttack.SetTrigger("DefaultAttack");
        bossRigidBody.velocity = Vector2.zero;

        if (Vector2.Distance(transform.position, target.position) > contactDistance)
        {
            bossFSM.bossState = BossState.MOVE_STATE; 
        }
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

