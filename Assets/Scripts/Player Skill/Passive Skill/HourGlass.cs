using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HourGlass : PassiveSkill
{
    [SerializeField] GameObject hourGlassEffectObject;
    [SerializeField] VolumeProfile hourGlassProfile;

    private Animator hourGlassAnimator;
    public Animator HourGlassAnimator { get { return hourGlassAnimator; } }

    private Coroutine hourGlassCheck = null;
    private WaitForSecondsRealtime skillDuration = new WaitForSecondsRealtime(5f);

    private void Start()
    {
        hourGlassAnimator = hourGlassEffectObject.GetComponent<Animator>();
    }

    public override void OnActive()
    {
        if (hourGlassCheck == null)
        {
            hourGlassCheck = StartCoroutine(BarrierSkillProcess());
        }
    }

    private void HourGlassSkillActive()
    {
        Time.timeScale = 0f; // 시간 정지 
        PlayerCamera.Instance.ChagnePostProcessProfile(hourGlassProfile); // Hour Glass 스킬 포스트 프로세싱 효과 활성화
        hourGlassEffectObject.SetActive(true); // Hour Glass Effect 오브젝트 활성화
    }

    private void HourGlassSkillDisable()
    {
        if (hourGlassCheck != null)
        {
            StopCoroutine(hourGlassCheck);
        }

        hourGlassAnimator.SetTrigger("ComeBack"); // 돌아오는 시계 애니메이션 재생
        hourGlassCheck = null;
    }

    IEnumerator BarrierSkillProcess()
    {
        HourGlassSkillActive();

        yield return skillDuration;

        HourGlassSkillDisable();
    }
}
