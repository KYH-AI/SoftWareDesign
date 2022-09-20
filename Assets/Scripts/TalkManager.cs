using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{ Dictionary<int, string[]> talkData; 
    //특정 오브젝트에 해당하는 id를 불러와 대화를 대치시키는 키값 형식의 변수 


    private void Awake()
    {
        talkData = new Dictionary<int, string[]>(); //변수 생성 
        GenerateData(); //메소드 불러오기
                        }


    void GenerateData()
    {
        talkData.Add(1, new string[] { "...흠?", "겂 업는 모험자는 오랜만에 보는군." });  //인덱스 0, 인덱스 1
        talkData.Add(2, new string[] { "어떻게, 잘 해쳐나갔는가? 대단하군.","다음 녀석들은 더욱 만만치 않을게야." }); 
        talkData.Add(3, new string[] {"다치진 않았나? 살아있는 자네 얼굴을 계속 보는 게 이리도 재밌을 줄이야." ,"명줄이 길군."});
        
        //캐릭터 일러 집어넣기. 
    }



    public string GetTalk(int id, int talkIndex)
    //지정된 대화 문장을 반환하는 함수. 
    {

        if (talkIndex == talkData[id].Length) //talkindex와 대화의 문장 갯수 비교하여 끝 확인
            return null;
    else
        return talkData[id][talkIndex]; //키 반환시 값 출력 

        //GenaarteDate의 talkDate를 한 문장씩 return해서 반환해준다. 
    }
}
