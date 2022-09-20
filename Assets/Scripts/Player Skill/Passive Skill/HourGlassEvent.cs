using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourGlassEvent : MonoBehaviour
{
    private void OnCompleteEvent() // HourGlass Anim 에서 이벤트 호출
    {
        Time.timeScale = 1f;  // 시간 정지 해체
        PlayerCamera.Instance.ChagnePostProcessProfile(null); // Hour Glass 스킬 포스트 프로세싱 효과 해체
        gameObject.SetActive(false); // 스스로 비활성화
    }
}
