using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dontdestroy : MonoBehaviour
{
    private void Awake()
    {
        var obj = FindObjectsOfType<Dontdestroy>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
