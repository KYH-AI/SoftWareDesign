using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{ Dictionary<int, string[]> talkData;
    //Ư�� ������Ʈ�� �ش��ϴ� id�� �ҷ��� ��ȭ�� ��ġ��Ű�� Ű�� ������ ���� ( Ÿ�� �ΰ� �ʿ�, string �� �迭�� �ִ´�.)


    private void Awake()
    {
        talkData = new Dictionary<int, string[]>(); // ���� ���� 
        GenerateData(); //�޼ҵ� �ҷ�����
                        }


    void GenerateData()
    {
        talkData.Add(1, new string[] { "...��? �� ���� �����ڴ� �������� ���±�." ,"�ڳ� ���� �ٺ��� ���� ������ ���� ������ �ϴ� �Դ�." , "�ʿ��� �� ���� �ٵ�, Ư���� �ΰ� ������."});  
        talkData.Add(2, new string[] { "���, �� ���ĳ����°�? ����ϱ�.","���� �༮���� ���� ����ġ �����Ծ�." }); 
        talkData.Add(3, new string[] {"��ġ�� �ʾҳ�? ����ִ� �ڳ� ���� ��� ���� �� �̸��� ����� ���̾�." ,"������ �決."});
       
    }



    public string GetTalk(int id, int talkIndex) //��ȯ���� ���ڿ�  
    //������ ��ȭ ������ ��ȯ�ϴ� �Լ�. 
    {

        if (talkIndex == talkData[id].Length) //talkindex�� ��ȭ�� ���� ���� ���Ͽ� �� Ȯ��
       
          
            return null;
        
        else
        return talkData[id][talkIndex]; //Ű ��ȯ�� �� ���
                                        // GetTalk�Լ��� �� ���徿 �ҷ��� ���� (id �� ��ȭ�� �ҷ����� , talklIndex�� ��ȭ�� �� ������ �ҷ��´�) 

        //GenaarteDate�� talkDate�� �� ���徿 return�ؼ� ��ȯ���ش�. 
    }
}
