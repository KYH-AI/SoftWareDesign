using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFSM
{


    public Boss.BossState bossState;
    private Boss boss;
    
    public BossFSM(Boss boss) 
    {
        this.boss = boss;
    }
    
    // Update is called once per frame
    public void Update()
    {
        switch (bossState)
        {
            case Boss.BossState.MOVE_STATE: 
                boss.Move(); break;
            case Boss.BossState.ATTACK_STATE:
                boss.Attack(); break;               
            case Boss.BossState.HURT_STATE:
                boss.Hurt(); break;
            case Boss.BossState.DEAD_STATE:
                boss.OnDead(); break;
            case Boss.BossState.PATTERN_DARKHEAL_STATE:
                boss.Pattern_DarkHeal(); break;
            case Boss.BossState.PATTERN_RUINSTK_STATE:
                boss.Pattern_RuinStk(); break;
            case Boss.BossState.PATTERN_SUMNSKELETON_STATE:
                boss.Pattern_SummonSkeleton(); break;
            case Boss.BossState.PATTERN_BIND_STATE:
                boss.Pattern_Bind(); break;
        }
      
    }
   
   
}

