using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject Portalpref;
    GameObject myInstance;
    int x, y;
    float distance = 2;
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
                for (int i = 0; i < 8; i++)
                {
                    myInstance = Instantiate(Portalpref);
                    myInstance.transform.position = new Vector2(portal_x, portal_y);
                }
                
                
            }
        if (myInstance != null)
        {
            distance = Vector2.Distance(myInstance.transform.position, transform.position);
        }
        if (distance < 2)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(SceneChange());
            }
        }

    }
    
    IEnumerator SceneChange()
    {
        switch (StageManager.Instance.stage)
        {
            case StageManager.Stage.one:
                StageManager.Instance.stage = StageManager.Stage.two;
                print("stage2상태");
                break;
            case StageManager.Stage.two:
                StageManager.Instance.stage = StageManager.Stage.three;
                print("stage3상태");
                break;
            case StageManager.Stage.three:
                StageManager.Instance.stage = StageManager.Stage.four;
                print("stage4상태");
                break;
            case StageManager.Stage.four:
                StageManager.Instance.stage = StageManager.Stage.five;
                break;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Store");
    }

}


