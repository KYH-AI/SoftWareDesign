using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SubBoss : Enemy
{
    protected GameObject myInstance;
    protected int Boss = 1 << 14;
    protected bool isStart = false;
    protected bool isDie = false;
    protected Vector2 dir;
    protected new void Start()
    {
        BasicStatInit();
        Invoke(nameof(GetBossLayer), 4.5f);
    }
    private void GetBossLayer()
    {
        gameObject.layer = 14;
        gameObject.tag = Define.StringTag.Enemy.ToString();
        isStart = true;
    }
   
    protected virtual void Measure() 
    {
        dir = (playerTarget.transform.position - transform.position);
    }
    protected void ChangeOrder()
    {
        if (dir.y < 0)
        {
            SpriteRenderer.sortingOrder = 3;
        }
        else
        {
            SpriteRenderer.sortingOrder = 5;
        }
    }
    protected void ChangeDir()
    {
        if (dir.x < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    protected void PlaySound(string path)
    {
        Managers.Sound.PlaySFXAudio(path);
    }
}
