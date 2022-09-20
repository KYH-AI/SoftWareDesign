using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campos : MonoBehaviour
{
    public GameObject campos;

    // Update is called once per frame
    void Update()
    {
        transform.position = campos.transform.position;
    }
}
