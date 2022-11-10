using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCamera : MonoBehaviour
{
    private static PlayerCamera instance;
    public static PlayerCamera Instance { get { return instance; } }

    [SerializeField] Transform targetPlayer;
    [SerializeField] Volume mainCameraGlobalVolume;
    private Camera mainCamera;
    private readonly Vector3 CAM_Z_OFFSET = new Vector3(0f, 0f, -10f);


    private float cameraTimeScale = 1.0f;
    private float customCameraDeltaTime;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        customCameraDeltaTime = cameraTimeScale * Time.deltaTime;
    }


    private void LateUpdate()
    {
        TrackingPlayer();
    }

    /// <summary>
    /// 스킬 URP Volume 변경
    /// </summary>
    /// <param name="postProcessProfile">스킬 URP Volume 프로필</param>
    public void ChagnePostProcessProfile(VolumeProfile postProcessProfile) 
    {
        mainCameraGlobalVolume.profile = postProcessProfile;
    }

    public void TrackingPlayer()
    {
        if (targetPlayer != null)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPlayer.position, customCameraDeltaTime * GameManager.Instance.PlayerCameraMoveSpeed);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, CAM_Z_OFFSET.z);
        }
    }

}
