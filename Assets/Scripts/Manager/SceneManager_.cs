using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneManager_ : MonoBehaviour
{
    public void LoadScene()                 //다음 씬 이동 함수
    {
        Debug.Log("GoToNextScene 접속");
        //현재 씬을 받아와서 Define.stage와 이름이 같으면 Define.stage++하고 그 이름으로 씬 이동

        if (Managers.StageManager.stage.ToString().StartsWith("STAGE"))
        {
            Debug.Log("if 접속");
            Managers.StageManager.ChangeStage();
            SceneManager.LoadScene("Store");
        }

        else if (Managers.StageManager.stage.ToString().StartsWith("STORE"))
        {
            Debug.Log("else if 접속");
            Managers.StageManager.ChangeStage();
            SceneManager.LoadScene(Managers.StageManager.stage.ToString());
        }
        else
        {
            Debug.Log("else 접속");
        }
        
    }


}
