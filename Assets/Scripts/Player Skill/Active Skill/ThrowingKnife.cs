using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ N�� ��ô
/// </summary>
public class ThrowingKnife : ActiveSkill
{
    [SerializeField] GameObject knifeObject;

    #region ��ų �⺻ ���� ������
    /// <summary>
    /// ��ų ������
    /// </summary>
    private int skillDamgae = 2;
    /// <summary>
    /// ǥâ ��ȯ ����
    /// </summary>
    private int skillProjectileCount = 50; 
    /// <summary>
    /// ǥâ ��ô�ӵ�
    /// </summary>
    private float skillProjectileSpeed = 10f; // �ʱ� ���� 15
    /// <summary>
    /// ǥâ ��ȯ���� (��� ��)
    /// </summary>
    private readonly WaitForSeconds SkillAttackDelay = new WaitForSeconds(0.25f);
    #endregion

    #region ��ų ���� ������Ƽ
    /// <summary>
    /// ��ų ������ ������Ƽ ( set : ��ų ������ �� ���� )
    /// </summary>
    public int SkillDamgae { set { skillDamgae = value; } }
    /// <summary>
    /// ǥâ ��ô�ӵ� ������Ƽ ( set : ǥâ ��ô�ӵ� �� ���� )
    /// </summary>
    public float SkillProjectileSpeed { set { skillProjectileSpeed = value; } }
    /// <summary>
    /// ǥâ ��ȯ ���� ������Ƽ ( set : ǥâ ��ȯ ���� ���� �� ���� )
    /// </summary>
    public float SkillProjectile { set { SkillProjectile = value; } }
    #endregion

    public override void OnActive()
    {
        if(currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            ThrowingKnifeSkillAttack();
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

    private void ThrowingKnifeSkillAttack()
    {
        StartCoroutine(ThrowingKnifeSkillProcess());
    }

     private IEnumerator ThrowingKnifeSkillProcess()
    {
        for(int projectiletCount = 0; projectiletCount < skillProjectileCount; projectiletCount++)
        {
            CreateProjectile();
            yield return SkillAttackDelay;
        }
        OnCoolTime();
    }


    private void CreateProjectile()
    {
        /*
        GameObject projectile = Instantiate(knifeObject,
                                            playerObject.PlayerController.AttackDir.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)),
                                            Quaternion.identity);

        projectile.transform.rotation =  Quaternion.Euler(0, 0, Mathf.Atan2(-playerObject.PlayerController.LastDir.y, -playerObject.PlayerController.LastDir.x) * Mathf.Rad2Deg);
        //   GunMuzzel.right* Random.Range(2, 4) + Vector3.up * Random.Range(2, 4);
        projectile.GetComponent<Projectile>().ProjectileInit(playerObject.PlayerController.LastDir.normalized, skillDamgae, skillProjectileSpeed);
        */
    }
}
