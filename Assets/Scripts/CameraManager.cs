using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Managers
{
    public Camera unscaledCamera;
    public CinemachineVirtualCamera bossCamera;

    #region �ð� ���ߴ� �нú�� ī�޶�
    public void TurnUnscaledCameraOn()                  //�нú갡 �ߵ��Ǿ� �ð��� ������ �� unscaledCamera�� ���ش�.
    {
        unscaledCamera.gameObject.SetActive(true);
    }

    public void TurnUnscaledCameraOff()                 //�нú갡 ������ unscaledCamera�� ����.
    {
        unscaledCamera.gameObject.SetActive(false);
    }
    #endregion

    #region ���� ī�޶� ��ġ ����
    public void SetBossCameraPosition(Transform Boss)   //���� ����� ������ Transform�� �������־�� ��.
    {
        bossCamera.Follow = Boss;
    }

    #endregion
}
