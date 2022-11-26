using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.AI;

public class MushroomAttack : BasicMonsterController
{
    float mushroomAttackRadius = 0.6f;
    Collider2D c;
    float time = 0;
    SpriteRenderer mushroomRenderer;
    Vector3 bombPosition;

    public Canvas canvas;

    bool isAttack = false;
    protected override void Attack()
    {
        if (isAttack == true)
        {
            return;
        }
        isAttack = true;
        mushroomRenderer = GetComponent<SpriteRenderer>();
        c = GetComponent<Collider2D>();
        base.EnemyAnimator.SetTrigger("Attack");
        bombPosition = this.transform.position;
        canvas.gameObject.SetActive(false);
        base.EnemyAnimator.SetTrigger("Smoke");
        transform.position = bombPosition;
        c.isTrigger = true;
        Hp = 10000000;
        StartCoroutine(AttackProcess());
    }

    IEnumerator AttackProcess()
    {
        transform.position = bombPosition;
        this.transform.localScale = (new Vector3(10, 10, 0));
        mushroomRenderer.color = new Color(100/255f, 55/255f, 140/255f, 220/255f);
        if (Physics2D.OverlapCircle(this.transform.position, mushroomAttackRadius, 1<<10) == true)
        {
            base.DefaultAttack();
            Debug.Log("Monster Attack!");
        }
        yield return new WaitForSeconds(2.0f);

        if (time >= 5)
        {
            
            this.transform.localScale = (new Vector3(4, 4, 0));
            canvas.gameObject.SetActive(true);
            mushroomRenderer.color = new Color(255, 255, 255,255);
            gameObject.SetActive(false);
        }
        else
        {
            time += Time.deltaTime;

            StartCoroutine("AttackProcess");
        }
    }

}