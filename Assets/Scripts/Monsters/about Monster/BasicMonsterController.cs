using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public abstract class BasicMonsterController : Enemy
{
    public Text DamageInform;
    //public GameObject coinPrephab;
    enum State
    {
        Run,
        Attack,
        Damage,
        Die
    }
    State state;

    public float coolTime=-1.0f, skillTime = 2.0f;
    SpriteRenderer renderer;


    public int minKillCount;
    public int maxKillCount;

    public void Start()
    {
        //base.Start();
        //base.playerTarget = GameObject.Find("Player").transform;
        //renderer = GetComponent<SpriteRenderer>();
        //state = State.Run;
    }
    public void Update()
    {
        if (state == State.Run) Run();
        if (state == State.Attack) Attack();
        if (state == State.Die) OnDead();
    }

    //�޸���
    public void Run()
    {
        //base.Move();
        //if ((playerTarget.position.x - this.transform.position.x) < 0)
        //    renderer.flipX = true;
        //else renderer.flipX = false;
        //    if (base.Hp <= 0)s
        //{
        //    state = State.Die;
        //    return;
        //}
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
        if(collision.tag=="Player")
            state = State.Run;
        coolTime = -1.0f;
        base.EnemyAnimator.SetTrigger("AttackToMove");
    }

    protected abstract void Attack();


    //������ �ޱ�
    //������ ǥ��, ������ ���� �ִϸ��̼�
    public override sealed void TakeDamage(int newDamage)
    {
        base.TakeDamage(newDamage);
        DamageInform.text = "-"+newDamage.ToString();
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
     
        base.EnemyAnimator.SetTrigger("Die");
        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(1.0f);
        /*GameObject coin = Resources.Load<GameObject>("Prefabs/BlueCoin");
        coin.transform.position = this.transform.position;
        Instantiate(coin);*/


        int killCount = Random.Range(minKillCount, maxKillCount);

        //���� ���� �˰���
        //ĳ���� ������ ųī��Ʈ �Ѱ��ֱ�
        gameObject.SetActive(false);
    }
}