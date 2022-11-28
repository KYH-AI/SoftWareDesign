using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDice: MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public int total = 0;
    public Card RandomDiceCard()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));/*정수로 빈환*/

        for (int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].Weight;
            if (selectNum >= weight)
            {//객체를 참조하는 형태 
                Card temp = new Card(deck[i]);
                return temp;
            }
        }

            //  카드에 weight의 값을 더해가며 , SelectNum의 값과 비교하며, 해당하는 값의 원소를 반환하면 된다.  
            return null ;

       // return deck [Random.Range(0, deck.Count)];
    }
    private void Start()
    {
        for (int i =0; i<deck.Count; i++)
        {
            total += deck[i].Weight; //List의 모든 weight에대해 total에 추가 . 

        }
        
    }
}
    
//특정원소에 선택권을 더 많이 부여하는 가중치 랜덤을 사용한다.
