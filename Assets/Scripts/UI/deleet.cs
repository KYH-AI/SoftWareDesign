using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello Unity!");


        //일반 변수
        int level = 5;
        float strength = 15.5f; //꼭 F 붙이기
        string playerName =" 윤여정";
        bool isFullLevel = false;
        //선언 초기화 호출 순으로 흘러감

        Debug.Log("플레이어의 이름은?");
        Debug.Log(playerName);
        Debug.Log("레벨이 꽉 찼는가?");
        Debug.Log(isFullLevel);
        Debug.Log("레벨은" + level +"이고, 힘은"+strength);

        //그룹형 변수.배열


        string[] name = { "코난", "장미", "미나" };
        Debug.Log(name[0]+name[1]+name[2]);



        int[] monsterLevel = new int [3];
        monsterLevel[0] = 1;
        monsterLevel[1] = 2;
        monsterLevel[3] = 3;

        Debug.Log("몬스터의 레벨");
        for (int i = 0; i <= monsterLevel.Length; i++)
            Debug.Log(monsterLevel[i]);


        //리스트.
        List<string> items = new List<string>();
        items.Add("물약");
        items.Add("마검");

        items.RemoveAt(0);

        Debug.Log(items[0] + items[1]);
        //인덱스 오류. 인덱스 0 의 값을 지우면 1의 값이 0으로 당겨지고, 1 인덱스는 비워지게 된다. 



        //연산자
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
