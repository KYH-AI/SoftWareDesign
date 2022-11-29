using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_1by1 : MonoBehaviour
{
    public GameObject Prefab;
    public float spawnRateMin;
    public float spawnRateMax;

    private float spawnRate;

    public int MaxCnt = 10;
    int cnt = 0;

    void Start()
    {
        MaxCnt = 10;
        StartCoroutine("Spawn");
        cnt = 0;
    }

    IEnumerator Spawn()
    {
        if (cnt < MaxCnt)
        {
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            yield return new WaitForSeconds(spawnRate);
            GameObject ob = MemoryPoolManager.GetInstance().OutputGameObject(Prefab, "Monsters/Stage Monster/" + Prefab.name, this.transform.position, Quaternion.identity);
            //ob.GetComponent<Enemy>().EnemyInit(Managers.Player);
            ob.GetComponent<Enemy>().EnemyInit(Managers.StageManager.Player);
            ob.SetActive(true);
            cnt++;
            StartCoroutine("Spawn");
        }
    }
}
