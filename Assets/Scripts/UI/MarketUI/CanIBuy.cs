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
        AnswerText.text = "<ȸ�� ����> ���� : 100 �����Ͻðڽ��ϱ�? ";
        Product = 100;
   
    } //ȸ�� ����


    public void S2()
    {
        AnswerText.text = "<�� ��ų? ���� ȹ��> ���� : 100 �����Ͻðڽ��ϱ�? ";
        Product = 100;
    } // �� ��ų ���� ?  Product = 150;


    public void S3()
    {
        AnswerText.text = "<���� ��ų ���ݷ� ����>  ���� : 100 �����Ͻðڽ��ϱ�? ";
        Product = 100;
    }// ���� ��ų ���ݷ� ����? 

    public void S4()
    {
        AnswerText.text = "<����1> ���� : 150 �����Ͻðڽ��ϱ�? ";
        Product = 150;
    }
    public void S5()
    {
        AnswerText.text = "<����2> ���� : 150 �����Ͻðڽ��ϱ�? ";
        Product = 150;
    }

    public void S6()
    {
        AnswerText.text = "<����1> ���� : 150 �����Ͻðڽ��ϱ�? ";
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
