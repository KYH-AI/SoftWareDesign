using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{ Dictionary<int, string[]> talkData;
    //Ư�� ������Ʈ�� �ش��ϴ� id�� �ҷ��� ��ȭ�� ��ġ��Ű�� Ű�� ������ ���� ( Ÿ�� �ΰ� �ʿ�, string �� �迭�� �ִ´�.)


    int count;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>(); // ���� ���� 
        GenerateData(); //�޼ҵ� �ҷ�����
                        }


    void GenerateData()
    {
        talkData.Add(count=1, new string[] { "...��? ������ �����ڴ� �������� ���±�." ,"�� ������ ���� �� ������ ���ٴ�, �������� �ƴϱ���.", "�ڳ� ���� �ٺ��� ���� ������ ���� ���⼭ ������ �ϴ� �Դ�." , "�ʿ��� �� ���� �ٵ�, Ư���� �ΰ� ������.", "���� �������� ���� ������." ,"�̰� ���� �غ� �� �Ǹ� ���� ���� ��Ż�� �ٽ� ����. "});  
        talkData.Add(count=2, new string[] { "���, �� ���ĳ����°�? �ʽ��� ġ�� ����ϱ�.","���� �༮���� ���� ����ġ �����Ծ�. �����Ͻð�." }); 
        talkData.Add(count = 3, new string[] { " ��ġ�� �ʾҳ�? ����ִ� �ڳ� ���� ��� ���� �� �̸��� ����� ���̾�." , "������ �決. �̷��Ա��� �뾲�� �����̾� �𸣰����� ���� �� �縮�Գ�." });
        talkData.Add(count=4, new string[] { "...�� �ʸӿ� �ִ� ���� �������� �������� ���߳�.", "�ҽ��� ���� ���� ����� �޲����. ���� ������ ���� �ε��� ���� �ڷ� ������ ���⼭ �����̳� �ϸ鼭 ��ġ�� �ִٸ�,", " ������ ���� �� ��ٳ�. �ε� �����ϽðԳ�." });

    }



    public string GetTalk(int id, int talkIndex) //��ȯ���� ���ڿ�  
                                                 //������ ��ȭ ������ ��ȯ�ϴ� �Լ�. 
    {  

        if (talkIndex == talkData[id].Length) //talkindex�� ��ȭ�� ���� ���� ���Ͽ� �� Ȯ��


            return null;
   
            else
               return talkData[id][talkIndex]; }//Ű ��ȯ�� �� ���
                                                // GetTalk�Լ��� �� ���徿 �ҷ��� ���� (id �� ��ȭ�� �ҷ����� , talklIndex�� ��ȭ�� �� ������ �ҷ��´�) 




    public void Update()
    {
        switch (Managers.StageManager.stage)
        {
            

            case Define.Stage.STAGE2:
                count = 1;
                break;
            case Define.Stage.STAGE3:
                count = 2;
                break;
            case Define.Stage.STAGE4:
                count = 3;
                break;

            case Define.Stage.Boss:
                count = 4;
                break;
        }
    }



    //GenaarteDate�� talkDate�� �� ���徿 return�ؼ� ��ȯ���ش�. 
}



