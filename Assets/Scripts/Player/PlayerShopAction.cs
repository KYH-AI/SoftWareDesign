using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerShopAction : MonoBehaviour
{
    //public GameManagerYJ manager;
    /*
    public float Speed;
  
    Rigidbody2D rigid;
     float h;
    float v;
    Vector3 dirVec;
        */
    GameObject scanObject;


    void Awake()
    {
       // rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        /*
        h =  manager.isAction ? 0: Input.GetAxisRaw("Horizontal");
        v =  manager.isAction ? 0: Input.GetAxisRaw("Vertical"); //isAction�� ������ ������? 0�̴�.(fasle) ���� ������ �÷��̾� �̵� ���� 
         */



        //scan Object 



    }
    void FixedUpdate()
    {

       // rigid.velocity = new Vector2(h, v) * Speed;


        if (Input.GetKeyDown(KeyCode.B))// && scanObject != null)
        {
            print("����2 ȣ���");
      

          //  Debug.DrawRay(transform.position, Vector2.up * 0.7f, new Color(0, 1, 0));
             Collider2D rayHIt = Physics2D.OverlapCircle(transform.position,  2f, LayerMask.GetMask("object")); //�ش緹�̾��ǹ�ü����ĵ
            if (rayHIt != null)
            {
                scanObject = rayHIt.gameObject;
                print("raycast ȣ���");
                Managers.StageManager.shopManager.Action(scanObject);
            } //�����ɽ�Ʈ �� obj�� ������ �����Ͽ� Ȱ��  
            else
                scanObject = null;   
        }




    }

}
