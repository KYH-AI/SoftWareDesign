using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_GN : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);
    int hp = 20;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed.x * x, speed.y * y, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
    }

    public void damage(int n)
    {
        hp -= n;
    }
}
