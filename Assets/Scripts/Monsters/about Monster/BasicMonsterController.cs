using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public abstract class BasicMonsterController : Enemy
{
    float radius = 0.2f;   
    public int playerLayer = 1<<10;
    //public GameObject coinPrephab;
    public enum State
    {
        Run,
        Attack,
        Damage,
        Die
    }
    public State state;

    public float coolTime=-1.0f, skillTime = 2.0f;
    new SpriteRenderer renderer;


    public int minKillCount;
    public int maxKillCount;

    new public void Start()
    {
        base.Start();
        renderer = GetComponent<SpriteRenderer>();
        state = State.Run;
    }
    public void Update()
    {
        if (state == State.Run) Run();
        if (state == State.Attack) Attack();
        if (state == State.Die) OnDead();
    }

    //달리기
    public void Run()
    {
        base.Move();
        if ((playerTarget.gameObject.transform.position.x - this.transform.position.x) < 0)
            renderer.flipX = true;
        else renderer.flipX = false;
    }

    //공격
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DamagedRadius"))
        {
            state = State.Attack;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="DamagedRadius")
            state = State.Run;
        coolTime = -1.0f;
        base.EnemyAnimator.SetTrigger("AttackToMove");
    }

    protected abstract void Attack();


    //데미지 받기
    //데미지 표시, 데미지 입은 애니메이션
    public override sealed void TakeDamage(int newDamage)
    {
        base.TakeDamage(newDamage);
        StartCoroutine(DamageProcess());
        if (base.Hp <= 0)
        {
            state = State.Die;
            return;
        }
        //state Run or Attack
        if (Physics2D.OverlapCircle(this.transform.position, radius, 1<<10) == true)
        {
            state = State.Attack;
            base.EnemyAnimator.SetTrigger("DamageToAttack");
        }
        else
        {
            state = State.Run;
            base.EnemyAnimator.SetTrigger("DamageToMove");
        }
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(1.0f);
    }


    //죽음
    //죽음 애니메이션,코인드랍,비활성화
    protected override sealed void OnDead()
    {
        base.OnDead();
     
        base.EnemyAnimator.SetTrigger("Die");
        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(0.5f);

        int killCount = Random.Range(minKillCount, maxKillCount);

        //동전 드랍
        //캐릭터 정보에 킬카운트 넘겨주기

        gameObject.SetActive(false);
    }
}