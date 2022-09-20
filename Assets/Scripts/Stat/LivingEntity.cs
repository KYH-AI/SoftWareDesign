using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 기본 스텟과 사망처리를 관리하는 클래스
/// </summary>
public abstract class LivingEntity : MonoBehaviour
{
    [SerializeField] BasicStat basicStat;

    /* 원본 데이터 */
    private int hp;
    private int maxHp;
    private float moveSpeed;
    private int armor;
    private int defaultAttackDamage;

    /* 제공되는 복사본 데이터 */
    private int copyHp;
    private int copyMaxHp;
    private float copyMoveSpeed;
    private int copyArmor;
    private int copyDefaultAttackDamage;

    /* 일시적으로 변경되는 버프 데이터 */
    private float buffSpeed;
    private int buffDefaultAttackDamage;

    /// <summary>
    /// HP 프로퍼티  get ( 복사본 HP ), set ( 복사복 HP, 원본 HP 값 수정 )
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
    /// MaxHp 프로퍼티  get ( 복사본 MaxHp ), set ( 복사복 MaxHp, 원본 MaxHp 값 수정 )
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
    /// 기본 스텟을 BasicStat 클래스로 부터 불러온다
    /// (최대체력, 이동속도, 방어력, 공격력)
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
    /// ( 추상화 = 구현필수 ) 사망처리 함수
    /// </summary>
    protected abstract void OnDead();
    /*
    {
        // TODO : Enemy는 메모리 풀링 (재정의)

        // TODO : Boss는 게임승리 UI (재정의)

        // TODO : Player는 게임오버 UI (재정의)
    }
    */

    /// <summary>
    /// 체력 감소 함수 (현재 체력 = 받은 데미지 - 방어력)
    /// </summary>
    /// <param name="newDamage">받은 데미지</param>
    public virtual void TakeDamage(int newDamage)
    {
        hp  -= newDamage - armor;  // 간단하게 공격력 - 방어력으로 계산

        if(hp <= 0)
        {
            OnDead();
        }

        // TODO : Boss UI에서 체력 게이지 변경 (재정의)
        // TODO : Player UI에서 체력 게이지 변경 (재정의)
    }

    public virtual void MoveSpeedBufffloat(float buffStat, float buffDuration)
    {
        // 버프 효과 시작
        buffSpeed = buffStat * buffSpeed;

        // 프로퍼티 접근이 아닌 다이렉트로 접근 (일시적으로 바꾸는 값)
        copyMoveSpeed = buffSpeed; // 현재 속도를 buffSpeed 로 변경

        // buffDuration 시작


        // 버브 효과 종료
        buffSpeed = moveSpeed;      // 버프 값을 buffStat 만큼 다시 역계산해서 buffSpeed 초기 값으로 돌림
        copyMoveSpeed = buffSpeed;  // 현재 속도를 버프를 받기 전 buffSpeed 값으로 돌림 


        //skillDelayTime = skillDelayTime * (1 - ((float)value / 100))
    }
    public virtual void DefaultAttackBuff(int buffStat)
    {

    }

}
