using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BatController : MonoBehaviour
{
    Transform target;
    float speed = 8f;
    int attackPower = 2;
    int HP;
    int maxHP;
    float attackDistance = 2.5f;
    CharacterController cc;
    Animator anim;

    public Slider hpBar;
    public GameObject ItemPrefab1;
    public GameObject ItemPrefab2;

    enum State
    {
        Run,
        Attack,
        Damage,
        Die
    }
    State state;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        HP = 20;
        maxHP = HP;
        state = State.Run;
        
        hpBar.value = HP / maxHP;
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (state == State.Run) Run();
        else if (state == State.Attack) Attack();
    }

    private void Run()
    {
        if (HP <= 0)
        {
            state = State.Die;
            Die();
            return;
        }
        Vector3 dir = (target.position - transform.position).normalized;
        //방향 바꾸기 재검토
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        cc.Move(dir * speed * Time.deltaTime);
        if (Vector2.Distance(target.position, transform.position) < attackDistance)
        {
            state = State.Attack;
        }
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        target.GetComponent<PlayerController_GN>().damage(attackPower);
        StartCoroutine(AttackProcess());
        state = State.Die;
        Die();
    }

    IEnumerator AttackProcess()
    {
        yield return new WaitForSeconds(2.0f);
    }

    private void Damage()
    {
        StartCoroutine(DamageProcess());
        anim.SetTrigger("DamageToMove");
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(1.0f);
        state = State.Run;
    }


    private void Die()
    {
        hpBar.value = 0;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        anim.SetTrigger("Die");
        int blue = Random.Range(1, 3);
        for (int i = 1; i <= blue; i++)
            spawnBlue(i);
        spawnOrange();
        StartCoroutine(DieProcess());
    }

    private void spawnBlue(int n)
    {
        GameObject bluecoin = Instantiate(ItemPrefab1, transform.position, transform.rotation);
        if(n==1)
            bluecoin.transform.Translate(Vector3.right);
        if(n==2)
            bluecoin.transform.Translate(Vector3.left);
    }
    private void spawnOrange()
    {
        GameObject orangecoin = Instantiate(ItemPrefab2, transform.position, transform.rotation);
    }
    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public void HitEnemy(int hitPower)
    {
        if (state == State.Die)
        {
            return;
        }
        hpBar.IsActive();
        HP -= hitPower;
        hpBar.value = (float)HP / (float)maxHP;
        if (HP > 0)
        {
            state = State.Damage;
            anim.SetTrigger("MoveToDamage");
            Damage();
        }
        else
        {
            state = State.Die;
            Die();
        }
    }

}
