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

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));/*������ ��ȯ*/

        for (int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].Weight;
            if (selectNum >= weight)
            {//��ü�� �����ϴ� ���� 
                Card temp = new Card(deck[i]);
                return temp;
            }
        }

            //  ī�忡 weight�� ���� ���ذ��� , SelectNum�� ���� ���ϸ�, �ش��ϴ� ���� ���Ҹ� ��ȯ�ϸ� �ȴ�.  
            return null ;

       // return deck [Random.Range(0, deck.Count)];
    }
    private void Start()
    {
        for (int i =0; i<deck.Count; i++)
        {
            total += deck[i].Weight; //List�� ��� weight������ total�� �߰� . 

        }
        
    }
}
    
//Ư�����ҿ� ���ñ��� �� ���� �ο��ϴ� ����ġ ������ ����Ѵ�.
