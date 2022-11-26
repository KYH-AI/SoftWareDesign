using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SliderSettings : MonoBehaviour
{
    public Slider playerSlider;
    public Slider bossSlider;

    // Start is called before the first frame update
    private void Start()
    {
        Managers.UI.playerSlider = playerSlider;
        Managers.UI.bossSlider = bossSlider;
    }

}
