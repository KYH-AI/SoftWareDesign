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
        v =  manager.isAction ? 0: Input.GetAxisRaw("Vertical"); //isAction�� ������ ������? 0�̴�.(fasle) ���� ������ �÷��̾� �̵� ���� 
         

        if (Input.GetKeyDown(KeyCode.B) && scanObject != null ) 
         {
            print("����2 ȣ���");
            manager.Action(scanObject); }
        
        //scan Object 



    }
    void FixedUpdate()
    {

        rigid.velocity = new Vector2(h, v) * Speed;

        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHIt = Physics2D.Raycast(rigid.position, Vector2.up, 0.7f, LayerMask.GetMask("object")); //�ش緹�̾��ǹ�ü����ĵ


        if (rayHIt.collider != null)
        {  scanObject = rayHIt.collider.gameObject;
            print("raycast ȣ���");
        } //�����ɽ�Ʈ �� obj�� ������ �����Ͽ� Ȱ��  
        else
            scanObject = null;   // �� ���׶�� ���۵��� ���׶�� ���׶�� ���۵��� ���׶�� 
        //�׳� �� 
    }

}