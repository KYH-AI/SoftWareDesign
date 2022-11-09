using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMove : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }
    void Update()
    {
        gameObject.transform.position = target.position;
    }
}
