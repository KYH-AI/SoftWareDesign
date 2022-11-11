using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Enemy
{
    float shootSpeed=5f;
    float rotSpeed=5f;
    Vector3 dir;
    float time;
    new void Start()
    {
        dir = (this.transform.position - base.playerTarget.transform.position).normalized;
        time = 0;
    }

    void Update()
    {
        transform.Translate(dir.x, dir.y, 0, Space.World);
        transform.Rotate(0, 0, rotSpeed);
        time += Time.deltaTime;
        if (Vector3.Distance(this.transform.position, base.playerTarget.transform.position) < 0.5f)
        {
            base.DefaultAttack();
            gameObject.SetActive(false);
        }
        if (time >= 4)
            gameObject.SetActive(false);
    }
}
