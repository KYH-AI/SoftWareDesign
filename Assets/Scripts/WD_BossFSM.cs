using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WD_BossFSM
{


    public WD_Boss.BossState bossState;
    private WD_Boss boss;
    
    public WD_BossFSM(WD_Boss boss) 
    {
        this.boss = boss;
    }
    
    // Update is called once per frame
    public void Update()
    {
        switch (bossState)
        {
            case WD_Boss.BossState.MOVE_STATE: 
                boss.Move(); break;
            case WD_Boss.BossState.ATTACK_STATE:
                boss.Attack(); break;               
            case WD_Boss.BossState.HURT_STATE:
                boss.Hurt(); break;
            case WD_Boss.BossState.DEAD_STATE:
                boss.OnDead(); break;
            case WD_Boss.BossState.PATTERN_DARKHEAL_STATE:
                boss.Pattern_DarkHeal(); break;
            case WD_Boss.BossState.PATTERN_RUINSTK_STATE:
                boss.Pattern_RuinStk(); break;
            case WD_Boss.BossState.PATTERN_SUMNSKELETON_STATE:
                boss.Pattern_SummonSkeleton(); break;
            case WD_Boss.BossState.PATTERN_BIND_STATE:
                boss.Pattern_Bind(); break;
        }
      
    }
   
   
}

