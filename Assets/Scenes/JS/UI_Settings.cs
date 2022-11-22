using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    Slider bossSlider;

    // Start is called before the first frame update
    void Start()
    {
        Managers.UI.bossSlider = bossSlider;
    }
}
