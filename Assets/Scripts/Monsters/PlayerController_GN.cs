using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_GN : MonoBehaviour
{
    public float speed=30f;
    int hp = 20;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed * x, speed * y, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
    }

    public void damage(int n)
    {
        hp -= n;
    }
}
