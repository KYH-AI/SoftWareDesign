using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.AI;

public class MushroomAttack : BasicMonsterController
{
    Collider2D c;
    float time = 0;
    Vector3 bombPosition;

    public Canvas canvas;
    protected override void Attack()
    {
        c = GetComponent<Collider2D>();
        base.EnemyAnimator.SetTrigger("Attack");
        bombPosition = this.transform.position;
        canvas.gameObject.SetActive(false);
        base.EnemyAnimator.SetTrigger("Smoke");
        transform.position = bombPosition;
        c.isTrigger = true;
        Hp = 10000;
        StartCoroutine(AttackProcess());
    }

    IEnumerator AttackProcess()
    {
        transform.position = bombPosition;
        this.transform.localScale = (new Vector3(10, 10, 0));
        if (c.gameObject.CompareTag("Player"))
        {
            base.DefaultAttack();
        }
        yield return new WaitForSeconds(2.0f);

        if (time >= 40)
        {
            this.transform.localScale = (new Vector3(4, 4, 0));
            canvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            time += Time.deltaTime;
            StartCoroutine("AttackProcess");
        }
    }

}