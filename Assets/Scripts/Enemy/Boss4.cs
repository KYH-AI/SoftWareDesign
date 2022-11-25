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
        HIDE_STATE
    };
    float distance;
    float moveDistance = 15f;
    float attackDistance = 2.5f;
    float moveSpeed = 4f;
    float attackDelay = 2f;
    Vector2 dir;
    BossState state = BossState.IDLE_STATE;
    bool isAttack = true;
    bool onTrigger = true;
    bool isHurt = false;
    int attackCnt = 0;
    bool isFadeout = true;
    


    // Update is called once per frame
    void Update()
    {
        dir = (playerTarget.transform.position - transform.position);
        ChangeDir();
        distance = dir.magnitude;  //Vector2.Distance(transform.position, playerTarget.transform.position);
        Fsm();
       // print(attackCnt);
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
            case BossState.HIDE_STATE:
                Hide();
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && onTrigger)
        {
            DefaultAttack();
        }
        onTrigger = false;
    }

    void Idle()
    {
        if (distance < moveDistance&&isAttack) 
        {
            state = BossState.MOVE_STATE;
        }
        EnemyAnimator.SetBool("isRun", false);
    }
    

    private new  void Move()
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
    private void DamagedIn()
    {
        gameObject.tag = "dontDamaged";
    }

    private void DamagedOut()
    {
        gameObject.tag = "Enemy";
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
    void DeadProcess()//Die애니메이션에 넣음
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
        DamagedIn();
        yield return new WaitForSeconds(1f);
        EnemyAnimator.SetBool("isDamaged", false);
        isHurt = false;
        DamagedOut();
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
       // yield return new WaitForSeconds(0.1f);//0.2초 동안 idle
        EnemyAnimator.SetBool("isAttack", true);
        
        yield return new WaitForSeconds(attackDelay);//공격 딜레이
        onTrigger = true;
        isAttack = true;
    }
    void AttackToIdle()//attack애니메이션 마지막에 넣음
    {
        attackCnt++;
        EnemyAnimator.SetBool("isAttack", false);
        StartCoroutine(AfterDelay());//공격 후 딜레이
        //EnemyCollider.isTrigger = false;
    }
    IEnumerator AfterDelay()
    {
        if(attackCnt == 2)
        {
            yield return new WaitForSeconds(0.8f);
            isFadeout = true;
            state = BossState.HIDE_STATE;
            attackCnt = 0;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            if (distance > attackDistance)//멀어지면 idle 상태로 돌아가서 플레이어 추적하기
            {
                state = BossState.IDLE_STATE;
            }
        }
       
    }
    private void Hide()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, moveSpeed * Time.deltaTime);
        if (isFadeout)
        {
           StartCoroutine(FadeOut());
            isFadeout = false;
        }
         
       
    }
    IEnumerator FadeOut()
    {
        while (SpriteRenderer.color.a > 0)
        {
            var color = SpriteRenderer.color;
            //color.a is 0 to 1. So .5*time.deltaTime will take 2 seconds to fade out
            color.a -= (3f * Time.deltaTime);
            if(color.a < 0)
            {
                color.a = 0;
            }
            SpriteRenderer.color = color;
            //wait for a frame
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        state = BossState.ATTACK_STATE;
        while (SpriteRenderer.color.a < 1)
        {
            var color = SpriteRenderer.color;
            //color.a is 0 to 1. So .5*time.deltaTime will take 2 seconds to fade out
            color.a += (3f * Time.deltaTime);
         
            SpriteRenderer.color = color;
            //wait for a frame
            yield return null;
        }
       
       
    }

}
