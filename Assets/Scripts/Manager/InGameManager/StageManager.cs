using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class StageManager : MonoBehaviour
{
    public Player Player;
    GameObject portal;
    public int killCount;      //현재 킬 카운트
    public Define.Stage stage;        //현재 스테이지
    public GameObject[] coins = new GameObject[3];

    #region 상점 변수
    public GameManagerYJ shopManager;
    #endregion

    #region 몬스터 스포너 변수
    public int monsterCounter;
    public bool isSpawnOkay;
    public bool isBossAlive;
    #endregion

    #region 씬 Fade 연출
    public TextMeshProUGUI mainTitleText;
    public TextMeshProUGUI subTitleText;
    public Animator sceneAnimator;
    public GameObject sceneMovie;
    #endregion

    #region 유니티 함수

    private void Start()
    {
        SetStageKillCount();
        stage = Define.Stage.STAGE1;
        SenecFadeEffect();
        monsterCounter = 0;
        isSpawnOkay = true;
        isBossAlive = true;
}

    private void Update()
    {
        if (Managers.UI.bossSlider.value <= 0f)
            isBossAlive = false;
        else
            isBossAlive = false;
    }

    #endregion
    public void InitMonsterCounter()
    {
        monsterCounter = 0;
    }

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

    public void SenecFadeEffect()
    {
        switch (stage)
        {
            case Define.Stage.STAGE1:
                mainTitleText.text = "제 1장";
                subTitleText.text = "피의 복수";
                break;
            case Define.Stage.STAGE2:
                mainTitleText.text = "제 2장";
                subTitleText.text = "굶주린 아이";
                break;
            case Define.Stage.STAGE3:
                mainTitleText.text = "제 3장";
                subTitleText.text = "약주고 병주고";
                break;
            case Define.Stage.STAGE4:
                mainTitleText.text = "제 4장";
                subTitleText.text = "꺾이지 않는 마음";
                break;
            case Define.Stage.Boss:
                mainTitleText.text = "제 5장";
                subTitleText.text = "시로의 희망";
                break;

            case Define.Stage.STORE1:
                mainTitleText.text = "휴식처";
                subTitleText.text = "푸근한 집";
                break;
            case Define.Stage.STORE2:
                mainTitleText.text = "휴식처";
                subTitleText.text = "행복한 하루";
                break;
            case Define.Stage.STORE3:
                mainTitleText.text = "휴식처";
                subTitleText.text = "가벼운 마음으로";
                break;
            case Define.Stage.STORE4:
                mainTitleText.text = "휴식처";
                subTitleText.text = "마지막 꿈나라";
                break;
        }
        sceneAnimator.SetTrigger("Movie Start");
    }
}
