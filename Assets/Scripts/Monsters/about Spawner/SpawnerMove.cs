using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SpawnerMove : MonoBehaviour
{
    private GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(Define.StringTag.Player.ToString());
    }
    void Update()
    {
        gameObject.transform.position = target.transform.position;
    }
}
