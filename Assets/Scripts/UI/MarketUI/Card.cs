using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardGrade { sheld, attack, speed, HP }
[System.Serializable]
public class Card
    {
        public string cardName;
        public GameObject cardImage;
        public CardGrade cardGrade;
        public int weight;  //표
     

        public Card(Card card) //복사를 위해 생성자 정의.
                               //매개변수로 Card class를 받아 현재 카드 클래스 초기화한다.
                               //생성자는 해당 클레스와 이름이 같아야 한다.
                               //생성자 정의 시 컴파일러는 자동으로 생성자를 만들지 않는다. 
        {
            this.cardName = card.cardName; 
            this.cardImage = card.cardImage;
            this.cardGrade = card.cardGrade;
            this.weight = card.weight;
    }  //지금 이 스크립트를 의미하는this,
       //게임 오브젝트에서 distroy(this) 시 오브젝트는 그대로 남고
       //오브젝트에 부착된 스크립트만 지워진다.


}



