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
    protected override void Attack()
    {
        mushroomRenderer = GetComponent<SpriteRenderer>();
        c = GetComponent<Collider2D>();
        base.EnemyAnimator.SetTrigger("Attack");
        bombPosition = this.transform.position;
        canvas.gameObject.SetActive(false);
        base.EnemyAnimator.SetTrigger("Smoke");
        transform.position = bombPosition;
        c.isTrigger = true;
        Hp = 10000;
        StartCoroutine(AttackProcess());
    }

    IEnumerator AttackProcess()
    {
        transform.position = bombPosition;
        this.transform.localScale = (new Vector3(10, 10, 0));
        mushroomRenderer.color = new Color(45/255f, 100/255f, 65/255f, 200/255f);





        if (Physics2D.OverlapCircle(this.transform.position, mushroomAttackRadius, 1<<10) == true)
        {
            base.DefaultAttack();
            Debug.Log("Monster Attack!");
        }
        yield return new WaitForSeconds(2.0f);

        if (time >= 40)
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