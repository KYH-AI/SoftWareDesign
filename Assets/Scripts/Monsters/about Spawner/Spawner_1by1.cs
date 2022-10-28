using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_1by1 : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject[] Monster;
    public float spawnRateMin;
    public float spawnRateMax;

    private float spawnRate;
    int idx = 0;
    int MAX = 100;
    bool state;

    // Start is called before the first frame update
    void Start()
    {
        Monster = new GameObject[MAX];
        for (int i = 0; i < MAX; i++)
        {
            GameObject ob = Instantiate(Prefab);
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
            Monster[idx].transform.position = gameObject.transform.position;
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
        StartCoroutine("Spawn");
    }
}
