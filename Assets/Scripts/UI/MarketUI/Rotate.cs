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
        //ȸ���Ϸ��� ��ü�� �� , �ʴ� ȸ����Ű���� ���� * Ÿ��
    }

  
}
