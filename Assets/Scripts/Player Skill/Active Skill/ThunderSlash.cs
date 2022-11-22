using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// �͸��� Į�� �����ϼ� N���� ���
/// </summary>
public class ThunderSlash : ActiveSkill
{
    private TrailRenderer thunderclapEffect;
    [SerializeField] GameObject thunderSlashLockOnEffect;
    [SerializeField] VolumeProfile thunderClapProfile;

    #region ��ų �⺻ ���� ������
    /// <summary>
    /// ��ų ������
    /// </summary>
    private int skillDamage = 25;
    /// <summary>
    /// ��ų Ÿ�� ��
    /// </summary>
    private int skillTotalTarget = 2;
    #endregion

    /*  ������Ƽ (09/28) ���� ����
    public int SkillDamage { set { skillDamage = value; } }
    public int SkillTotalTarget { set { skillTotalTarget = value; } }
    */

    private void Start()
    {
        ThunderSlashInit();
    }

    private void ThunderSlashInit()
    {
        thunderclapEffect = GetComponentInChildren<TrailRenderer>();
        thunderclapEffect.enabled = false;
    }

    public override void OnActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = ThunderSlashSkillAttack();
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

    private Define.CurrentSkillState ThunderSlashSkillAttack()
    {
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(playerObject.transform.position, 10f, enemyLayer);//��Ʈ�Ͻ� q

        if (enemyCollider.Length > 0)
        {
            OnSkillEffect();
            ThunderSlashEffect(); // ���� ȿ�� ����Ʈ (Trail Renderer) Ȱ��ȭ
            Time.timeScale = 0.1f;
            StartCoroutine(ThunderSlashSkillAttackSkillProcess(enemyCollider));
            return Define.CurrentSkillState.COOL_TIME;
        }
        return Define.CurrentSkillState.ACTIVE;
    }

    IEnumerator ThunderSlashSkillAttackSkillProcess(Collider2D[] enemyColiders)
    {
       GameObject[] lockOnEffect = new GameObject[skillTotalTarget];

        for(int enemyCount = 0; enemyCount < skillTotalTarget; enemyCount++)
        {
            lockOnEffect[enemyCount] = Instantiate(thunderSlashLockOnEffect, enemyColiders[enemyCount].transform.position, Quaternion.identity);
            yield return new WaitForSecondsRealtime(1f);   // ĳ�� ����

        }

        for (int enemyCount = 0; enemyCount < skillTotalTarget; enemyCount++)
        {
           // GameManager.Instance.PlayerCameraMoveSpeed = 0f;
            playerObject.transform.position = enemyColiders[enemyCount].transform.position;

            // TODO : ������ ó�� �ϱ� (10/30)
            enemyColiders[enemyCount].GetComponent<Enemy>().TakeDamage(skillDamage);

             yield return new WaitForSecondsRealtime(0.2f);  // ĳ�� ����
          //  GameManager.Instance.PlayerCameraMoveSpeed = 10f;

          //  yield return new WaitForSecondsRealtime(1f);   // ĳ�� ����
            Destroy(lockOnEffect[enemyCount]);
        }
        OffSkillEffect();
        Time.timeScale = 1.0f;
 
        Invoke(nameof(ThunderSlashEffect), 1f); // ���� ȿ�� ����Ʈ (Trail Renderer) ��Ȱ��ȭ
        OnCoolTime(); // ��ų ��Ÿ�� ����
    }

    private void OnSkillEffect()
    {
        Managers.SkillEffectVolume.ChagnePostProcessProfile(thunderClapProfile);
    }

    private void OffSkillEffect()
    {
        Managers.SkillEffectVolume.ChagnePostProcessProfile(null);
    }

    private void ThunderSlashEffect()
    {
        thunderclapEffect.enabled = !thunderclapEffect.enabled;
    }

}
