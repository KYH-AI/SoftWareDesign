using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneManager_ : MonoBehaviour
{
    public void LoadScene()                 //���� �� �̵� �Լ�
    {
        Debug.Log("GoToNextScene ����");
        //���� ���� �޾ƿͼ� Define.stage�� �̸��� ������ Define.stage++�ϰ� �� �̸����� �� �̵�

        if (Managers.StageManager.stage.ToString().StartsWith("STAGE"))
        {
            Debug.Log("if ����");
            Managers.StageManager.ChangeStage();
            SceneManager.LoadScene("Store");
        }

        else if (Managers.StageManager.stage.ToString().StartsWith("STORE"))
        {
            Debug.Log("else if ����");
            Managers.StageManager.ChangeStage();
            SceneManager.LoadScene(Managers.StageManager.stage.ToString());
        }
        else
        {
            Debug.Log("else ����");
        }
        
    }


}
