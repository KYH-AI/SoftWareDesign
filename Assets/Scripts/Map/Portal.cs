using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject Portalpref;
    GameObject myInstance;
    int x, y;


    private void Start()
    {
       

        x = Random.Range(-8,9);
        y = Random.Range(-8, 9);
        if ( 0 < x || x <= 3) { x = Random.Range(4, 9); }
        if (-3 <= x || x <= 0) { x = Random.Range(-8, -3); }
        if (0 < y || y <= 3) { y = Random.Range(4, 9); }
        if (-3 <= y || y <= 0) { y = Random.Range(-8, -3); }
        print(x);
        print(y);
    } 
    // Update is called once per frame
    void Update()
    {
        int portal_x = (int)transform.position.x + x;
        int portal_y = (int)transform.position.y + y;

        if (portal_y > 30) { portal_y = 30; }
        if (portal_y < -32) { portal_y = -32; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i=0; i < 8; i++)
            {
                myInstance = Instantiate(Portalpref);
                myInstance.transform.position = new Vector2(portal_x, portal_y);
            }
            
        }
        float distance = Vector2.Distance(myInstance.transform.position, transform.position);
        if (distance < 1)
        {
            StartCoroutine(SceneChange());
        }
    


    }
    
    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1f);

    }

}


