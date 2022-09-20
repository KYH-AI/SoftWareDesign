using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class SlimeController: MonoBehaviour
{ 
    Transform target;
    float speed = 8f;
    int attackPower = 2;
    float attackDistance = 2.5f;
    CharacterController cc;
    Animator anim;

    public GameObject ItemPrefab;

    enum State
    {
        Run,
        Attack,
        Die
    }
    State state;

    void Start()
    {
        gameObject.SetActive(true);
        target = GameObject.Find("Player").transform;

        state = State.Run;
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
        Vector3 dir = (target.position - transform.position).normalized;

        //방향 바꾸기 재검토
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-180, Vector3.forward);
        cc.Move(dir * speed * Time.deltaTime);
        //transform.forward = target.forward;
        if(Vector2.Distance(target.position, transform.position) < attackDistance)
        {
            state = State.Attack;
        }
    }


    private void Attack()
    {
        anim.SetTrigger("Attack");
        target.GetComponent<PlayerController_GN>().damage(attackPower);
        StartCoroutine(AttackProcess());
        Die();
    }

    IEnumerator AttackProcess()
    {
        yield return new WaitForSeconds(1.0f);
        state = State.Die;
    }

    private void Die()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        anim.SetTrigger("Die");
        GameObject coin = Instantiate(ItemPrefab, transform.position, transform.rotation);
        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    public void HitEnemy(int hitPower)
    {
            state = State.Die;
            Die();
    }
}