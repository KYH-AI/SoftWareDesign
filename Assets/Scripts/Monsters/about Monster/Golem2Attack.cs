using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.AI;

public class Golem2Attack : BasicMonsterController
{
    public GameObject Prefab;
    public GameObject[] Monster;
    int idx = 0;
    int MAX = 2;
    protected override void Attack()
    {
        Monster = new GameObject[MAX];
        for (int i = 0; i < MAX; i++)
        {
            GameObject ob = Instantiate(Prefab);
            Monster[i] = ob;
            ob.SetActive(false);
        }
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(base.skillTime);
        Monster[idx].transform.position = gameObject.transform.position;
        Monster[idx++].SetActive(true);
        StartCoroutine("Spawn");
    }
}