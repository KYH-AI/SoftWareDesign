using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMove : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
       target = StageManager.GetInstance().Player.GetComponent<Transform>();    
    }
    void Update()
    {
        gameObject.transform.position = target.position;
    }
}
