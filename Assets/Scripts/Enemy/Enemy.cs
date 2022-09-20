using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : LivingEntity, IBasicMovement
{
    public Transform playerTarget;

    private Rigidbody2D enemyRigidbody;
    private Animator enemyAnimator;
    public Rigidbody2D EnemyRigidbody { get { return enemyRigidbody; } }
    public Animator EnemyAnimator { get { return enemyAnimator; } }

    AIPath a;

    protected void Start()
    {
        Init();

        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }


    public void DefaultAttack()
    {
         playerTarget.GetComponent<LivingEntity>().TakeDamage(DefaultAttackDamage);
    }

    protected override void OnDead() { }


    public void Move()
    {
        Vector2 dir = (playerTarget.transform.position - transform.position).normalized;
        enemyRigidbody.velocity = MoveSpeed * dir;
    }

}

