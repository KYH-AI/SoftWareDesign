using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void Update()
    {
        print(Managers.StageManager.stage);
    }
    bool inPortal =false;
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
       
            if (Input.GetKey(KeyCode.Space)&& !inPortal)
            {
                Managers.UI.killCount.gameObject.SetActive(false);
                inPortal = true;
                print("�������� �̵�!");
                StartCoroutine(ToStore());
                MemoryPoolManager.GetInstance().InitPool();
            }

        }
        else if (collision.gameObject.tag == "StorePortal")
        {
            if (Input.GetKey(KeyCode.Space)&&!inPortal)
            {
                Managers.UI.killCount.gameObject.SetActive(true);
                inPortal = true;
                print("���� ���������� �̵�");
                StartCoroutine(SceneChange());
            }

        }
    }
    IEnumerator ToStore()
    {
        switch (Managers.StageManager.stage)
        {
            case Define.Stage.STAGE1:
                Managers.StageManager.stage = Define.Stage.STAGE2;
                print("stage2����");
                break;
            case Define.Stage.STAGE2:
                Managers.StageManager.stage = Define.Stage.STAGE3;
                print("stage3����");
                break;
            case Define.Stage.STAGE3:
                Managers.StageManager.stage = Define.Stage.STAGE4;
                print("stage4����");
                break;
            case Define.Stage.STAGE4:
                Managers.StageManager.stage = Define.Stage.Boss;
                break;
        }
        yield return new WaitForSeconds(1f);
        inPortal = false;
        transform.position = new Vector2(0, 0);
        SceneManager.LoadScene("JinminStore");
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1f);
        transform.position = new Vector2(0, 0);
        inPortal = false;
        switch (Managers.StageManager.stage)
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
            case Define.Stage.Boss:
                SceneManager.LoadScene("Stage5");
                break;

        }
    }

}


