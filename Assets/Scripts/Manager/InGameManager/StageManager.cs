using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StageManager : MonoBehaviour
{
    public Player Player;
    GameObject portal;
    public int killCount;      //���� ų ī��Ʈ
    public Define.Stage stage;        //���� ��������
    public GameObject[] midBoss = new GameObject[4];
    public GameObject[] coins = new GameObject[3];

    #region ����Ƽ �Լ�

    private void Start()
    {
        SetStageKillCount();
        stage = Define.Stage.STAGE1;
    }
    #endregion

    public void DecreaseKillCount()     //ųī��Ʈ�� ���̴� ������� �����Ϸ��� ��. �÷��̾ ���͸� ���̸� ����.
    {
        if (killCount <= 0)
            return;
        killCount--;
        Managers.UI.UpdateKillCounts();
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
    public int ReturnKillCount()
    {
        return killCount;
    }

}
