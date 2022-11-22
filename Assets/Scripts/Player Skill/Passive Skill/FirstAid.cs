using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : PassiveSkill
{
    #region ��ų �ʱ� ���� ������
    /// <summary>
    /// ��ų �ִ� ü���� ȸ�� �ۼ�Ʈ
    /// </summary>
    private int buffHpRegenPercent = 8;
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
    /// <summary>
    /// ��ų ���ӽð� ������Ƽ (  set : ������ ���ӽð� �ڷ�ƾ WaitForSeconds �� ���� )
    /// </summary>
    public float SkillDuration
    {
        set
        {
            skillDuration = value;
        }
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
        /*
         *  ���ӽð� ����
         *  �ӵ� ���� ����
         *  ��Ÿ�� ����
         */
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

        /*  
        // playerController.MoveSpeed *= buffSpeed;

        // ���� ȿ�� ����
        // ��ü�� X �ۼ�Ʈ �� 100 //
        playerController.MoveSpeed = (buffSpeed * buffStat) / 100;
        // ������Ƽ ������ �ƴ� ���̷�Ʈ�� ���� (�Ͻ������� �ٲٴ� ��)
        copyMoveSpeed = buffSpeed; // ���� �ӵ��� buffSpeed �� ����

        // buffDuration ����
        // �ڷ�ƾ 


        // ���� ȿ�� ����
        buffSpeed = moveSpeed;      // ���� ���� buffStat ��ŭ �ٽ� ������ؼ� buffSpeed �ʱ� ������ ����
        copyMoveSpeed = buffSpeed;  // ���� �ӵ��� ������ �ޱ� �� buffSpeed ������ ���� 
        */
    }

    private IEnumerator FirstAidSkillProcess(float buffDuration, int addHP)
    {
        while(buffDuration > 0)
        {
            print("�÷��̾� ü�� ȸ�� �� (�����ð� : " + buffDuration);
            playerObject.Hp += addHP;
            buffDuration--;
            yield return PER_SECONDS;
        }

        OnCoolTime();
    }
}
