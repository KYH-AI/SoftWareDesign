using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{ 
    protected Player playerObject;

    protected int enemyLayer = 1 << 11;
    protected int wallLayer = 1 << 12;

    /// <summary>
    /// ��ų ����
    /// </summary>
    protected Define.CurrentSkillState currentSkillState;

    #region ��ų ��Ÿ�� ����
    /// <summary>
    /// �ڷ�ƾ WaitForSeconds ����
    /// </summary>
    private WaitForSeconds skillCoolTimeSec; 
    /// <summary>
    /// �ش� ��ų ��Ÿ��
    /// </summary>
   [SerializeField] private float skillCoolTime;
    /// <summary>
    /// ��ų ��Ÿ�� ������Ƽ  ( set : ��ų ��Ÿ�� �ڷ�ƾ WaitForSeconds �� ���� )
    /// </summary>
    protected float SkillCoolTime                
    {
        set 
        {
            if (skillCoolTime != value)  // ���� ��ų ��Ÿ���� ����Ǹ� �ڷ�ƾ WaitForSeconds ���� ����
            { 
                skillCoolTimeSec = new WaitForSeconds(value);
            }
            skillCoolTime = value;
        }
    }
    #endregion

    public void Init(Player playerObject)
    {
        this.playerObject = playerObject;
        skillCoolTimeSec = new WaitForSeconds(skillCoolTime);  // �ʱ� ���� �̿��� �ڷ�ƾ WaitForSeconds ����
        currentSkillState = Define.CurrentSkillState.ACTIVE;
    }

    /// <summary>
    /// ��ų ��Ÿ�� ���� �Լ�
    /// </summary>
    protected void OnCoolTime()
    {
        if (currentSkillState != Define.CurrentSkillState.COOL_TIME) return;
        playerObject.OnActiveSkillEvent?.Invoke();
        StartCoroutine(SkillCoolTimeProcess());
    }

    /// <summary>
    /// ��ų ��Ÿ�� �ڷ�ƾ �Լ�
    /// </summary>
    /// <returns>�ش� ��ų ��Ÿ��</returns>
    private IEnumerator SkillCoolTimeProcess()
    {
        //Debug.Log("�ش� ��ų ��Ÿ�� : " + skillCoolTime);

        yield return skillCoolTimeSec;

        currentSkillState = Define.CurrentSkillState.ACTIVE;
    }

}
