using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Managers
{
    public Camera bossCamera;

    #region ���� ī�޶� ����
    public void SetFollow(Transform Boss)   //���� ����� ������ Transform�� �������־�� ��.
    {
        //bossCamera.Follow = Boss;
        print("SetFollow");
    }

    public void SetPriority(int priority)
    {
        //bossCamera.Priority = priority;
        print("SetPriority");
    }
    #endregion
}
