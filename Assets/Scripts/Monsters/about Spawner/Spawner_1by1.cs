using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_1by1 : MonoBehaviour
{
    public GameObject Prefab;
    public float spawnRateMin;
    public float spawnRateMax;

    private float spawnRate;

    void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        yield return new WaitForSeconds(spawnRate);
        GameObject ob = MemoryPoolManager.GetInstance().OutputGameObject(Prefab, "Monsters/Stage Monster/"+Prefab.name, this.transform.position, Quaternion.identity);
        //ob.GetComponent<Enemy>().EnemyInit(Managers.Player);
        ob.GetComponent<Enemy>().EnemyInit(StageManager.GetInstance().Player);
        ob.SetActive(true);
        StartCoroutine("Spawn");
    }
}
