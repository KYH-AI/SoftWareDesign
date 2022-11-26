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
    public Text stext; //구매창에 뜨는 텍스트
    public Text itemDetailExplan; //무기 설명 
    int product; //물건 가격
   

    // Start is called before the first frame update
    void Start()

        
    {
        RandomInt = Random.Range(1,3);

   
        if (RandomInt == 1)
        {
            skill.sprite = simage[0];
            stext.text = " <스킬1> 가격:100 구매하시겠습니까?";
            ItemName.text = "<스킬1>";
            product = 100;
            itemDetailExplan.text = "스킬1";

        }
        else if (RandomInt == 2)
        {

            skill.sprite = simage[1];
            stext.text = "  <스킬2> 가격:200 구매하시겠습니까?";
            ItemName.text = "<스킬2>";
            product = 200;
            itemDetailExplan.text = "스킬2";
        }
        else if (RandomInt == 3)
        {
            skill.sprite = simage[2];
            stext.text = "  <스킬3> 가격:300 구매하시겠습니까?";
            ItemName.text = "<스킬3>";
            product = 300;
            itemDetailExplan.text = "스킬3";
        }



    }

}

   

   