using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDice : MonoBehaviour

{
    public int total=0;
    public List<Card> deck = new List<Card>();

    public Card RandomCard() //카드 클레스를 반환형으로 갖는 메소드 생성
    {
        int weight=0;
        int selectNum = 0;
        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        for(int i = 0; i< deck.Count; i++)
        {
            weight += deck[i].weight;
            if(selectNum<= weight)
            {
                Card temp = new Card(deck[i]);
                return temp;
            }

        }
        return null ;
       // return deck[Random.Range(0, deck.Count)]; 렌덤함수로 리스트에서 아무 카드 한장을 뽑아 리턴해준다. 
    }

    private void Start()
    {
        for(int i =0; i<deck.Count;i++)
        { total += deck[i].weight; }
        ResultSelect();
    }



    public void ResultSelect()
    {
        for(int i=0; i< 10; i++)

        { }

    }
}
    
//특정원소에 선택권을 더 많이 부여하는 가중치 랜덤을 사용한다.
