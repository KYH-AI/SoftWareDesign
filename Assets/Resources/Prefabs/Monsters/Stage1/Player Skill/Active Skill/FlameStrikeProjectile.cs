using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStrikeProjectile : Projectile
{
    private BoxCollider2D flameStrikeColilder;

    private void Start()
    {
        FlameStrikeInit();
    }


    private void OnEnable()
    {
        Invoke(nameof(DisableObject), 5f);
    }


    private void FlameStrikeInit()
    {
        flameStrikeColilder = GetComponent<BoxCollider2D>();
    }

 
    private void DisableObject()
    {
        // TODO : �ִϸ��̼��� ����Ǹ� ����ü ����
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        MemoryPoolManager.GetInstance().InputGameObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.CompareTag(Define.StringTag.Enemy.ToString()))
        {
            target.GetComponent<Enemy>().TakeDamage(ProjectileDamage);
        }
    }
}
