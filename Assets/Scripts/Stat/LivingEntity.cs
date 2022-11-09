using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 기본 스텟과 사망처리를 관리하는 클래스
/// </summary>
public abstract class LivingEntity : MonoBehaviour
{
    [SerializeField] BasicStat basicStat; // 기본 스텟

    /* 내부 원본 데이터 */
    private int hp;
    private int maxHp;
    private float moveSpeed;
    private int armor;
    private int defaultAttackDamage;

    /* 버프 이벤트 */
    private UnityEvent buffEvent;
    public UnityEvent BuffEvent { get { return buffEvent; } }


    /// <summary>
    /// 현재체력 프로퍼티  get ( hp ), set ( hp 값 수정 )
    /// </summary>
    public int Hp 
    { 
        get { return hp; } 
        set 
        { 
            hp = value;
        } 
    }

    /// <summary>
    /// 최대체력 프로퍼티  get ( maxHp ), set ( maxHp 값 수정 )
    /// </summary>
    public int MaxHp
    {
        get { return maxHp; }
        set
        {
            maxHp = value;
        }
    }

    /// <summary>
    /// 이동속도 프로퍼티  get ( moveSpeed ), set (  moveSpeed 값 수정)
    /// </summary>
    public float MoveSpeed 
    {

        get { return moveSpeed; }
        set
        {
            moveSpeed = value;
        }
    }
    public float Armor { get; set; }
    public int DefaultAttackDamage { get; set; }

    /// <summary>
    /// 기본 스텟을 BasicStat 클래스로 부터 값을 받아 초기화
    /// (최대체력, 이동속도, 방어력, 기본 공격력)
    /// </summary>
    protected void BasicStatInit()
    {
        hp = basicStat.Hp;
        maxHp = hp;
        moveSpeed = basicStat.MoveSpeed;
        armor = basicStat.Armor;
        defaultAttackDamage = basicStat.DefaultAttackDamage;

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
}
