using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCount : MonoBehaviour
{

    public int potion;
    int Tobuy;
    public int Money=1000 ;
    public GameObject select;
    private Text Count;
    private void Update()
    {
        
        
        if(Input.GetMouseButtonDown(1))//���� ���� ���¸� ǥ���ϴ� ��ũ��Ʈ�� ���� ������ ��ü�ϱ�. ��) GetMoney.Now
        { Money -= Tobuy; }


    } }

/*public button select ����,  selectbutton �� �����Ű��, ��ư 6���� �ش� ��ũ��Ʈ �� ���̱�. 
 * ��ư Ŭ�� �̺�Ʈ �߻� �� selectĭ�� �ش� ��ü�� �̸� ������ ����. 
 * int gameobject selecttext�� ����text�� ����, public int GetText �� <text>(shadow����)�� ����.
 * buttonclick(1) {select = GetText.text} �ϱ�. 
 * �׸��� public button buy�� buy����, buyŬ�� �� <>�� ������ ������ ����. 
 * ��ü ������ public int tobuy�� ����. 
 * money�� public int moeny�� ����. <- �̰� ���߿� �ٸ� ��ũ��Ʈ�� �ӴϿ� ����. 
 * if(buttonclick(1)){if(moeny>=tobuy){money-=money-tobuy; text�� ���ŵǾ����ϴ�. 3�� ��������}
 * else{text�� ���� �����մϴ�! 3�� ��������. } }*/
