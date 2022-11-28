using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{ Dictionary<int, string[]> talkData;
    //특정 오브젝트에 해당하는 id를 불러와 대화를 대치시키는 키값 형식의 변수 ( 타입 두개 필요, string 은 배열로 넣는다.)


    private void Awake()
    {
        talkData = new Dictionary<int, string[]>(); // 변수 생성 
        GenerateData(); //메소드 불러오기
                        }


    void GenerateData()
    {
        talkData.Add(1, new string[] { "...흠? 겂 업는 모험자는 오랜만에 보는군." ,"자네 같은 바보를 위해 나같은 놈이 상점을 하는 게다." , "필요한 게 많을 텐데, 특별히 싸게 쳐주지."});  
        talkData.Add(2, new string[] { "어떻게, 잘 해쳐나갔는가? 대단하군.","다음 녀석들은 더욱 만만치 않을게야." }); 
        talkData.Add(3, new string[] {"다치진 않았나? 살아있는 자네 얼굴을 계속 보는 게 이리도 재밌을 줄이야." ,"명줄이 길군."});
       
    }



    public string GetTalk(int id, int talkIndex) //반환값은 문자열  
    //지정된 대화 문장을 반환하는 함수. 
    {

        if (talkIndex == talkData[id].Length) //talkindex와 대화의 문장 갯수 비교하여 끝 확인
       
          
            return null;
        
        else
        return talkData[id][talkIndex]; //키 반환시 값 출력
                                        // GetTalk함수가 한 문장씩 불러와 리턴 (id 로 대화를 불러오고 , talklIndex로 대화의 한 문장을 불러온다) 

        //GenaarteDate의 talkDate를 한 문장씩 return해서 반환해준다. 
    }
}
