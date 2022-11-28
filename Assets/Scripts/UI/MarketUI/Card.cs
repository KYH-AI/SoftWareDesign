using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardGrade{Sheld, Attack, Speed, Hp}
[System.Serializable]
public class Card
{
    public string cardName;
    public GameObject cardImage;
    public CardGrade cardGrade;
    public int Weight;
    public Card(Card card) //생성자에는 현재 카드 class를 매개변수로 받아 현재 카드 class를 초기화한다. 
    {
        this.cardName = card.cardName;
        this.cardImage = card.cardImage;
        this.cardGrade = card.cardGrade;
        this.Weight = card.Weight;

    }
}




