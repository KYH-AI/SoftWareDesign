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
   

    // Start is called before the first frame update
    void Start()


       // Player.Money =-150;

    {
        RandomInt = Random.Range(1, 8);

        if (RandomInt == 1)
        {
            skill.sprite = simage[0];
            statResult.text = "공격력 100 증가!";
        }

        else if (RandomInt == 2)
        {
            skill.sprite = simage[1];
            statResult.text = "방어력 100 증가!";
        }

        else if (RandomInt == 3)
        {
            skill.sprite = simage[2];
            statResult.text = "이동속도 100 증가!";
        }

        else if (RandomInt == 4)
        {
            skill.sprite = simage[3];
            statResult.text = "체력 100 증가!";
        }

        else if (RandomInt == 5)
        {
            skill.sprite = simage[4];
            statResult.text = "공격력 50 감소!";
        }

        else if (RandomInt == 6)
        {
            skill.sprite = simage[5];
            statResult.text = "방어력 50 감소!";
        }

        else if (RandomInt == 7)
        {
            skill.sprite = simage[6];
            statResult.text = "이동속도 50 감소!";
        }

        else if (RandomInt == 8)
        {
            skill.sprite = simage[7];
            statResult.text = "체력 50 증가!";
        }

      
    }




}



