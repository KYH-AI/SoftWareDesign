using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanIBuy : MonoBehaviour

{
    public Button btn;
   int Product;
    public GameObject Yes;
    public GameObject No;
    int Money;
    public float Speed;
    public Text AnswerText;

    // GameObject  Money = new GameObject();
    //  Money.AddComponent<Text>();

    public void Start()
    {//Money = GameObject.Find("NowMoney");

        Yes.SetActive(false);
        No.SetActive(false);
        btn.onClick.AddListener(Answer);
        Money = 200;
    }




    public void S1()
    {
        AnswerText.text = "<회복 포션> 가격 : 100 구매하시겠습니까? ";
        Product = 100;
   
    } //회복 포션


    public void S2()
    {
        AnswerText.text = "<새 스킬? 무기 획득> 가격 : 100 구매하시겠습니까? ";
        Product = 100;
    } // 새 스킬 구매 ?  Product = 150;


    public void S3()
    {
        AnswerText.text = "<기존 스킬 공격력 증가>  가격 : 100 구매하시겠습니까? ";
        Product = 100;
    }// 기존 스킬 공격력 증가? 

    public void S4()
    {
        AnswerText.text = "<스텟1> 가격 : 150 구매하시겠습니까? ";
        Product = 150;
    }
    public void S5()
    {
        AnswerText.text = "<스텟2> 가격 : 150 구매하시겠습니까? ";
        Product = 150;
    }

    public void S6()
    {
        AnswerText.text = "<스텟1> 가격 : 150 구매하시겠습니까? ";
        Product = 150;
    }


    void Answer()
    {
        if (Money >= Product)
        {
            Money -= Product;
            Yes.SetActive(true);
        Invoke("HideAnswerY", Speed); }


        else { No.SetActive(true);
            Invoke("HideAnswerN", Speed);
        }
       
    }

    void HideAnswerY() { Yes.SetActive(false); } 
    void HideAnswerN() { No.SetActive(false); }

  



}
