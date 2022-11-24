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
    bool inPortal =false;
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

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
       
            if (Input.GetKey(KeyCode.Space)&& !inPortal)
            {
                inPortal = true;
                print("상점으로 이동!");
                StartCoroutine(ToStore());
            }

        }
        else if (collision.gameObject.tag == "StorePortal")
        {
            if (Input.GetKey(KeyCode.Space)&&!inPortal)
            {
                inPortal = true;
                print("다음 스테이지로 이동");
                StartCoroutine(SceneChange());
            }

        }
    }
    void SpawnPortal()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            myInstance = Instantiate(Portalpref);
            myInstance.transform.position = new Vector2(portal_x, portal_y);
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
        inPortal = false;
        transform.position = new Vector2(0, 0);
        SceneManager.LoadScene("Store");
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1f);
        transform.position = new Vector2(0, 0);
        inPortal = false;
        switch (StageManager.GetInstance().stage)
        {
            case Define.Stage.TWO:
                SceneManager.LoadScene("Stage2");
                break;
            case Define.Stage.THREE:
                SceneManager.LoadScene("Stage3");
                break;
            case Define.Stage.FOUR:
                SceneManager.LoadScene("Stage4");
                break;
            case Define.Stage.FIVE:
                SceneManager.LoadScene("Stage5");
                break;

        }
    }

}


