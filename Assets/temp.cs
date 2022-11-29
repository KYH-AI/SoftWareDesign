using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temp : MonoBehaviour
{
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = 0;
    }
}
