using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorePortal : MonoBehaviour
{
    /*[SerializeField] GameObject player;
    float distance;

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < 1)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(SceneChange());
            }
        }
    }*/
    bool inPortal = false;
  
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("다음 스테이지로 이동");
                StartCoroutine(SceneChange());
            }

        }
    }


    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1f);
       // inPortal = false;
        switch (StageManager.GetInstance().stage)
        {
            case Define.Stage.STAGE2:
                SceneManager.LoadScene("Stage2");
                break;
            case Define.Stage.STAGE3:
                SceneManager.LoadScene("Stage3");
                break;
            case Define.Stage.STAGE4:
                SceneManager.LoadScene("Stage4");
                break;
            case Define.Stage.STAGE1:
                SceneManager.LoadScene("Stage5");
                break;

        }
    }
}
