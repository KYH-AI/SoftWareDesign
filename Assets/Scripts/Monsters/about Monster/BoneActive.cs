using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneActive : MonoBehaviour
{
    public GameObject bone;
    public GameObject blue1;
    public GameObject blue2;
    public GameObject blue3;
    void Start()
    {
        bone.SetActive(true);
    }

    void Update()
    {
        if (bone.activeSelf == false)
        {
            blue1.SetActive(true);
            blue2.SetActive(true);
            blue3.SetActive(true);
        }
    }
}
