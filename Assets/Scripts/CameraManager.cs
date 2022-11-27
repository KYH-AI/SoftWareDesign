using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Managers
{
    public Camera unscaledCamera;
    public CinemachineVirtualCamera bossCamera;

    #region 시간 멈추는 패시브용 카메라
    public void TurnUnscaledCameraOn()                  //패시브가 발동되어 시간이 멈췄을 때 unscaledCamera를 켜준다.
    {
        unscaledCamera.gameObject.SetActive(true);
    }

    public void TurnUnscaledCameraOff()                 //패시브가 끝나면 unscaledCamera를 끈다.
    {
        unscaledCamera.gameObject.SetActive(false);
    }
    #endregion

    #region 보스 카메라 위치 설정
    public void SetBossCameraPosition(Transform Boss)   //보스 등장시 본인의 Transform을 전달해주어야 함.
    {
        bossCamera.Follow = Boss;
    }

    #endregion
}
