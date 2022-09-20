using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �⺻ ���ݰ� ���ó���� �����ϴ� Ŭ����
/// </summary>
public abstract class LivingEntity : MonoBehaviour
{
    [SerializeField] BasicStat basicStat;

    /* ���� ������ */
    private int hp;
    private int maxHp;
    private float moveSpeed;
    private int armor;
    private int defaultAttackDamage;

    /* �����Ǵ� ���纻 ������ */
    private int copyHp;
    private int copyMaxHp;
    private float copyMoveSpeed;
    private int copyArmor;
    private int copyDefaultAttackDamage;

    /* �Ͻ������� ����Ǵ� ���� ������ */
    private float buffSpeed;
    private int buffDefaultAttackDamage;

    /// <summary>
    /// HP ������Ƽ  get ( ���纻 HP ), set ( ���纹 HP, ���� HP �� ���� )
    /// </summary>
    public int Hp 
    { 
        get { return copyHp; } 
        set 
        { 
            copyHp = value;
            hp = copyHp;
        } 
    }

    /// <summary>
    /// 
    /// MaxHp ������Ƽ  get ( ���纻 MaxHp ), set ( ���纹 MaxHp, ���� MaxHp �� ���� )
    /// </summary>
    public int MaxHp
    {
        get { return copyMaxHp; }
        set
        {
            copyMaxHp = value;
            maxHp = copyMaxHp;
        }
    }
    public float MoveSpeed 
    {
        get { return copyMoveSpeed; }
        set
        {
            copyMoveSpeed = value;
            moveSpeed = copyMoveSpeed;
            buffSpeed = copyMoveSpeed;
        }
    }
    public float Armor { get; set; }
    public int DefaultAttackDamage { get; set; }

    /// <summary>
    /// �⺻ ������ BasicStat Ŭ������ ���� �ҷ��´�
    /// (�ִ�ü��, �̵��ӵ�, ����, ���ݷ�)
    /// </summary>
    protected void Init()
    {
        hp = basicStat.Hp;
        maxHp = hp;
        moveSpeed = basicStat.MoveSpeed;
        armor = basicStat.Armor;
        defaultAttackDamage = basicStat.DefaultAttackDamage;


        copyHp = hp;
        copyMaxHp = maxHp;
        copyMoveSpeed = moveSpeed;
        copyArmor = armor;
        copyDefaultAttackDamage = defaultAttackDamage;

        buffSpeed = copyMoveSpeed;
    }

    /// <summary>
    /// ( �߻�ȭ = �����ʼ� ) ���ó�� �Լ�
    /// </summary>
    protected abstract void OnDead();
    /*
    {
        // TODO : Enemy�� �޸� Ǯ�� (������)

        // TODO : Boss�� ���ӽ¸� UI (������)

        // TODO : Player�� ���ӿ��� UI (������)
    }
    */

    /// <summary>
    /// ü�� ���� �Լ� (���� ü�� = ���� ������ - ����)
    /// </summary>
    /// <param name="newDamage">���� ������</param>
    public virtual void TakeDamage(int newDamage)
    {
        hp  -= newDamage - armor;  // �����ϰ� ���ݷ� - �������� ���

        if(hp <= 0)
        {
            OnDead();
        }

        // TODO : Boss UI���� ü�� ������ ���� (������)
        // TODO : Player UI���� ü�� ������ ���� (������)
    }

    public virtual void MoveSpeedBufffloat(float buffStat, float buffDuration)
    {
        // ���� ȿ�� ����
        buffSpeed = buffStat * buffSpeed;

        // ������Ƽ ������ �ƴ� ���̷�Ʈ�� ���� (�Ͻ������� �ٲٴ� ��)
        copyMoveSpeed = buffSpeed; // ���� �ӵ��� buffSpeed �� ����

        // buffDuration ����


        // ���� ȿ�� ����
        buffSpeed = moveSpeed;      // ���� ���� buffStat ��ŭ �ٽ� ������ؼ� buffSpeed �ʱ� ������ ����
        copyMoveSpeed = buffSpeed;  // ���� �ӵ��� ������ �ޱ� �� buffSpeed ������ ���� 


        //skillDelayTime = skillDelayTime * (1 - ((float)value / 100))
    }
    public virtual void DefaultAttackBuff(int buffStat)
    {

    }

}
