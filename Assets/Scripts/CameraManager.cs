using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Managers
{
    public Camera bossCamera;

    #region 보스 카메라 설정
    public void SetFollow(Transform Boss)   //보스 등장시 본인의 Transform을 전달해주어야 함.
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
