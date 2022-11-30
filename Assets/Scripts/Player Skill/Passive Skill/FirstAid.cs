using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : PassiveSkill
{
    [SerializeField] GameObject firstAidEffect;
    private readonly string firstAidSFX = "Player/Passive Skill/FirstAid";

    #region ��ų �ʱ� ���� ������
    /// <summary>
    /// ��ų �ִ� ü���� ȸ�� �ۼ�Ʈ
    /// </summary>
    private int buffHpRegenPercent = 10;
    /// <summary>
    /// ��ų ���ӽð�
    /// </summary>
    private float skillDuration = 3f;
    /// <summary>
    /// ��ų ������ (���� ��) = �ʴ� ȸ������ ���� 1�� �ð� �ڷ�ƾ
    /// </summary>
    private readonly WaitForSeconds PER_SECONDS = new WaitForSeconds(1f);
    #endregion

    #region ��ų ���� ������Ƽ
    /// <summary>
    /// ��ų �̵��ӵ� ������Ƽ (  set : ������ �̵��ӵ� buffSpeed �� ���� )
    /// </summary>
    public int BuffHpRegenPercent
    {
        set { buffHpRegenPercent = value; }
    }
    #endregion

    private void Start()
    {
        FirstAidInit();
    }

    private void FirstAidInit()
    {
        //skillDurationSec = new WaitForSeconds(skillDuration);
    }

    /// <summary>
    ///  �ش� ��ų �ǰ� �� �ߵ� �ǵ��� �̺�Ʈ ���
    /// </summary>
    public override void OnActive()
    {
        playerObject.HitEvent += FirstAidSkillActive;
    }

    public override void Upgrade()
    {
        skillDuration += 1;
        SkillCoolTime -= 3f;
        buffHpRegenPercent += 5;
    }

    private void FirstAidSkillActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            StartCoroutine(FirstAidSkillProcess(skillDuration, (playerObject.MaxHp * buffHpRegenPercent) / 100));
        }
        else
        {
            return;
        }
    }

    private IEnumerator FirstAidSkillProcess(float buffDuration, int addHP)
    {
        firstAidEffect.SetActive(true);
        while(buffDuration > 0)
        { 
            playerObject.Hp += addHP;
            Managers.Sound.PlaySFXAudio(firstAidSFX);
            buffDuration--;
            yield return PER_SECONDS;
        }

        OnCoolTime();
        firstAidEffect.SetActive(false);
    }
}
