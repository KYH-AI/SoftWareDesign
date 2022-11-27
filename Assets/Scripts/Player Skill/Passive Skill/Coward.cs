using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coward : PassiveSkill
{
    [SerializeField] GameObject cowardEffect;

    #region ��ų �ʱ� ���� ������
    /// <summary>
    /// ��ų �̵��ӵ�
    /// </summary>
    private int buffSpeed = 3;  //�ʱ� ������ 1
    /// <summary>
    /// ��ų ���ӽð�
    /// </summary>
    private float skillDuration = 1.5f; // �ʱ� ������ 1.5f 
    /// <summary>
    /// ��ų ���ӽð� �ڷ�ƾ
    /// </summary>
    private WaitForSeconds skillDurationSec;
    #endregion

    #region ��ų ���� ������Ƽ
    /// <summary>
    /// ��ų �̵��ӵ� ������Ƽ (  set : ������ �̵��ӵ� buffSpeed �� ���� )
    /// </summary>
    public int BuffSpeed
    {
        set { buffSpeed = value; }
    }
    /// <summary>
    /// ��ų ���ӽð� ������Ƽ (  set : ������ ���ӽð� �ڷ�ƾ WaitForSeconds �� ���� )
    /// </summary>
    public float SkillDuration 
    { 
        set 
        { 
            if(skillDuration != value)
            {
                skillDurationSec = new WaitForSeconds(value);
            }
            skillDuration = value; 
        }     
    }
    #endregion

    private void Start()
    {
        CowardInit();
    }

    private void CowardInit()
    {
        skillDurationSec = new WaitForSeconds(skillDuration);
    }

    /// <summary>
    ///  �ش� ��ų �ǰ� �� �ߵ� �ǵ��� �̺�Ʈ ���
    /// </summary>
    public override void OnActive()
    {
        playerObject.HitEvent += CowardSkillActive;
    }

    public override void Upgrade()
    {
        /*
         *  ���ӽð� ����
         *  �ӵ� ���� ����
         *  ��Ÿ�� ����
         */
    }

    private void CowardSkillActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            Debug.Log("������ �нú� �۵�");
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            StartCoroutine(CowardSkillProcess());
        }
        else
        {
            return;
        }
        #region  ���� ȿ���� %�� ��� �Ʒ� �ڵ带 �̿�
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
        #endregion
    }

    private IEnumerator CowardSkillProcess()
    {
        cowardEffect.SetActive(true);
        playerObject.MoveSpeed += buffSpeed; // �ӵ� ���� ����
        yield return skillDurationSec;
        playerObject.MoveSpeed -= buffSpeed; // �ӵ� ���� ��ü
        cowardEffect.SetActive(false);
        OnCoolTime();
    }
}
