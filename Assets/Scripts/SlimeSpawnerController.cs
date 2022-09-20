using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnerController : MonoBehaviour
{
    public GameObject SlimePrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 1f;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;
        if (timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;

            GameObject monster = Instantiate(SlimePrefab, transform.position, transform.rotation);
            monster.transform.LookAt(target);
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
