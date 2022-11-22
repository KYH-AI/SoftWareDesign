using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Boss4 : Enemy
{
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
    Vector2 dir;
    BossState state;
    SpriteRenderer renderer;
    Collider2D collider;
    void Awake()
    {
        EnemyInit(StageManager.GetInstance().player);
        state = BossState.IDLE_STATE;
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        dir = (playerTarget.transform.position - transform.position).normalized;
        ChangeDir();
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));//회전이 안되게 고정
        distance = Vector2.Distance(transform.position, playerTarget.transform.position);
        Fsm();
    }

    private void ChangeDir()
    {
        if (dir.x < 0)
        {
            renderer.flipX = true;

        }
        else
        {
            renderer.flipX = false;
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
    void Idle()
    {
        if (distance < moveDistance)
        {
            StartCoroutine(IdleTOMove()); 
        }
        EnemyAnimator.SetBool("isRun", false);
    }
    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, moveSpeed * Time.deltaTime);
        EnemyAnimator.SetBool("isRun", true);
        print("뿌");
       if (distance <= attackDistance)
        {
            state = BossState.ATTACK_STATE;
        }
    }
    private void Attack()
    {
        EnemyAnimator.SetBool("isAttack", true);
        collider.isTrigger = true;
        if (distance > attackDistance)
        {
            StartCoroutine(AttackToIdle());
        }
            
    }

    private void Hurt()
    {
        EnemyAnimator.SetBool("isDamaged", true);
        StartCoroutine(HurtToIdle());
    }
    
    IEnumerator AttackToIdle()
    {
        yield return new WaitForSeconds(0.9f);
        EnemyAnimator.SetBool("isRun", false);
        EnemyAnimator.SetBool("isAttack", false);
        collider.isTrigger = false;
        state = BossState.IDLE_STATE;
    }
    IEnumerator HurtToIdle()
    {
        yield return new WaitForSeconds(1f);
        EnemyAnimator.SetBool("isDamaged", false);
        state = BossState.IDLE_STATE;
    }

    IEnumerator IdleTOMove()
    {
        yield return new WaitForSeconds(1f);
        state = BossState.MOVE_STATE;
    }
}
*/