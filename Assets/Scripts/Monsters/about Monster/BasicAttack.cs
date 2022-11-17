using System.Collections;
using UnityEngine;

public class BasicAttack : BasicMonsterController
{
    protected int playerLayer = 1 << 10;
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
        Collider2D playerCollider = Physics2D.OverlapCircle(gameObject.transform.position, 2f, playerLayer);
        if (playerCollider != null){
            base.DefaultAttack();
            print("µ¥¹ÌÁö ÁÜ!");
        }
    }
}

