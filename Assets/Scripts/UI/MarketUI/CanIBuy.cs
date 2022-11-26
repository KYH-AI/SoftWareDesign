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


    // GameObject  Money = new GameObject();
    //  Money.AddComponent<Text>();

    public void Start()
    {//Money = GameObject.Find("NowMoney");

        Yes.SetActive(false);
        No.SetActive(false);
        btn.onClick.AddListener(Answer);
        Money = 200;
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
