using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SuicideMonsterController : Enemy
{
    public Text DamageInform;
    enum State
    {
        Run,
        Attack,
        Damage,
        Die
    }
    State state;

    float coolTime = -1.0f, skillTime = 2.0f;
    private void Awake()
    {
        base.Start();
        base.playerTarget = GameObject.Find("Player").transform;
        state = State.Run;
    }
    private void Update()
    {
        if (state == State.Run) Run();
        if (state == State.Attack) Attack();
        if (state == State.Die) OnDead();
    }

    //�޸���
    private void Run()
    {
        base.Move();
        if (base.Hp <= 0)
        {
            state = State.Die;
            return;
        }
    }

    //����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            state = State.Attack;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            state = State.Run;
        coolTime = -1.0f;
        base.EnemyAnimator.SetTrigger("AttackToMove");
    }

    private void Attack()
    {
        if (coolTime < 0)
        {
            Debug.Log(coolTime);
            base.EnemyAnimator.SetTrigger("Attack");
            StartCoroutine(AttackProcess());
            base.EnemyAnimator.SetTrigger("AttackToMove");
            coolTime = skillTime;
        }
        coolTime -= Time.deltaTime;
        Debug.Log(coolTime);
    }

    IEnumerator AttackProcess()
    {
        base.DefaultAttack();
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }


    //������ �ޱ�
    //������ ǥ��, ������ ���� �ִϸ��̼�
    public override sealed void TakeDamage(int newDamage)
    {
        base.TakeDamage(newDamage);
        DamageInform.text = "-" + newDamage.ToString();
        StartCoroutine(DamageProcess());
        base.EnemyAnimator.SetTrigger("DamageToMove");
    }

    IEnumerator DamageProcess()
    {
        DamageInform.enabled = true;
        yield return new WaitForSeconds(1.0f);
        DamageInform.enabled = false;
        state = State.Run;
    }


    //����
    //���� �ִϸ��̼�,���ε��,��Ȱ��ȭ
    protected override sealed void OnDead()
    {
        base.OnDead();
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        base.EnemyAnimator.SetTrigger("Die");
        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject coin = Resources.Load<GameObject>("Prefabs/BlueCoin");
        coin.transform.position = this.transform.position;
        Instantiate(coin);
        gameObject.SetActive(false);
    }
}
