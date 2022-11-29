using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PutSpace : MonoBehaviour
{
    TextMeshPro space;
    float time;

    private void Awake()
    {
        space = GetComponentInChildren<TextMeshPro>();
    }
    private void Update()
    {
        if (time < 0.5f)
        {
            space.alpha =1- time;
        }
        else
        {
            space.alpha = time;
            if (time > 1f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime;
    }

}
