using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAction : MonoBehaviour
{
    float h;
    float v;
    public float movespeed;
    public GameObject right;
    public GameObject left;
    int r= 0 ;
    int l = 0;
    private void Start()
    {
        GameObject rightPre = Instantiate(right);
        rightPre.transform.position = new Vector2(38.1f, -41.2f);

        
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(h, v) * Time.deltaTime * movespeed);

        if(transform.position.x > r)
        {
            GameObject rightPre = Instantiate(right);
            rightPre.transform.position = new Vector2(r+38.1f, -41.2f);
            r += 20;
        }
        if (transform.position.x < l)
        {
            GameObject leftPre = Instantiate(left);
            leftPre.transform.position = new Vector2(l-16.6f, 14);
            l -= 20;
        }




    }


}
