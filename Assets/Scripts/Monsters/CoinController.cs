using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    Transform target;
    float obtainDistance=2.0f;
    float rotateSpeed=40f;
    public int value;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    void Update()
    {
        float spendtime = 0f;
        spendtime+=Time.deltaTime;
        transform.Rotate(0, spendtime * rotateSpeed,0, Space.Self);
        if (Vector2.Distance(target.position, transform.position) <obtainDistance )
        {
            Destroy(gameObject);
        }
    }
}
