using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class BossTimer : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        watch.Stop();
        UnityEngine.Debug.Log(watch.ElapsedMilliseconds + " ms"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
