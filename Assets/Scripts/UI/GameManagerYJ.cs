using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerYJ : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text easyTalk;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;
    public GameObject marketUI;


    private void Start()
    {
        Managers.StageManager.shopManager = this;
    }

    public void Action(GameObject scanOb)
    {

        //isAction = true;  �׼��� ���� ������ enter�� �ѹ� �� ������ �״�� ������. 
        scanObject = scanOb;
        ObjData A/*�����̳� ����ó�� �̿�*/ = scanObject.GetComponent<ObjData>();


        if(scanObject.CompareTag("Market"))
            {
            marketUI.gameObject.SetActive(true);
        }
    
         

        else
        {
            print(A.id + " ," + A.isNPC);
            Talk(A.id, A.isNPC);
    
            talkPanel.SetActive(isAction); //true or fasle 
        }
        
    }



    void Talk(int id, bool isNPC)
    {

        
            string talkData = talkManager.GetTalk(id, talkIndex); //�ش��ϴ� ���ڿ��� ���´�. 

        if (talkData == null)
        {  isAction = false;
           Managers.StageManager.Player.PlayerController.isMoveable = true;
            talkIndex = 0;
            return; } // �̾߱Ⱑ �� ������, �� �ε����� �� ���ư��� ��ȭâ ������,
                                       // talkIndex�� ��ȭ�� ���� ������ ���� �� Ȯ��  
                                       //���⼭ ������ ���� if���� �����������

        if (isNPC) //é�ͺ� ���� ���θ��� id���� 
        {
            easyTalk.text = talkData;
            

        }

        else 
        {
            easyTalk.text = talkData;
        }
        isAction = true;
        Managers.StageManager.Player.PlayerController.isMoveable = true;
        talkIndex++; 
    }
}



/*  public void Action(GameObject scanOb) {

if(isAction) //��ȭâ ������
        {
           isAction = false;
        }

        else //��ȭâ �˾���
        {
            //isAction = true;  �׼��� ���� ������ enter�� �ѹ� �� ������ �״�� ������. 
            scanObject = scanOb;
            ObjData A/*�����̳� ����ó�� �̿�* = scanObject.GetComponent<ObjData>();
Talk(A.id, A.isNPC);
        }

        talkPanel.SetActive(isAction); //true or fasle 

    }*/