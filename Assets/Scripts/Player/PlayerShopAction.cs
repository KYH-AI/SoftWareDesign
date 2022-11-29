using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerShopAction : MonoBehaviour
{
    GameObject scanObject;

    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
             Collider2D rayHIt = Physics2D.OverlapCircle(transform.position,  3.5f, LayerMask.GetMask("object")); //�ش緹�̾��ǹ�ü����ĵ
            if (rayHIt != null)
            {
                scanObject = rayHIt.gameObject;
                print("raycast ȣ���");
                Managers.StageManager.shopManager.Action(scanObject);
            } 
            else
                scanObject = null;   
        }




    }

}
