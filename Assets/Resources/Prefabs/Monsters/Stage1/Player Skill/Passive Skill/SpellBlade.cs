using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBlade : PassiveSkill
{
    #region ��ų �ʱ� ���� ������
    /// <summary>
    /// ��ų �⺻ ���ݷ� ���� �ۼ�Ʈ
    /// </summary>
    private int buffDamagePercent = 50;
    /// <summary>
    /// ��ų ���� ���� �� ������ ������
    /// </summary>
    private int lastBuffDamage;
    /// <summary>
    /// �÷��̾� ������ �⺻ ���ݷ�
    /// </summary>
    private int lastDefaultAttackDamage;
    /// <summary>
    /// ��ų ���ӽð�
    /// </summary>
    private float skillDuration = 5f;
    /// <summary>
    /// ��ų ������ (���� ��) = �ʴ� ȸ������ ���� 1�� �ð� �ڷ�ƾ
    /// </summary>
    private WaitForSeconds skillDurationSec;
    #endregion

    #region ��ų ���� ������Ƽ
    /// <summary>
    /// ��ų �̵��ӵ� ������Ƽ (  set : ������ �̵��ӵ� buffSpeed �� ���� )
    /// </summary>
    public int BuffDamagePercent
    {
        set 
        {
            buffDamagePercent = value; 
            lastBuffDamage = (playerObject.DefaultAttackDamage * buffDamagePercent) / 100;
        }
    }
    /// <summary>
    /// ��ų ���ӽð� ������Ƽ (  set : ������ ���ӽð� �ڷ�ƾ WaitForSeconds �� ���� )
    /// </summary>
    public float SkillDuration
    {
        set
        {
            if (skillDuration != value)
            {
                skillDurationSec = new WaitForSeconds(value);
            }
            skillDuration = value;
        }
    }
    #endregion

    private void Start()
    {
        SpellBladeInit();
    }

    private void SpellBladeInit()
    {
        skillDurationSec = new WaitForSeconds(skillDuration);
        lastDefaultAttackDamage = playerObject.DefaultAttackDamage;
        lastBuffDamage = (playerObject.DefaultAttackDamage * buffDamagePercent) / 100;
    }

    public override void OnActive()
    {
        // TODO : �÷��̾�� Active ��ų�� ���� ������ AddListener �̺�Ʈ ���
        playerObject.BuffEvent.AddListener(SpellBladeActive);
    }

    public override void Upgrade()
    {
        
    }

    private void SpellBladeActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            StartCoroutine(SpellBladeSkillProcess());
        }
        else
        {
            return;
        }
    }

    private IEnumerator SpellBladeSkillProcess()
    {
        if(playerObject.DefaultAttackDamage != lastDefaultAttackDamage)
        {
            lastBuffDamage = (playerObject.DefaultAttackDamage * buffDamagePercent) / 100;
        }

        playerObject.DefaultAttackDamage = lastBuffDamage;
        yield return skillDurationSec;
        StopSkillProcess();
    }

    /// <summary>
    /// SpellBlade ��ų �ڷ�ƾ ����
    /// </summary>
    public void StopSkillProcess()
    {
        OnCoolTime();
        StopCoroutine(SpellBladeSkillProcess());
    }
}


