using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomDices : MonoBehaviour
{
    public Text statResult;
    public Image skill;
    public Sprite[]simage;
    int RandomInt;
    public Text money;


    // Start is called before the first frame update
    void Start()


    // Player.Money =-150;

    {
        RandomInt = Random.Range(1, 8);
        money.text = StageManager.GetInstance().Player.PlayerGold.ToString();

        if (StageManager.GetInstance().Player.PlayerGold >= 150)
            StageManager.GetInstance().Player.PlayerGold -= 150;
        {



            if (RandomInt == 1)
            {
                skill.sprite = simage[0];
                statResult.text = "���ݷ� 100 ����!";
                //���� +=100; 
                StageManager.GetInstance().Player.DefaultAttackDamage += 100;

            }

            else if (RandomInt == 2)
            {
                skill.sprite = simage[1];
                statResult.text = "���� 100 ����!";
                StageManager.GetInstance().Player.Armor += 100;
            }

            else if (RandomInt == 3)
            {
                skill.sprite = simage[2];
                statResult.text = "�̵��ӵ� 100 ����!";
                StageManager.GetInstance().Player.MoveSpeed += 100;
            }

            else if (RandomInt == 4)
            {
                skill.sprite = simage[3];
                statResult.text = "ü�� 100 ����!";
                StageManager.GetInstance().Player.MaxHp += 100;
            }

            else if (RandomInt == 5)
            {

                if (StageManager.GetInstance().Player.DefaultAttackDamage >= 50)
                {
                    StageManager.GetInstance().Player.DefaultAttackDamage -= 50;
                    skill.sprite = simage[4];
                    statResult.text = "���ݷ� 50 ����!";
                }
                else
                    statResult.text = "���� �Ұ����մϴ�. ";
            }

            else if (RandomInt == 6)
            {

                if (StageManager.GetInstance().Player.Armor >= 50)
                {
                    StageManager.GetInstance().Player.Armor -= 50;
                    skill.sprite = simage[5];
                    statResult.text = "���� 50 ����!";
                }
                else
                    statResult.text = "���� �Ұ����մϴ�. ";
            }

            else if (RandomInt == 7)
            {

                if (StageManager.GetInstance().Player.MoveSpeed >= 50)
                { skill.sprite = simage[6];
                statResult.text = "�̵��ӵ� 50 ����!";
                StageManager.GetInstance().Player.MoveSpeed -= 50;
            }
                else
                statResult.text = "���� �Ұ����մϴ�. ";
            }

            else if (RandomInt == 8)
            {
                skill.sprite = simage[7];
                statResult.text = "ü�� 50 ����!";
                if (StageManager.GetInstance().Player.MaxHp >= 50) {
                    StageManager.GetInstance().Player.MaxHp -= 50;
                    skill.sprite = simage[7];
                    statResult.text = "ü�� 50 ����!";
                             
                }
               else
                statResult.text = "���� �Ұ����մϴ�. ";
            }


        }
    }




}



