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
            statResult.text = "���ݷ� 100 ����!";
        }

        else if (RandomInt == 2)
        {
            skill.sprite = simage[1];
            statResult.text = "���� 100 ����!";
        }

        else if (RandomInt == 3)
        {
            skill.sprite = simage[2];
            statResult.text = "�̵��ӵ� 100 ����!";
        }

        else if (RandomInt == 4)
        {
            skill.sprite = simage[3];
            statResult.text = "ü�� 100 ����!";
        }

        else if (RandomInt == 5)
        {
            skill.sprite = simage[4];
            statResult.text = "���ݷ� 50 ����!";
        }

        else if (RandomInt == 6)
        {
            skill.sprite = simage[5];
            statResult.text = "���� 50 ����!";
        }

        else if (RandomInt == 7)
        {
            skill.sprite = simage[6];
            statResult.text = "�̵��ӵ� 50 ����!";
        }

        else if (RandomInt == 8)
        {
            skill.sprite = simage[7];
            statResult.text = "ü�� 50 ����!";
        }

      
    }




}



