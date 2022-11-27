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
    int MAX = 10;

    // Start is called before the first frame update
    void Start()
    {
        Monster = new GameObject[MAX];
        for (int i = 0; i < MAX; i++)
        {
            //GameObject ob = MemoryPoolManager.GetInstance().OutputGameObject(Prefab, Define.PrefabType.Monsters, this.transform.position, Quaternion.identity);
            GameObject ob = Instantiate(Prefab);
            //ob.GetComponent<Enemy>().EnemyInit(Managers.Player);
            ob.GetComponent<Enemy>().EnemyInit(StageManager.GetInstance().Player);
            Monster[i] = ob;
            ob.SetActive(false);
        }
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        yield return new WaitForSeconds(spawnRate);
        /*GameObject ob = MemoryPoolManager.GetInstance().OutputGameObject(Prefab, Define.PrefabType.Monsters, this.transform.position, Quaternion.identity);
        //ob.GetComponent<Enemy>().EnemyInit(Managers.Player);
        ob.GetComponent<Enemy>().EnemyInit(StageManager.GetInstance().Player);
        ob.SetActive(true);*/
     
        Monster[idx].transform.position = gameObject.transform.position;
        Monster[idx].SetActive(true);
        idx++;
        if (idx == MAX)
            idx = 0;
        StartCoroutine("Spawn");
    }
}
