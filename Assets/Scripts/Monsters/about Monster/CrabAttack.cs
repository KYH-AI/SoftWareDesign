using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CrabAttack : BasicMonsterController
{
    protected override void Attack()
    {
        base.EnemyAnimator.SetTrigger("Attack");
        StartCoroutine(AttackProcess());
        base.EnemyAnimator.SetTrigger("AttackToMove");
    }

    IEnumerator AttackProcess()
    {
        yield return new WaitForSeconds(1.0f);
        Vector3 dir = (this.transform.position - base.playerTarget.transform.position);
        transform.Translate(dir * MoveSpeed*10,Space.Self);
        Collider2D c = GetComponent<Collider2D>();
        if (c.gameObject.CompareTag("Player"))
        {
            base.DefaultAttack();
        }
    }
}