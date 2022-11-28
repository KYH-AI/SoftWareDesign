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



    public void Action(GameObject scanOb)
    {

        //isAction = true;  액션이 켜져 있을때 enter을 한번 더 누르면 그대로 꺼진다. 
        scanObject = scanOb;
        ObjData A/*형식이나 변수처럼 이용*/ = scanObject.GetComponent<ObjData>();


        if(scanObject.CompareTag("Market"))
            {
            marketUI.gameObject.SetActive(true);
        }
    
         

        else
        {
            Talk(A.id, A.isNPC);
            talkPanel.SetActive(isAction); //true or fasle 
        }
        
    }



    void Talk(int id, bool isNPC)
    {
        string talkData = talkManager.GetTalk(id, talkIndex); //해당하는 문자열이 나온다. 

        if (talkData == null)
        { isAction = false;
            talkIndex = 0;
            return; } // 이야기가 다 끝나고, 즉 인덱스가 다 돌아가면 대화창 내리기,
                                       // talkIndex와 대화의 문장 갯수를 비교해 끝 확인  
                                       //여기서 끝내면 뒤쪽 if문은 실행되지않음

        if (isNPC) //챕터별 상점 주인마다 id지정 
        {
            easyTalk.text = talkData;
        }

        else 
        {
            easyTalk.text = talkData;
        }
        isAction = true;
        talkIndex++; 
    }
}



/*  public void Action(GameObject scanOb) {

if(isAction) //대화창 나가기
        {
           isAction = false;
        }

        else //대화창 팝업
        {
            //isAction = true;  액션이 켜져 있을때 enter을 한번 더 누르면 그대로 꺼진다. 
            scanObject = scanOb;
            ObjData A/*형식이나 변수처럼 이용* = scanObject.GetComponent<ObjData>();
Talk(A.id, A.isNPC);
        }

        talkPanel.SetActive(isAction); //true or fasle 

    }*/