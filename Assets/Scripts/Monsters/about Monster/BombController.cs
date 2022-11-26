using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    int damage = 15;
    float bombAttackRadius = 0.5f;
    public GameObject crab;
    Animator anim;
    float shootSpeed=2.0f;
    Vector3 dir;
    float time;
    void Start()
    {
        dir = (this.transform.position - crab.transform.position).normalized;
        anim = GetComponent<Animator>();
        time = 0;
    }

    void Update()
    {
        transform.Translate(dir.x*shootSpeed*Time.deltaTime, dir.y*shootSpeed*Time.deltaTime, 0, Space.World);
        time += Time.deltaTime;
         if (time >= 0.8f)
         {
            time = 0;
            anim.SetTrigger("bomb");
            anim.SetTrigger("back");
            StartCoroutine(Bomb());
         }
    }

    IEnumerator Bomb()
    {
        Collider2D target = Physics2D.OverlapCircle(this.transform.position, bombAttackRadius, 1<<10);
        if(target!=null)
            target.gameObject.GetComponent<Player>().TakeDamage(damage);

        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }
}
