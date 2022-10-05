using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WD_BossFSM
{
   public enum BossState
    {
        Move,
        Attack,
        Hurt,
        Dead,
        Patten
    }

    private WD_Boss wdboss;

    public WD_BossFSM(WD_Boss wD_Boss) 
    {
        wdboss = wD_Boss; 
    }

    public BossState b_state;
    public int attackPower = 10;
    public int hp = 5000;
    public int maxHp = 5000;
    //public float moveSpeed = 40;  //추후 Enemy 클래스에서 참조하는 변수명으로 통일 

    // Start is called before the first frame update
    void Start()
    {
        b_state = BossState.Move;
    }
    // Update is called once per frame
    public void Update()
    {
        switch (b_state)
        {
            case BossState.Move: wdboss.Move(); break;
            case BossState.Attack: wdboss.Attack(); break;


                /*
                case BossState.Hurt: Hurt(); break;
                case BossState.Dead: Dead(); break;
                case BossState.Patten: Patten(); break;
                */
        }
    }
   
    public void Hurt()
    {

    }
    public void Dead()
    {

    }
    public void Patten()
    {

    }
}

