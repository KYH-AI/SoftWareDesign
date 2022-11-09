using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStrikeProjectile : Projectile
{
    private BoxCollider2D flameStrikeColilder;

    private void Start()
    {
        FlameStrikeInit();
        // TODO : �ִϸ��̼��� ����Ǹ� ����ü ����
    }

    private void FlameStrikeInit()
    {
        flameStrikeColilder = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.CompareTag(Define.StringTag.Enemy.ToString()))
        {
            target.GetComponent<Enemy>().TakeDamage(ProjectileDamage);
        }
    }
}
