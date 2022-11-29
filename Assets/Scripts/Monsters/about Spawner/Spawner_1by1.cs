using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_1by1 : MonoBehaviour
{
    public GameObject Prefab;
    public float spawnRateMin;
    public float spawnRateMax;

    private float spawnRate;

    int MaxCnt = 10;

    bool spawnerRestart = false;
    void Start()
    {
        Invoke(nameof(dealy), 1f);
    }

    void dealy()
    {
        StartCoroutine("Spawn");
        spawnerRestart = true;
    }

    private void OnEnable()
    {
        if (spawnerRestart == true)
        {
            spawnRateMin = 5.0f;
            spawnRateMax = 7.0f;
            Debug.Log("spawn restart!");
            StartCoroutine("Spawn");
        }
    }

    IEnumerator Spawn()
    {
        if (Managers.StageManager.monsterCounter < MaxCnt)
        {
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            yield return new WaitForSeconds(spawnRate);
            GameObject ob = MemoryPoolManager.GetInstance().OutputGameObject(Prefab, "Monsters/Stage Monster/" + Prefab.name, this.transform.position, Quaternion.identity);
            ob.GetComponent<Enemy>().EnemyInit(Managers.StageManager.Player);
            ob.SetActive(true);
            Managers.StageManager.monsterCounter++;
            StartCoroutine("Spawn");
        }
    }
}
