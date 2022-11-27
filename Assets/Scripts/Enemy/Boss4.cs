using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
    float attackDelay = 2f;
    Vector2 dir;
    BossState state = BossState.IDLE_STATE;
    bool isAttack = true;
    bool isHurt = false;
    bool isDie = false;
    int attackCnt = 0;
    bool isFadeout = true;
    bool isHide = false;
    [SerializeField] GameObject Portalpref;
    GameObject myInstance;


    // Update is called once per frame
    void Update()
    {
        dir = (playerTarget.transform.position - transform.position);
        if (!isHurt)
        {
            ChangeDir();
        }
       distance = dir.magnitude; 
        Fsm();
        ChangeOrder();
    }
    void ChangeOrder()
    {
        if (dir.y < 0)
        {
            SpriteRenderer.sortingOrder = 3;
        }
        else
        {
            SpriteRenderer.sortingOrder = 5;
        }
    }
    private void ChangeDir()
    {
        if (dir.x < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
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

    void Idle()
    {
        if (distance < moveDistance&&isAttack&&!isDie) 
        {
            state = BossState.MOVE_STATE;
        }
        EnemyAnimator.SetBool("isRun", false);
    }
    

    private new  void Move()
    {
        if (!isHurt)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, MoveSpeed * Time.deltaTime);
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
        print("데미지 받음");
        base.TakeDamage(newDamage);
        Hurt();
    }

  

    protected override void OnDead()
    {
        base.OnDead();
        isDie = true;
        EnemyAnimator.SetTrigger("isDie");
    }
    void DeadProcess()//Die애니메이션에 넣음
    {
        var color = SpriteRenderer.color;
        color.a = 0;
        SpriteRenderer.color = color;
        Invoke(nameof(SpawnPortal), 2f);
    }
    void SpawnPortal()
    {
        myInstance = Instantiate(Portalpref);
        myInstance.transform.position = transform.position;
        Destroy(gameObject);

    }

    private void Hurt()
    {
        EnemyAnimator.SetBool("isDamaged", true);
        isHurt = true;
    }
    
    void  HurtToIdle()
    {
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
       // yield return new WaitForSeconds(0.1f);//0.2초 동안 idle
        EnemyAnimator.SetBool("isAttack", true);
        
        yield return new WaitForSeconds(attackDelay);//공격 딜레이 
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
            state = BossState.HIDE_STATE;
            yield return new WaitForSeconds(0.8f);
            isFadeout = true;
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
        if (isHide)
        {
            int result = Random.Range(0, 2);
            if (result == 0)
            {
                transform.position = new Vector2(playerTarget.transform.position.x + 3f, playerTarget.transform.position.y);
            }
            else
            {
                transform.position = new Vector2(playerTarget.transform.position.x - 3f, playerTarget.transform.position.y);
            }
            
        }
        if (isFadeout)
        {
           StartCoroutine(FadeOut());
            isFadeout = false;
        }
         
       
    }
    IEnumerator FadeOut()
    {
        gameObject.tag = "Untagged";
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
        isHide = true;
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        gameObject.tag = "Enemy";
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

        isHide = false;

    }

}
