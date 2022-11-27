using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierEvent : MonoBehaviour
{

    Barrier barrier;

    private void Awake()
    {
        barrier = GetComponentInParent<Barrier>();
    }

    private void OnCompleteEvent() // Barrier Anim ���� �̺�Ʈ ȣ��
    {
        barrier.BarrierAnimator.SetTrigger("OnAcitveBarrier");
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.CompareTag(Define.StringTag.Enemy.ToString()))
        {
            target.GetComponent<Enemy>().TakeDamage(barrier.SkillDamage);
        }
    }
}

