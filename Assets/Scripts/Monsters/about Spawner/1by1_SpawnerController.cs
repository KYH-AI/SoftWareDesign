using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject[] Monster;
    public float spawnRateMin;
    public float spawnRateMax;

    private float spawnRate;
    int idx = 0;
    int MAX = 100;

    // Start is called before the first frame update
    void Start()
    {
        Monster = new GameObject[MAX];
        for(int i=0; i<MAX; i++)
        {
            GameObject ob = Instantiate(Prefab);
            Monster[i] = ob;
            ob.SetActive(false);
        }
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        yield return new WaitForSeconds(spawnRate);
        Monster[idx].transform.position = gameObject.transform.position+Vector3.right*20;
        Monster[idx++].SetActive(true);
        Monster[idx].transform.position = gameObject.transform.position + Vector3.left * 20;
        Monster[idx++].SetActive(true);
        if (idx == MAX) idx = 0;
        StartCoroutine("Spawn");
    }

}
