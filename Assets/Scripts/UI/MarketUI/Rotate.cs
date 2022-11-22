using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.up ,Speed * Time.deltaTime);
        //회전하려는 물체의 축 , 초당 회전시키려는 각도 * 타임
    }

  
}
