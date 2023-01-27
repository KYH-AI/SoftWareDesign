using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : SubBoss
{
    public enum BossState
    {
        IDLE_STATE,
        MOVE_STATE,
        ATTACK1_STATE,
        ATTACK2_STATE,
        Dead_STATE
    }
    float distance;
    BossState state = BossState.IDLE_STATE;
    bool isIdle = true;
    float attackDelay = 4f;
    float attackdistance = 3f;
    int attack2Cnt =0;
    int skillDamage = 3;
    [SerializeField] GameObject Spell;
    [SerializeField] GameObject Portalpref;
    private void Start()
    {
        base.Start();
    }
    void Update()
    {
        Measure();
        Fsm();
        ChangeOrder();

    }
    protected override void Measure()
    {
        base.Measure();
        distance = dir.magnitude;
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
            case BossState.ATTACK1_STATE:
                Attack1();
                break;
            case BossState.ATTACK2_STATE:
                Attack2();
                break;
            case BossState.Dead_STATE:
                break;
        }
    }
    void Idle()
    {
        if (isIdle&& isStart)
        {
            isIdle = false;
            Invoke(nameof(IdleToAttack), attackDelay);
        }
    }
    private void IdleToAttack()
    {

        isIdle = true;
        int result = Random.Range(0, 2);
        if (result == 0)
        {
            state = BossState.MOVE_STATE;
        }
        else
        {
            state = BossState.ATTACK2_STATE;
        }
    }


    new void Move()
    {
        if (!isDie)
        {
            EnemyAnimator.SetBool("isMove", true);
            ChangeDir();
            if (dir.x < 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTarget.transform.position.x + 2.5f,
                    playerTarget.transform.position.y - 1f), MoveSpeed * Time.deltaTime);

            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTarget.transform.position.x - 2.5f,
                    playerTarget.transform.position.y - 1f), MoveSpeed * Time.deltaTime);
            }

            if (distance < attackdistance && dir.normalized.y < 0.6f && dir.normalized.y > 0.2f)
            {
                state = BossState.ATTACK1_STATE;
            }
        }
        
    }

    void Attack1ToIdle()
    {
        state = BossState.IDLE_STATE;
        EnemyAnimator.SetBool("isAttack1", false);
        EnemyAnimator.SetBool("isMove", false);
    }

    void Attack2ToIdle()
    {
        attack2Cnt++;
        if(attack2Cnt == 5)
        {
            state = BossState.IDLE_STATE;
            EnemyAnimator.SetBool("isAttack2", false);
            attack2Cnt = 0;
        }
        GameObject projectile = MemoryPoolManager.GetInstance().OutputGameObject(Spell,
                                                                                   "SubBoss/"+Spell.name,
                                                                                   new Vector2(playerTarget.transform.position.x, playerTarget.transform.position.y +3.8f),
                                                                                   Quaternion.identity);
        projectile.GetComponent<Projectile>().ProjectileInit(Define.StringTag.Player, Vector2.zero, skillDamage);
        projectile.SetActive(true);

    }
    void Attack1()
    {
        EnemyAnimator.SetBool("isAttack1", true);
        EnemyAnimator.SetBool("isHit", false);
    }
    void Attack2()
    {
        EnemyAnimator.SetBool("isAttack2", true);
        EnemyAnimator.SetBool("isHit", false);
    }
    public override void TakeDamage(int newDamage)
    {
        base.TakeDamage(newDamage);
        Managers.StageManager.IsBossAlive(Hp);
        Managers.UI.UpdateBossHpSlider(Hp, MaxHp);
        EnemyAnimator.SetBool("isHit", true);
    }
    private void HurtToIdle()
    {
        EnemyAnimator.SetBool("isHit", false);
    }
    protected override void OnDead()
    {
        isDie = true;
        state = BossState.Dead_STATE;
        base.OnDead();
        Managers.UI.bossSlider.gameObject.SetActive(false);
        EnemyAnimator.SetTrigger("Die");
        DeadSound();
    }

    private void destory()//죽는 애니 마지막에 넣기
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
    void Attack1Sound()
    {
        PlaySound("SubBoss/Boss3_4_Attack_SFX");
    }
    void DeadSound()
    {
        PlaySound("SubBoss/boss4_Die_SFX");
    }
}
