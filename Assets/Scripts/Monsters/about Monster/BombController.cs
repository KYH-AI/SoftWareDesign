using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    int damage = 15;
    float bombAttackRadius = 0.5f;
    public GameObject crab;
    public Animator anim;
    float shootSpeed=2.0f;
    Vector3 dir;
    float time;

    void OnEnable()
    {
        dir = (this.transform.position - crab.transform.position).normalized;
        anim = GetComponent<Animator>();
        time = 0;
    }

    void Update()
    {
         if (time >= 0.6f)
         {
            anim.SetTrigger("bomb");
            Debug.Log("Set Bomb!");
            StartCoroutine(Bomb());
        }
        else
        {
            transform.Translate(dir.x * shootSpeed * Time.deltaTime, dir.y * shootSpeed * Time.deltaTime, 0, Space.World);
            time += Time.deltaTime;
        }
    }

    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(1.2f);
        anim.SetTrigger("back");
        time = 0;
        gameObject.SetActive(false);
    }

    void AttackPlayer()
    {
        Collider2D target = Physics2D.OverlapCircle(this.transform.position, bombAttackRadius, 1 << 10);
        if (target != null)
            target.gameObject.GetComponent<Player>().TakeDamage(damage);
    }
}
