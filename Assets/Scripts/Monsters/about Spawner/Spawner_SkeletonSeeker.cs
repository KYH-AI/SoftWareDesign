using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_SkeletonSeeker : MonoBehaviour
{
    int[] dx = new int[] { 10,10 };
    int[] dy = new int[] { -10,-10};
    public GameObject Prefab;
    public GameObject[] Monster;
    public float spawnRateMin;
    public float spawnRateMax;

    private float spawnRate;

    bool state;
    int idx;
    public int MAX;

    // Start is called before the first frame update
    void Start()
    {
        //dealy();
        Invoke(nameof(dealy), 1f);
    }

    void dealy()
    {
        idx = 0;
        Monster = new GameObject[MAX];
        for (int i = 0; i < MAX; i++)
        {
            GameObject ob = Instantiate(Prefab);
            ob.GetComponent<Enemy>().EnemyInit(Managers.StageManager.Player);
            Monster[i] = ob;
            ob.SetActive(false);
        }
        state = true;
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        yield return new WaitForSeconds(spawnRate);
        if (Monster[idx].activeInHierarchy == false)
        {
            int n = (int)Random.Range(0, 2);
            Monster[idx].transform.position = gameObject.transform.position + Vector3.left * dx[n] + Vector3.up * dy[n];
            Monster[idx].SetActive(true);
            state = false;
        }
        idx++;
        if (idx == MAX)
        {
            if (state == true)//Max까지 최대 오브젝트 setActive(true) 상태
            {
               // go();
            }
            idx = 0;
            state = true;
        }
        StartCoroutine("Spawn");
    }

    public void go()
    {
        Monster[0].GetComponent<SkeletonSeekerController>().Ready();
        Monster[1].GetComponent<SkeletonSeekerController>().Ready();
    }
}
