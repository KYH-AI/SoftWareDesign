using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello Unity!");


        //�Ϲ� ����
        int level = 5;
        float strength = 15.5f; //�� F ���̱�
        string playerName =" ������";
        bool isFullLevel = false;
        //���� �ʱ�ȭ ȣ�� ������ �귯��

        Debug.Log("�÷��̾��� �̸���?");
        Debug.Log(playerName);
        Debug.Log("������ �� á�°�?");
        Debug.Log(isFullLevel);
        Debug.Log("������" + level +"�̰�, ����"+strength);

        //�׷��� ����.�迭


        string[] name = { "�ڳ�", "���", "�̳�" };
        Debug.Log(name[0]+name[1]+name[2]);



        int[] monsterLevel = new int [3];
        monsterLevel[0] = 1;
        monsterLevel[1] = 2;
        monsterLevel[3] = 3;

        Debug.Log("������ ����");
        for (int i = 0; i <= monsterLevel.Length; i++)
            Debug.Log(monsterLevel[i]);


        //����Ʈ.
        List<string> items = new List<string>();
        items.Add("����");
        items.Add("����");

        items.RemoveAt(0);

        Debug.Log(items[0] + items[1]);
        //�ε��� ����. �ε��� 0 �� ���� ����� 1�� ���� 0���� �������, 1 �ε����� ������� �ȴ�. 



        //������
        int exp = 1500;
        exp = 1500 + 320;
        exp = 1500 - 10;
        level = exp / 300;
        strength = level * 3.1f;

        Debug.Log(exp);
        Debug.Log(level);
        Debug.Log(strength);

        string title = "The Legned of";
            Debug.Log(title + " " + name);
    }

   
    void Update()
    {
        
    }
}
