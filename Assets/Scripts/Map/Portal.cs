using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    /*
    bool inPortal =false;
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
       
            if (Input.GetKey(KeyCode.Space)&& !inPortal)
            {
                inPortal = true;
                print("�������� �̵�!");
                StartCoroutine(ToStore());
            }

        }
        else if (collision.gameObject.tag == "StorePortal")
        {
            if (Input.GetKey(KeyCode.Space)&&!inPortal)
            {
                inPortal = true;
                print("���� ���������� �̵�");
                StartCoroutine(SceneChange());
            }

        }
    }
    IEnumerator ToStore()
    {
        switch (StageManager.GetInstance().stage)
        {
            case Define.Stage.STAGE1:
                StageManager.GetInstance().stage = Define.Stage.STAGE2;
                print("stage2����");
                break;
            case Define.Stage.STAGE2:
                StageManager.GetInstance().stage = Define.Stage.STAGE3;
                print("stage3����");
                break;
            case Define.Stage.STAGE3:
               StageManager.GetInstance().stage = Define.Stage.STAGE4;
                print("stage4����");
                break;
            case Define.Stage.STAGE4:
               StageManager.GetInstance().stage = Define.Stage.STAGE4;
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
    }*/

}


