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
        
        
        if(Input.GetMouseButtonDown(1))//현재 재정 상태를 표시하는 스크립트의 변수 가져와 대체하기. 예) GetMoney.Now
        { Money -= Tobuy; }


    } }

/*public button select 생성,  selectbutton 에 연결시키기, 버튼 6개에 해당 스크립트 다 붙이기. 
 * 버튼 클릭 이벤트 발생 시 select칸에 해당 물체의 이름 가져와 띄우기. 
 * int gameobject selecttext를 좌측text와 연결, public int GetText 를 <text>(shadow빼고)에 연결.
 * buttonclick(1) {select = GetText.text} 하기. 
 * 그리고 public button buy에 buy연결, buy클릭 시 <>의 가격을 가져와 뺀다. 
 * 물체 가격을 public int tobuy와 연결. 
 * money를 public int moeny에 연결. <- 이건 나중에 다른 스크립트의 머니와 연결. 
 * if(buttonclick(1)){if(moeny>=tobuy){money-=money-tobuy; text로 구매되었습니다. 3초 내보내기}
 * else{text로 돈이 부족합니다! 3초 내보내기. } }*/
