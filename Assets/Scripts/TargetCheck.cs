using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCheck : MonoBehaviour
{
    Boss bossScript; 

    


    // Start is called before the first frame update
    void Start()
    {

        bossScript = GetComponentInParent<Boss>(); 


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            bossScript.follow = true;                
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            bossScript.follow = false;
    }
}
