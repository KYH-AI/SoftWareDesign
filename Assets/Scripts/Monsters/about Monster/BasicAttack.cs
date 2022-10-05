using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BasicAttack : BasicMonsterController
{
    protected override void Attack()
    {
        if (base.coolTime < 0)
        {
            base.EnemyAnimator.SetTrigger("Attack");
            StartCoroutine(AttackProcess());
            base.EnemyAnimator.SetTrigger("AttackToMove");
            base.coolTime = base.skillTime;
        }
        base.coolTime -= Time.deltaTime;
    }

    IEnumerator AttackProcess()
    {
        yield return new WaitForSeconds(1.0f);
        base.DefaultAttack();
    }
}

