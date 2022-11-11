using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform followingTarget;
    public float moveSpeed;

    private void Start()
    {
        followingTarget = StageManager.GetInstance().Player.GetComponent<Transform>();
    }

    void LateUpdate()                   /* 카메라 이동은 LateUpdate()에서 처리 */
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = new Vector3(followingTarget.position.x,
                                             followingTarget.transform.position.y,
                                             this.transform.position.z);

        this.transform.position = Vector3.Lerp(this.transform.position,
                                               targetPosition,
                                               moveSpeed * Time.deltaTime);
        
    }
}
