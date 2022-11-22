using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject crab;
    Animator anim;
    float shootSpeed=1.0f;
    public Vector3 dir;
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
         if (time >= 1.5)
         {
            time = 0;
            anim.SetTrigger("bomb");
            anim.SetTrigger("back");
            StartCoroutine(Bomb());
         }
    }

    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
