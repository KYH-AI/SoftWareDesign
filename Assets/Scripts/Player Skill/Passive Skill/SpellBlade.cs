using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBlade : PassiveSkill
{
    /// <summary>
    /// ��ų ����Ʈ ������Ʈ
    /// </summary>
    [SerializeField] GameObject spellBladeEffect;

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
    /// ��ų ���ӽð� �ڷ�ƾ
    /// </summary>
    private WaitForSeconds skillDurationSec;
    #endregion

    #region ��ų ���� ������Ƽ
    /// <summary>
    /// ���ݷ� ���� �ۼ�Ʈ ���� ������Ƽ (  set : ���� ������ buffDamagePercent �� ���� )
    /// </summary>
    public int BuffDamagePercent
    {
        set 
        {
            buffDamagePercent = value; 
            lastBuffDamage = (playerObject.DefaultAttackDamage * buffDamagePercent) / 100; // ���� �ۼ�Ʈ�� �޶������� �ٽ� ����
        }
    }
    /// <summary>
    /// ��ų ���ӽð� ������Ƽ (  set : ���� ������ ���ӽð� �ڷ�ƾ WaitForSeconds �� ���� )
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

    private Coroutine buffCoroutine;

    private void Start()
    {
        SpellBladeInit();
    }

    private void SpellBladeInit()
    {
        skillDurationSec = new WaitForSeconds(skillDuration);
        lastDefaultAttackDamage = playerObject.DefaultAttackDamage;
        lastBuffDamage = (playerObject.DefaultAttackDamage * buffDamagePercent) / 100;  // ���� ������ �ʱ� ����
    }

    public override void OnActive()
    {
        // TODO : �÷��̾�� Active ��ų�� ���� ������ AddListener �̺�Ʈ ���
        playerObject.OnActiveSkillEvent += SpellBladeActive;
        playerObject.DisableBuffEvent += StopSkillProcess;
    }

    public override void Upgrade()
    {
        buffDamagePercent += 50;
        SkillCoolTime -= 2f;
        skillDuration += 2f;
    }

    private void SpellBladeActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            print("ȣ���");
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            buffCoroutine = StartCoroutine(SpellBladeSkillProcess());
        }
        else
        {
            return;
        }
    }

    private IEnumerator SpellBladeSkillProcess()
    {
   //     print("��ų ���� �۵�");
        // �÷��̾� �⺻���ݷ��� �޶��� ��� �ٽ� ���� ������ ����
        if (playerObject.DefaultAttackDamage != lastDefaultAttackDamage)
        {
            lastBuffDamage = (playerObject.DefaultAttackDamage * buffDamagePercent) / 100;
        }
        spellBladeEffect.SetActive(true);

        playerObject.DefaultAttackDamage += lastBuffDamage;  // ���� ������ ����
        yield return skillDurationSec;
        StopSkillProcess();
    //    print("��ų ���ӽð��� ��� �����");
    }

    /// <summary>
    /// SpellBlade ��ų �ڷ�ƾ ����
    /// </summary>
    private void StopSkillProcess()
    {
        // ��ų�� ����� ���°� �ƴϸ� ����
        if (buffCoroutine == null) return;

        StopCoroutine(buffCoroutine);  // ��ų �ڷ�ƾ �ٷ� ����
        buffCoroutine = null;          // �ڷ�ƾ �ʱ�ȭ
        OnCoolTime();
        playerObject.DefaultAttackDamage -= lastBuffDamage;  // ���� ������ ��ü
    //    print(playerObject.DefaultAttackDamage + " ���� ��ü ������");
    //    print("��ų ���ӽð��� ��� �����");
        spellBladeEffect.SetActive(false);
    }
}


