using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : Enemy
{
    protected int playerLayer = 1 << 10;
    public enum BossState
    {
        IDLE_STATE,
        MOVE_STATE,
        ATTACK_STATE,
        HURT_STATE,
        DEAD_STATE
    };
    float distance;
    float moveDistance = 10f;
    float attackDistance = 3f;
    float moveSpeed = 4f;
    float attackDelay = 2f;
    Vector2 dir;
    BossState state = BossState.IDLE_STATE;
    bool isAttack = true;
    bool onTrigger = true;
    bool isHurt = false;


    // Update is called once per frame
    void Update()
    {
        dir = (playerTarget.transform.position - transform.position);
        ChangeDir();
        distance = dir.magnitude;  //Vector2.Distance(transform.position, playerTarget.transform.position);
        Fsm();
    }

    private void ChangeDir()
    {
        if (dir.x < 0)
        {
            SpriteRenderer.flipX = true;

        }
        else
        {
            SpriteRenderer.flipX = false;
        }
    }
    void Fsm()
    {
        switch (state)
        {
            case BossState.IDLE_STATE:
                Idle();
                break;
            case BossState.MOVE_STATE: 
                Move();
                 break;
            case BossState.ATTACK_STATE:
                Attack();
                break;
            case BossState.HURT_STATE:
                break;
            case BossState.DEAD_STATE:
                break;
        }
    }
     private void OnTriggerEnter2D(Collider2D collision)
     {
         if (collision.gameObject.tag == "Player"&& onTrigger)
         {
             DefaultAttack();
             print("�������� ��");
         }
        onTrigger = false;
     }

    void Idle()
    {
        if (distance < moveDistance)
        {
            state = BossState.MOVE_STATE;
            //StartCoroutine(IdleTOMove()); 
        }
        EnemyAnimator.SetBool("isRun", false);
    }
    private void Move()
    {
        if (!isHurt)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, moveSpeed * Time.deltaTime);
        }
        
        EnemyAnimator.SetBool("isRun", true);
       if (distance <= attackDistance)
        {
            state = BossState.ATTACK_STATE;
        }
    }
    private void Attack()
    {
        if (isAttack)
        {
            StartCoroutine(AttackDelay());
        }
            
    }
    public override void TakeDamage(int newDamage)
    {
        base.TakeDamage(newDamage);
        Hurt();
    }

  

    protected override void OnDead()
    {
        base.OnDead();
        EnemyCollider.isTrigger = true;
        EnemyAnimator.SetTrigger("isDie");
    }
    void DeadProcess()//Die�ִϸ��̼ǿ� ����
    {
        Destroy(gameObject);
    }


    private void Hurt()
    {
        EnemyAnimator.SetBool("isDamaged", true);
        isHurt = true;
        StartCoroutine(HurtToIdle());
    }
    
    IEnumerator HurtToIdle()
    {
        yield return new WaitForSeconds(1f);
        EnemyAnimator.SetBool("isDamaged", false);
        isHurt = false;
        state = BossState.IDLE_STATE;
    }

    IEnumerator IdleTOMove()
    {
        yield return new WaitForSeconds(1f);
        state = BossState.MOVE_STATE;
    }

    IEnumerator AttackDelay()
    {
        isAttack = false;
        EnemyAnimator.SetBool("isRun", false);
       // yield return new WaitForSeconds(0.1f);//0.2�� ���� idle
        EnemyAnimator.SetBool("isAttack", true);
        //EnemyCollider.isTrigger = true;//�����Ҷ� �˰� ĳ���Ͱ� ��ġ�� ����
        yield return new WaitForSeconds(attackDelay);//���� ������
        onTrigger = true;
        isAttack = true;
    }
    void AttackToIdle()//attack�ִϸ��̼� �������� ����
    {
        EnemyAnimator.SetBool("isAttack", false);
        StartCoroutine(AfterDelay());//���� �� ������
        //EnemyCollider.isTrigger = false;
    }
    IEnumerator AfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (distance > attackDistance)//�־����� idle ���·� ���ư��� �÷��̾� �����ϱ�
        {
            state = BossState.IDLE_STATE;
        }
    }
}