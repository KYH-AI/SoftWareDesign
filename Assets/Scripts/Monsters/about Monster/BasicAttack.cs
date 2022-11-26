using System.Collections;
using UnityEngine;

public class BasicAttack : BasicMonsterController
{
    public float attackRadius;
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
        if(Physics2D.OverlapCircle(this.transform.position, attackRadius, 1<<10))
        {
            base.DefaultAttack();
        }

    }
}

