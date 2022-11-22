using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class FlameStrike : ActiveSkill
{
    [SerializeField] GameObject firePillarObject;
    [SerializeField] VolumeProfile firePillarProfile;

    #region ��ų �⺻ ���� ������
    /// <summary>
    /// ��ų ������
    /// </summary>
    private int skillDamgae = 60;
    /// <summary>
    /// �ұ�� ��ų ��ȯ ����
    /// </summary>
    private int skillProjectileCount = 1; // �ʱ� �� 1��
    /// <summary>
    /// �ұ�� ��ȯ ���ݽð�
    /// </summary>
    private float skillAttackDelay = 0.0f; // �ʱ� �� 0��
    /// <summary>
    /// �ұ�� ��ȯ���� �ð� �ڷ�ƾ  
    /// </summary>
    private WaitForSeconds skillAttackDelayTimeSec;
    #endregion

    #region ��ų ���� ������Ƽ
    /// <summary>
    /// ��ų ������ ������Ƽ ( set : ��ų ������ �� ���� )
    /// </summary>
    public int SkillDamgae { set { skillDamgae = value; } }
    /// <summary>
    /// �ұ�� ��ų ��ȯ ���� ������Ƽ ( set : �ұ�� ��ų ��ȯ ���� �� ���� )
    /// </summary>
    public float SkillProjectile { set { SkillProjectile = value; } }
    /// <summary>
    /// �ұ�� ��ų ��ȯ���� �ð� ������Ƽ ( set : �ұ�� ��ȯ���� �ð� �ڷ�ƾ WaitForSeconds �� ���� )
    /// </summary>
    public float SkillAttackDelay
    {
        set
        {
            if(skillAttackDelay != value)
            {
                skillAttackDelayTimeSec = new WaitForSeconds(value);
            }
            skillAttackDelay = value;
        }
    }
    #endregion

    private void Start()
    {
        FlameStrikeInit();
    }

    private void FlameStrikeInit()
    {
        skillAttackDelayTimeSec = new WaitForSeconds(skillAttackDelay);
    }

    public override void OnActive()
    {

        Debug.Log("���� ��ų���´� : " + currentSkillState);
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            FlameStrikeSkillAttack();
        }
        else
        {
            // TODO : UI���� "���� ���� ���ð� �Դϴ�." �����ϱ�
            return;
        }
    }

    public override void Upgrade()
    {
        // TODO : �������� ���׷��̵� ����� �������� ���� ���� (09/28)
    }

    private void FlameStrikeSkillAttack()
    {
        // TODO (09/29) : �ұ�� ��ȯ��� �������� ����
        StartCoroutine(FlameStrikeSkillProcess());
    }
    private Vector2 RandomSpawnLocation (float xPos = 0f, float yPos = 0f)
    {
        return new Vector2(transform.position.x + xPos, transform.position.y + yPos);
    }

    IEnumerator FlameStrikeSkillProcess()
    {
        OnSkillEffect();

        for (int i = 0; i < skillProjectileCount; i++)
        { 
            GameObject projectile = MemoryPoolManager.GetInstance().OutputGameObject(firePillarObject, 
                                                                                     Define.PrefabType.Player_Skill,
                                                                                     new Vector2(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f)),
                                                                                     Quaternion.identity); 
            projectile.GetComponent<Projectile>().ProjectileInit(Define.StringTag.Enemy, Vector2.zero, skillDamgae);
            projectile.SetActive(true);
            yield return skillAttackDelayTimeSec; // �ұ�� ��ȯ���� �ð�
        }

       Invoke(nameof(OffSkillEffect), 2f); //  2�ʵ� ����Ʈ ��Ȱ��ȭ ;
        OnCoolTime();
    }

    private void OnSkillEffect()
    {
        Managers.SkillEffectVolume.ChagnePostProcessProfile(firePillarProfile);
    }

    private void OffSkillEffect()
    {
        Managers.SkillEffectVolume.ChagnePostProcessProfile(null);
    }
}
