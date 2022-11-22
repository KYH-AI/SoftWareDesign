using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{ 
    protected Player playerObject;

    protected int enemyLayer = 1 << 11;
    protected int wallLayer = 1 << 12;

    /// <summary>
    /// 스킬 상태
    /// </summary>
    protected Define.CurrentSkillState currentSkillState;

    #region 스킬 쿨타임 변수
    /// <summary>
    /// 코루틴 WaitForSeconds 변수
    /// </summary>
    private WaitForSeconds skillCoolTimeSec; 
    /// <summary>
    /// 해당 스킬 쿨타임
    /// </summary>
   [SerializeField] private float skillCoolTime;
    /// <summary>
    /// 스킬 쿨타임 프로퍼티  ( set : 스킬 쿨타임 코루틴 WaitForSeconds 값 변경 )
    /// </summary>
    protected float SkillCoolTime                
    {
        set 
        {
            if (skillCoolTime != value)  // 기존 스킬 쿨타임이 변경되면 코루틴 WaitForSeconds 값도 변경
            { 
                skillCoolTimeSec = new WaitForSeconds(value);
            }
            skillCoolTime = value;
        }
    }
    #endregion

    public void Init(Player playerObject)
    {
        this.playerObject = playerObject;
        skillCoolTimeSec = new WaitForSeconds(skillCoolTime);  // 초기 값을 이용해 코루틴 WaitForSeconds 생성
        currentSkillState = Define.CurrentSkillState.ACTIVE;
    }

    /// <summary>
    /// 스킬 쿨타임 실행 함수
    /// </summary>
    protected void OnCoolTime()
    {
        if (currentSkillState != Define.CurrentSkillState.COOL_TIME) return;
        playerObject.OnActiveSkillEvent?.Invoke();
        StartCoroutine(SkillCoolTimeProcess());
    }

    /// <summary>
    /// 스킬 쿨타임 코루틴 함수
    /// </summary>
    /// <returns>해당 스킬 쿨타임</returns>
    private IEnumerator SkillCoolTimeProcess()
    {
        //Debug.Log("해당 스킬 쿨타임 : " + skillCoolTime);

        yield return skillCoolTimeSec;

        currentSkillState = Define.CurrentSkillState.ACTIVE;
    }

}
