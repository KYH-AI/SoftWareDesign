using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;

public class BossSpawnEffect : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public TimelineAsset timeline;
    public Text bossEffectText;
    public CinemachineVirtualCamera bossCamera;


    private void Update()
    {
        //if (Input.anyKeyDown)
        //    PlayFromTimeline();
    }

    #region 타임라인 함수
    public void Play()
    {
        // 현재 playableDirector에 등록되어 있는 타임라인을 실행
        playableDirector.Play();
    }
    public void PlayFromTimeline()
    {
        TimeLineEndSignal();
        playableDirector.Play(timeline);
    }
    #endregion

    #region 타임라인 시그널 함수
    public void TimeLineStartSignal()
    {
        //플레이어의 움직임을 제한. -> 윤호
        //보스들은 signal이 없어도 그냥 대기상태에 있도록.
        //카메라의 priority는 BossSpawn에서 바꿔줬음.
    }

    public void TimeLineEndSignal()
    {
        Managers.CameraManager.SetPriority(9);
        //중간보스는 얘기해봐야한다.
        //보스의 state를 move로 바꿔주자.
    }
    #endregion

    public void UpdateBossEffectText()      //보스마다 text를 달리하자.
    {
        switch (Managers.StageManager.stage)
        {
            case Define.Stage.STAGE1:
                bossEffectText.text = "첫 번째 보스";
                break;
            case Define.Stage.STAGE2:
                bossEffectText.text = "두 번째 보스";
                break;
            case Define.Stage.STAGE3:
                bossEffectText.text = "세 번째 보스";
                break;
            case Define.Stage.STAGE4:
                bossEffectText.text = "네 번째 보스";
                break;
            case Define.Stage.Boss:
                bossEffectText.text = "최종 보스";
                break;

        }
    }

}
