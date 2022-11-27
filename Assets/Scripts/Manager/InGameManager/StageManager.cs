using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StageManager : MonoBehaviour
{
    #region ���߿� �������� �κ�
    public static StageManager stageManager;
    #endregion

    public Player Player;
    //[SerializeField] GameObject portalPref;
    GameObject portal;
    public int killCount;      //���� ų ī��Ʈ
    public Define.Stage stage;        //���� ��������

    #region ����Ƽ �Լ�
    private void Awake()            //���߿� ������ ��.
    {
        if (stageManager == null)
        {
            stageManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        SetStageKillCount();
        stage = Define.Stage.STAGE1;
        Debug.Log("StageManager : " + killCount);
    }
    #endregion

    public void DecreaseKillCount()     //ųī��Ʈ�� ���̴� ������� �����Ϸ��� ��. �÷��̾ ���͸� ���̸� ����.
    {
        if (killCount <= 0)
        {
            //MakePortal();
        }
        killCount--;
    }
    public bool IsStageCleared()        //���������� Ŭ���� �Ǿ��°� Ȯ���ϴ� �Լ�.
    {
        if (killCount <= 0)
            return true;
        else
            return false;
    }
    public void SetStageKillCount()     //ų ī��Ʈ�� 100���� �ʱ�ȭ �ϴ� �Լ�.
    {
        killCount = 100;
    }
   /* public void MakePortal()            //��Ż ���� �Լ�.
    {
        portal = Instantiate(portalPref);
        portal.transform.position = Player.transform.position;
    }*/
    public void ChangeStage()                   //�� �̵��� �����ؾ� �ϴ� �Լ�. ���������� 1�� ������Ų��.
    {
        stage++;
    }
    public static StageManager GetInstance()    //���߿� �������� �Լ�.
    {
        return stageManager;
    }
    public int ReturnKillCount()
    {
        return killCount;
    }

    public void KillProcess()
    {
        DecreaseKillCount();
        Managers.UI.UpdateKillCounts();
    }
}
