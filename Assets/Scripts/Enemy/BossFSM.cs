using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossFSM
{
    private Boss boss;
    public Boss Boss { set { boss = value; } }

    public enum BossState
    {
        Move = 0,
        DefaultAttack = 1,
        SkillAttack = 2,
    }


    private BossState currentState;
    public BossState CurrentState { set { currentState = value; }}


    public BossFSM (Boss boss, BossState defaultState)
    {
        this.boss = boss;
        currentState = defaultState;
    }
    
    public void StateUpdate()
    {
        switch(currentState)
        {
            case BossState.Move: boss.Move(); break;
            case BossState.DefaultAttack: boss.DefaultAttack(); break;
            case BossState.SkillAttack: boss.SkillAttack(); break;
        }
    }

}
