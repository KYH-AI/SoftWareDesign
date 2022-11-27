using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���� �Ŀ� ������ ���
/// </summary>
public class WindDash : ActiveSkill
{
    private TrailRenderer windDlashEffect;

    #region ��ų �⺻ ���� ������
    /// <summary>
    /// ��ų ������
    /// </summary>
    private int skillDamgae = 6;
    /// <summary>
    /// ��ų �뽬 �ִ����
    /// </summary>
    private float skillDashDistacne = 5f;
    #endregion

    #region ��ų ���� ������Ƽ
    /// <summary>
    /// ��ų ������ ������Ƽ ( set : ��ų ������ �� ���� )
    /// </summary>
    public int SkillDamgae { set { skillDamgae = value; } }
    /// <summary>
    /// ��ų �뽬 �ִ���� ������Ƽ ( set : ��ų �뽬 �ִ���� �� ���� )
    /// </summary>
    public float SkillDashDistacne { set { skillDashDistacne = value; } }   
    #endregion

    private void Start()
    {
        WindSlashInit();
    }

    private void WindSlashInit()
    {
        windDlashEffect = GetComponentInChildren<TrailRenderer>();
        windDlashEffect.enabled = true;
        windDlashEffect.enabled = false;
    }

    public override void OnActive()
    {
        if (currentSkillState == Define.CurrentSkillState.ACTIVE)
        {
            currentSkillState = Define.CurrentSkillState.COOL_TIME;
            WindSlashSkillAttack();
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

    private void WindSlashSkillAttack()
    {

        playerObject.SwitchPlayerSprite(playerObject.PlayerController.LastDirection, false);
        playerObject.PlayerController.IsSilence = true;
        playerObject.PlayerController.IsMoveable = false;
        OnSkillEffect();  // ����Ʈ Ȱ��ȭ

        Vector3 firstPosition = playerObject.transform.position;
        float maxDistance = skillDashDistacne;

        // x, y ������ ������ �̿��� �ش� �������� 10m ���󰣴�.

        RaycastHit2D hitObject = Physics2D.Raycast(transform.position, playerObject.PlayerController.LastDirection, maxDistance, wallLayer);

        if (hitObject)  // RayCast�� ���� �浹�ߴٴ� �ǹ�
        {
            maxDistance = hitObject.distance;     // �� �浹 ��ġ������ Ray�� ��� ���� �Ÿ� ����
            playerObject.transform.position = hitObject.normal; // �浹�� �� �� ������ �̵�
        }

        else
        {
            playerObject.transform.Translate(playerObject.PlayerController.LastDirection.normalized * maxDistance); // ���������� �� ���� + dashDistance ���� ��ŭ �̵�
        }


        RaycastHit2D[] enemyObjects = Physics2D.RaycastAll(firstPosition, playerObject.PlayerController.LastDirection, maxDistance, enemyLayer);
        /*
        Debug.Log("��ų �浹 �Ÿ� : " + dashDistance);
        Debug.Log("�浹�� �� : " + enemyObject.Length);
        */


        if (enemyObjects.Length > 0)
        {
            for (int enemyCount = 0; enemyCount < enemyObjects.Length; enemyCount++)
            {
                enemyObjects[enemyCount].transform.GetComponent<Enemy>().TakeDamage(skillDamgae);
            }
        }

        playerObject.PlayerController.IsSilence = false;
        playerObject.PlayerController.IsMoveable = true;

        Invoke(nameof(OnSkillEffect), 1.5f); //  ����Ʈ ��Ȱ��ȭ
        OnCoolTime();

    }

    private void OnSkillEffect()
    {
        windDlashEffect.enabled = !windDlashEffect.enabled;
    }
}