using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSkill : MonoBehaviour
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
        RandomInt = Random.Range(1,3);

   
        if (RandomInt == 1)
        {
            skill.sprite = simage[0];
            stext.text = " <��ų1> ����:100 �����Ͻðڽ��ϱ�?";
            ItemName.text = "<��ų1>";
            product = 100;
            itemDetailExplan.text = "��ų1";

        }
        else if (RandomInt == 2)
        {

            skill.sprite = simage[1];
            stext.text = "  <��ų2> ����:200 �����Ͻðڽ��ϱ�?";
            ItemName.text = "<��ų2>";
            product = 200;
            itemDetailExplan.text = "��ų2";
        }
        else if (RandomInt == 3)
        {
            skill.sprite = simage[2];
            stext.text = "  <��ų3> ����:300 �����Ͻðڽ��ϱ�?";
            ItemName.text = "<��ų3>";
            product = 300;
            itemDetailExplan.text = "��ų3";
        }



    }

}

   

   