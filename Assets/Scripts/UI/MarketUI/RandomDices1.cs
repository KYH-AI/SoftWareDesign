using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomDices1 : MonoBehaviour
{
    public Text statResult;
    public Image skill;
    public Sprite[]simage;
    public Button btn;

    int RandomInt;

    public Text money;
    public Text speed;
    public Text sheld;
    public Text hp;
    public Text attack;

    private void Start()
    {
        btn.onClick.AddListener(Dice);

        money.text = Managers.StageManager.Player.PlayerGold.ToString();
        sheld.text = Managers.StageManager.Player.Armor.ToString();
        attack.text = Managers.StageManager.Player.DefaultAttackDamage.ToString();
        speed.text = Managers.StageManager.Player.MoveSpeed.ToString();
        hp.text = Managers.StageManager.Player.MaxHp.ToString();
    }

    public void Dice()


    {
       

        RandomInt = Random.Range(1, 8);
   

        if (Managers.StageManager.Player.PlayerGold >= 150)

        {    Managers.StageManager.Player.PlayerGold -= 150;
            
            if (RandomInt == 1)
            {
                skill.sprite = simage[0];
                statResult.text = "���ݷ� 100 ����!";
                Managers.StageManager.Player.DefaultAttackDamage += 100;
               

            }

            else if (RandomInt == 2)
            {
                skill.sprite = simage[1];
                statResult.text = "���� 100 ����!";
                Managers.StageManager.Player.Armor += 100;
          
            }

            else if (RandomInt == 3)
            {
                skill.sprite = simage[2];
                statResult.text = "�̵��ӵ� 100 ����!";
                Managers.StageManager.Player.MoveSpeed += 100;
               
            }

            else if (RandomInt == 4)
            {
                skill.sprite = simage[3];
                statResult.text = "ü�� 100 ����!";
                Managers.StageManager.Player.MaxHp += 100;
               
            }

            else if (RandomInt == 5)
            {
                

                if (Managers.StageManager.Player.DefaultAttackDamage >= 50)
                {
                    
                    skill.sprite = simage[4];
                    statResult.text = "���ݷ� 50 ����!";
                    Managers.StageManager.Player.DefaultAttackDamage -= 50;
                }
                else //if (Managers.StageManager.Player.DefaultAttackDamage <= 50)
                {
                    
                    skill.sprite = simage[0];
                     statResult.text = "���ݷ� 100 ����!";
                     Managers.StageManager.Player.DefaultAttackDamage += 100;
                }

            }

            else if (RandomInt == 6)
            {
               

                if (Managers.StageManager.Player.Armor >= 50)
                {
                   
                    skill.sprite = simage[5];
                    statResult.text = "���� 50 ����!";
                    Managers.StageManager.Player.Armor -= 50;
                }
                else // if (Managers.StageManager.Player.Armor <= 50)
                {
                    
                    skill.sprite = simage[1];
                     statResult.text = "���� 100 ����!";
                     Managers.StageManager.Player.Armor += 100;
                }
            }

            else if (RandomInt == 7)
            {
               

                if (Managers.StageManager.Player.MoveSpeed >= 50)
                {
                    skill.sprite = simage[6];
                    statResult.text = "�̵��ӵ� 50 ����!";
                    Managers.StageManager.Player.MoveSpeed -= 50;
                }
                else //if (Managers.StageManager.Player.MoveSpeed <= 50)
                {
                   
                     skill.sprite = simage[2];
                     statResult.text = "�̵��ӵ� 100 ����!";
                     Managers.StageManager.Player.MoveSpeed += 100;
                }
            }

            else if (RandomInt == 8)
            {
                skill.sprite = simage[7];


                if (Managers.StageManager.Player.MaxHp >= 50) 
                {
                   
                    skill.sprite = simage[7];
                    statResult.text = "ü�� 50 ����!";
                    Managers.StageManager.Player.MaxHp -= 50;
                }
                else //if (Managers.StageManager.Player.MaxHp <= 50)
                {
                    {
                      
                        skill.sprite = simage[3];
                         statResult.text = "ü�� 100 ����!";
                         Managers.StageManager.Player.MaxHp += 100;

                    }
                }
            }




            money.text = Managers.StageManager.Player.PlayerGold.ToString();
            sheld.text = Managers.StageManager.Player.Armor.ToString();
            attack.text = Managers.StageManager.Player.DefaultAttackDamage.ToString();
            speed.text = Managers.StageManager.Player.MoveSpeed.ToString();
            hp.text = Managers.StageManager.Player.MaxHp.ToString();



        }
        else
        {
            statResult.text = " ���� �����Ͽ� ���� �Ұ����մϴ�. "; }
    }




}



