using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSkill3 : MonoBehaviour
{
    public Image skill;
    public Sprite[] simage;
    public Text ItemName;
    int RandomInt;
    public Text stext; //����â�� �ߴ� �ؽ�Ʈ
    public Text itemDetailExplan; //���� ���� 
    int product; //���� ����


    // Start is called before the first frame update
    void Start()
    {
        RandomInt = Random.Range(1, 4);


        if (RandomInt == 1)
        {
            skill.sprite = simage[0];
            stext.text = " <��ų7> ����:100 �����Ͻðڽ��ϱ�?";
            ItemName.text = "<��ų7>";
            product = 100;
            itemDetailExplan.text = "��ų7";

        }
        else if (RandomInt == 2)
        {

            skill.sprite = simage[1];
            stext.text = "  <��ų8> ����:200 �����Ͻðڽ��ϱ�?";
            ItemName.text = "<��ų8>";
            product = 200;
            itemDetailExplan.text = "��ų8";
        }
        else if (RandomInt == 3)
        {
            skill.sprite = simage[2];
            stext.text = "  <��ų9> ����:300 �����Ͻðڽ��ϱ�?";
            ItemName.text = "<��ų9>";
            product = 300;
            itemDetailExplan.text = "��ų9";
        }

        else if (RandomInt == 4)
        {
            skill.sprite = simage[2];
            stext.text = "  <��ų10> ����:300 �����Ͻðڽ��ϱ�?";
            ItemName.text = "<��ų10>";
            product = 300;
            itemDetailExplan.text = "��ų10";
        }
    }

}