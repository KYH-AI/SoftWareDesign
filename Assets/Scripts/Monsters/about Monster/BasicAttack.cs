using System.Collections;
using UnityEngine;

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
    }

    void AttackPlayer()
    {
        Collider2D c = GetComponent<Collider2D>();
        if (c.gameObject.CompareTag("Player")){
            base.DefaultAttack();
        }
    }
}

