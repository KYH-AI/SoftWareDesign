using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowStat : MonoBehaviour
{
    public Text speed;
    public Text sheld;
    public Text hp;
    public Text attack;
    public Text money;

    // Start is called before the first frame update
    void Start()
    {

        //이동속도 , 체력 , 방어력, 공격력 
        speed.text = StageManager.GetInstance().Player.MoveSpeed.ToString();
        sheld.text = StageManager.GetInstance().Player.Armor.ToString();
        hp.text = StageManager.GetInstance().Player.MaxHp.ToString();
        attack.text = StageManager.GetInstance().Player.DefaultAttackDamage.ToString();
        StageManager.GetInstance().Player.PlayerGold = 10000;
        money.text = StageManager.GetInstance().Player.PlayerGold.ToString();

    }
}

    