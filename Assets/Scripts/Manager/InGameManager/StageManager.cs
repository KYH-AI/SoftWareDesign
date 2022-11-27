using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StageManager : MonoBehaviour
{
    public Player Player;
    GameObject portal;
    public int killCount;      //현재 킬 카운트
    public Define.Stage stage;        //현재 스테이지
    public GameObject[] midBoss = new GameObject[4];
    public GameObject[] coins = new GameObject[3];

    #region 유니티 함수

    private void Start()
    {
        SetStageKillCount();
        stage = Define.Stage.STAGE1;
    }
    #endregion

    public void DecreaseKillCount()     //킬카운트를 줄이는 방식으로 진행하려고 함. 플레이어가 몬스터를 죽이면 실행.
    {
        if (killCount <= 0)
            return;
        killCount--;
        Managers.UI.UpdateKillCounts();
    }
    public bool IsStageCleared()        //스테이지가 클리어 되었는가 확인하는 함수.
    {
        if (killCount <= 0)
            return true;
        else
            return false;
    }
    public void SetStageKillCount()     //킬 카운트를 100으로 초기화 하는 함수.
    {
        killCount = 100;
    }
   /* public void MakePortal()            //포탈 생성 함수.
    {
        portal = Instantiate(portalPref);
        portal.transform.position = Player.transform.position;
    }*/
    public void ChangeStage()                   //씬 이동시 실행해야 하는 함수. 스테이지를 1씩 증가시킨다.
    {
        stage++;
    }
    public int ReturnKillCount()
    {
        return killCount;
    }

}
