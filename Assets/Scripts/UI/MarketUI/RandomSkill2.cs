using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSkill2 : MonoBehaviour
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
        RandomInt = Random.Range(1, 3);


        if (RandomInt == 1)
        {
            skill.sprite = simage[0];
            stext.text = " <��ų4> ����:100 �����Ͻðڽ��ϱ�?";
            ItemName.text = "<��ų4>";
            product = 100;
            itemDetailExplan.text = "��ų4";

        }
        else if (RandomInt == 2)
        {

            skill.sprite = simage[1];
            stext.text = "  <��ų5> ����:200 �����Ͻðڽ��ϱ�?";
            ItemName.text = "<��ų5>";
            product = 200;
            itemDetailExplan.text = "��ų5";
        }
        else if (RandomInt == 3)
        {
            skill.sprite = simage[2];
            stext.text = "  <��ų6> ����:300 �����Ͻðڽ��ϱ�?";
            ItemName.text = "<��ų6>";
            product = 300;
            itemDetailExplan.text = "��ų6";
        }


    }
}
   