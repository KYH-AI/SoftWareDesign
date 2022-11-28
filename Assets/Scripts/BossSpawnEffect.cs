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

    #region Ÿ�Ӷ��� �Լ�
    public void Play()
    {
        // ���� playableDirector�� ��ϵǾ� �ִ� Ÿ�Ӷ����� ����
        playableDirector.Play();
    }
    public void PlayFromTimeline()
    {
        TimeLineEndSignal();
        playableDirector.Play(timeline);
    }
    #endregion

    #region Ÿ�Ӷ��� �ñ׳� �Լ�
    public void TimeLineStartSignal()
    {
        //�÷��̾��� �������� ����. -> ��ȣ
        //�������� signal�� ��� �׳� �����¿� �ֵ���.
        //ī�޶��� priority�� BossSpawn���� �ٲ�����.
    }

    public void TimeLineEndSignal()
    {
        Managers.CameraManager.SetPriority(9);
        //�߰������� ����غ����Ѵ�.
        //������ state�� move�� �ٲ�����.
    }
    #endregion

    public void UpdateBossEffectText()      //�������� text�� �޸�����.
    {
        switch (Managers.StageManager.stage)
        {
            case Define.Stage.STAGE1:
                bossEffectText.text = "ù ��° ����";
                break;
            case Define.Stage.STAGE2:
                bossEffectText.text = "�� ��° ����";
                break;
            case Define.Stage.STAGE3:
                bossEffectText.text = "�� ��° ����";
                break;
            case Define.Stage.STAGE4:
                bossEffectText.text = "�� ��° ����";
                break;
            case Define.Stage.Boss:
                bossEffectText.text = "���� ����";
                break;

        }
    }

}
