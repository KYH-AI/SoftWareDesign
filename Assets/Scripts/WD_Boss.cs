using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WD_Boss : MonoBehaviour
{
    WD_BossFSM bossFSM;

    [SerializeField] GameObject player;
    [SerializeField] BoxCollider2D weaponCollider;   
    [SerializeField] GameObject weapon;

    private Vector2 dir;

    Transform target;
    
    //[SerializeField] AnimationTriggers attack;


    Rigidbody2D bossRigidBody;
    Animator bossAttack;


    [SerializeField] [Range(1f, 20f)] float moveSpeed = 3f;
    [SerializeField] [Range(0f, 50f)] float contactDistance = 1f;

    public bool follow = false;
    private float scaleX;
  
    // Start is called before the first frame update
    void Start()
    {
        bossFSM = new WD_BossFSM(this);

        scaleX = transform.localScale.x; 
        bossRigidBody = GetComponent<Rigidbody2D>();
        weaponCollider = weapon.GetComponent<BoxCollider2D>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bossAttack = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        ChangeDir();
        bossFSM.Update();

    }


    /// <summary>
    /// 플레이어 오브젝트의 위치에 따라서 보스 오브젝트의 스프라이트 좌우 반전을 컨트롤 하는 함수
    /// </summary>
    private void ChangeDir()
    {
        if (dir.x < 0)
        {             
            bossRigidBody.transform.localScale = new Vector2(scaleX, transform.localScale.y);

        }
        else
        {
            bossRigidBody.transform.localScale = new Vector2(-scaleX, transform.localScale.y);
        }
    }
  
    public void Move()
    {
        if (Vector2.Distance(transform.position, target.position) > contactDistance && follow)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            dir = (player.transform.position - transform.position).normalized;
        }
        else
        {
            bossFSM.b_state = WD_BossFSM.BossState.Attack;
          
        }
    }

    bool check = true;
    public void Attack()
    {
        
        bossAttack.SetTrigger("Attack");
        bossRigidBody.velocity = Vector2.zero;

        if (Vector2.Distance(transform.position, target.position) > contactDistance && follow && !check)
        {
            bossFSM.b_state = WD_BossFSM.BossState.Move;
        }
       
    }
   private void ChangeCheck()
    {
        check = false;
    }

    private void StopMove()
    {
        //1.공격 애니메이션 시작되면 이동 불가능
        //이동이 불가능 하면서 공격모션은 진행이 계속 되어야 함 

        //2.칼을 휘두른 애니메이션이 나올때 Weapon 콜라이더를 활성화

        //2.공격 애니메이션 종료되면 이동


    }

    



}

