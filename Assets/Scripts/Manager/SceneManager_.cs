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

        if (StageManager.stageManager.stage.ToString().StartsWith("STAGE"))
        {
            Debug.Log("if ����");
            StageManager.stageManager.ChangeStage();
            SceneManager.LoadScene("Store");
        }

        else if (StageManager.stageManager.stage.ToString().StartsWith("STORE"))
        {
            Debug.Log("else if ����");
            StageManager.stageManager.ChangeStage();
            SceneManager.LoadScene(StageManager.stageManager.stage.ToString());
        }
        else
        {
            Debug.Log("else ����");
        }
        
    }


}
