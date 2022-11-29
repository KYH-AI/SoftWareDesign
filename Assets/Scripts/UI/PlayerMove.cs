using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMove : MonoBehaviour
{
    public GameManagerYJ manager; 
    public float Speed;
  
    Rigidbody2D rigid;
     float h;
    float v;
    Vector3 dirVec;
    GameObject scanObject;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        h =  manager.isAction ? 0: Input.GetAxisRaw("Horizontal");
        v =  manager.isAction ? 0: Input.GetAxisRaw("Vertical"); //isAction이 취해져 있으면? 0이다.(fasle) 상태 변수로 플레이어 이동 제한 
         

        if (Input.GetKeyDown(KeyCode.B) && scanObject != null ) 
         {
            print("레이2 호출됨");
            manager.Action(scanObject); }
        
        //scan Object 



    }
    void FixedUpdate()
    {

        rigid.velocity = new Vector2(h, v) * Speed;

        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHIt = Physics2D.Raycast(rigid.position, Vector2.up, 0.7f, LayerMask.GetMask("object")); //해당레이어의물체만스캔


        if (rayHIt.collider != null)
        {  scanObject = rayHIt.collider.gameObject;
            print("raycast 호출됨");
        } //레이케스트 된 obj를 변수로 저장하여 활용  
        else
            scanObject = null;   // 내 동그라미 동글동글 동그라미 동그라미 동글동글 동그라미 
        //네네 네 
    }

}
