using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BoneAttack :BasicMonsterController
{
    public GameObject Prefab;
    public GameObject[] Monster;
    int idx = 0;
    int MAX = 5;
    bool isAttack=true;
    protected override void Attack()
    {
        if (isAttack == false) base.state = State.Run;
        else
        {
            isAttack = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            Monster = new GameObject[MAX];
            for (int i = 0; i < MAX; i++)
            {
                GameObject ob = Instantiate(Prefab);
                ob.GetComponent<Enemy>().EnemyInit(StageManager.GetInstance().Player);
                Monster[i] = ob;
                ob.SetActive(false);
            }
            StartCoroutine("Spawn");
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(base.skillTime);
        Monster[idx].transform.position = gameObject.transform.position;
        Monster[idx++].SetActive(true);
        if (idx != MAX)
            StartCoroutine("Spawn");
        else
        {
            //gameObject.SetActive(false);
            idx = 0;
        }
    }
}