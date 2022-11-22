using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController_ : MonoBehaviour
{
    #region 이동 관련 변수 선언부
    Vector3 moveDirection;                  //이동방향
    Vector2 lastDirection;                  //마지막 이동방향
    public Vector2 LastDirection { get { return lastDirection; } }
    #endregion

    #region 애니메이션 관련 변수 선언부
    Animator anim;                          
    BoxCollider2D boxCol;                   //콜라이더의 크기를 애니메이션에 맞게 조절하기 위해 사용
    #endregion

    #region 상태 제어 변수 선언부
    bool isMoveable = true;
    #endregion

    #region 플레이어 정보 변수 선언부
    Player player;
    #endregion

    #region 유니티 함수
    public void PlayerControllerInit(Player player)
    {
        this.player = player;
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if(isMoveable == true)              //기본 공격시 이동을 막기 위함.
            Move();
    }
    #endregion

    #region 이동 구현부
    public void OnMove(InputValue value)    //new input system 사용시 키 입력 받는 부분. 방향 키 입력을 받으면 OnMove()가 실행된다.
    {
        Vector2 input = value.Get<Vector2>();
        moveDirection = new Vector3(input.x, input.y, 0f);
    }

    private void Move()                     //Move 기능 구현부.
    {
        bool hasControl = (moveDirection != Vector3.zero);
        if (hasControl == true)
        {
    
            anim.SetBool("isMove", true);
            anim.SetFloat("X", moveDirection.x);
            anim.SetFloat("Y", moveDirection.y);

            transform.position += moveDirection * player.MoveSpeed * Time.deltaTime;

            lastDirection = moveDirection;  //마지막 보고있는 방향 저장
        }
        else
        {
            anim.SetBool("isMove", false);
        }
    }
    #endregion

    #region 공격부
    /// <summary>
    /// 스킬이 없거나 쿨타임일땐 return 구문 필요.
    /// </summary>
    /// 
    void OnSkill1()
    {
        //첫번째 스킬 사용
        player.playerActiveSkills[0].OnActive();
    }
    void OnSkill2()
    {
        //2번째 스킬 사용
        player.playerActiveSkills[1].OnActive();
    }
    void OnSkill3()
    {
        //3번째 스킬 사용
        player.playerActiveSkills[2].OnActive();
    }
    void OnSkill4()
    {
        //4번째 스킬 사용
        player.playerPassiveSkills[0].OnActive();
    }
    void OnSkill5()
    {
        //5번째 스킬 사용
        player.playerActiveSkills[4].OnActive();
    }
    void OnAttack()
    {
        //기본공격 사용
        isMoveable = false;         //공격을 시작했을 때 움직임을 제한.
        anim.SetTrigger("BasicAttack");
    }
    #endregion

    #region 애니메이션 이벤트 함수
    void SetIsMoveableTrue()        //공격이 끝났을 때 움직이게 할 수 있도록 하는 애니메이션 이벤트 함수.
        => isMoveable = true;
    void SetColliderEnabled()       //플레이어가 기본 공격시 콜라이더를 켜는 애니메이션 이벤트 함수.
        => boxCol.enabled = true;
    void SetColliderDisabled()      //플레이어가 기본 공격시 콜라이더를 끄는 애니메이션 이벤트 함수.
    {
        boxCol.enabled = false;
        player.DisableBuffEvent?.Invoke();
    }
    #endregion
}
