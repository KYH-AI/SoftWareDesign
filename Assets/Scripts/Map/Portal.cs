using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject Portalpref;
    GameObject myInstance;
    int x, y;
    float distace=10;
    int portal_x;
    int portal_y;
    private void Start()
    {
       

        x = Random.Range(-8,9);
        y = Random.Range(-8, 9);
        if ( 0 < x || x <= 3) { x = Random.Range(4, 9); }
        if (-3 <= x || x <= 0) { x = Random.Range(-8, -3); }
       
    } 
    // Update is called once per frame
    void Update()
    {
        
        portal_x = (int)transform.position.x + x;
        portal_y = (int)transform.position.y + y;

        if (portal_y > 30) { portal_y = 30; }
        if (portal_y < -32) { portal_y = -32; }


        SpawnPortal();
        /*
        if (myInstance != null)
        {
            distace = Vector2.Distance(myInstance.transform.position, transform.position);
        }

        if (distace < 2)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(ToStore());
            }
        }*/

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(ToStore());
            }
        }
    }
 
    void SpawnPortal()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            for (int i = 0; i < 8; i++)
            {
                myInstance = Instantiate(Portalpref);
                myInstance.transform.position = new Vector2(portal_x, portal_y);
            }
        }

    }
    IEnumerator ToStore()
    {
        switch (StageManager.GetInstance().stage)
        {
            case Define.Stage.ONE:
                StageManager.GetInstance().stage = Define.Stage.TWO;
                print("stage2상태");
                break;
            case Define.Stage.TWO:
                StageManager.GetInstance().stage = Define.Stage.THREE;
                print("stage3상태");
                break;
            case Define.Stage.THREE:
                StageManager.GetInstance().stage = Define.Stage.FOUR;
                print("stage4상태");
                break;
            case Define.Stage.FOUR:
                StageManager.GetInstance().stage = Define.Stage.FIVE;
                break;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Store");
    }

}


