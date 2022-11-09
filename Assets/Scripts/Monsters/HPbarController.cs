using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbarController : MonoBehaviour
{
    Slider s;
    float value;

    void Start()
    {
        s = GetComponent<Slider>();
    }

    void Update()
    {
        if(s.value<=0||s.value==1)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }
}
