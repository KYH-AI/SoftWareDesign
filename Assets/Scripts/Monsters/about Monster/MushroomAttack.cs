using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.AI;

public class MushroomAttack : BasicMonsterController
{
    Collider2D c;
    float time = 0;
    protected override void Attack()
    {
        c = GetComponent<Collider2D>();
        base.EnemyAnimator.SetTrigger("Attack");
        base.EnemyAnimator.SetTrigger("Smoke");
        c.isTrigger = true;
        this.transform.localPosition = (new Vector3(10,10,0));
        StartCoroutine(AttackProcess());
        base.coolTime = base.skillTime;
    }

    IEnumerator AttackProcess()
    {
        if (c.gameObject.CompareTag("Player"))
        {
            base.DefaultAttack();
        }
        yield return new WaitForSeconds(2.0f);

        if (time >= 20)
        {
            gameObject.SetActive(false);
        }
        else
        {
            time += Time.deltaTime;
            StartCoroutine("AttackProcess");
        }
    }

}