using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeProjectile : Projectile
{
    private Rigidbody2D knifteRigidBody;

    private void Start()
    {
        KnifeProjectileInit();
        // TODO : ����ü �����Ÿ� ����
    }

    private void KnifeProjectileInit()
    {
        knifteRigidBody = GetComponent<Rigidbody2D>();
        knifteRigidBody.velocity = Dir.normalized * ProjectileSpeed;
    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag(Define.StringTag.Enemy.ToString()))
        {
            target.GetComponent<Enemy>().TakeDamage(ProjectileDamage);
            // TODO : ����ü ����
        }
    }
}
