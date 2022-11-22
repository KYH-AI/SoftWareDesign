using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDice : MonoBehaviour

{
    public int total=0;
    public List<Card> deck = new List<Card>();

    public Card RandomCard() //ī�� Ŭ������ ��ȯ������ ���� �޼ҵ� ����
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
       // return deck[Random.Range(0, deck.Count)]; �����Լ��� ����Ʈ���� �ƹ� ī�� ������ �̾� �������ش�. 
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
    
//Ư�����ҿ� ���ñ��� �� ���� �ο��ϴ� ����ġ ������ ����Ѵ�.
