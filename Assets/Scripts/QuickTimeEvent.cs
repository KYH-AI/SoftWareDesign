using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickTimeEvent : MonoBehaviour
{
    public float fillAmount = 0; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("a"))
        {
            //Debug.Log("press"); 
            fillAmount += .2f; 
        }

        GetComponent<Image>().fillAmount = fillAmount; 
    }
}
