using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_FixedMaximum : MonoBehaviour
{
    int[] dx = new int[] { 10, -10, 10, -10, 30, -30, 30, -30 };
    int[] dy = new int[] { 30, 30, -30, -30, 30, 10, -10, -10 };
    public GameObject Prefab;
    public GameObject[] Monster;
    public float spawnRateMin;
    public float spawnRateMax;

    private float spawnRate;

    bool state;
    int idx;
    public int MAX;

    int MaxCnt=10;
    int cnt = 0;

    bool spawnRestart = false;
    // Start is called before the first frame update
    void Start()
    {
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
        spawnRestart = true;
        StartCoroutine("Spawn");
    }

    private void OnEnable()
    {
        if (spawnRestart == true)
        {
            spawnRateMin = 10.0f;
            spawnRateMax = 12.0f;
            StartCoroutine("Spawn");
        }
    }


    IEnumerator Spawn()
    {
        if (Managers.StageManager.monsterCounter < MaxCnt)
        {
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            yield return new WaitForSeconds(spawnRate);
            if (Monster[idx].activeInHierarchy == false)
            {
                int n = (int)Random.Range(0, 8);
                Monster[idx].transform.position = gameObject.transform.position + Vector3.left * dx[n] + Vector3.up * dy[n];
                Monster[idx].SetActive(true);
                state = false;
            }
            idx++;
            if (idx == MAX)
            {
                if (state == true)//Max까지 최대 오브젝트 setActive(true) 상태
                {

                }
                idx = 0;
                state = true;
            }
            cnt++;
            StartCoroutine("Spawn");
        }
    }
}
