using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Projectile
{
    private Animator Animator;
    float burnDelay = 0.5f;
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    IEnumerator Burning()
    {
        yield return new WaitForSeconds(burnDelay);
        Animator.SetBool("isBurn",true);
    }
    private void DisableObject()
    {
        Animator.SetBool("isBurn", false);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        MemoryPoolManager.GetInstance().InputGameObject(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag(TargetTag.ToString()))
        {
            target.GetComponent<Player>().TakeDamage(ProjectileDamage);
        }
    }
}

