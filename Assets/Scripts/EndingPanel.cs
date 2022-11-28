using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EndingPanel : MonoBehaviour
{
    public GameObject panel;
    public Text gameResult;
    float time = 0;

    private void Start()
    {
        if (true)//게임 클리어 조건
        {
            gameResult.text = "Success Mission!";
            gameResult.color = new Color(240/255f, 240/255f, 90/255f, 255/255f);
        }

        time = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 5&&time<73)
        {
            transform.Translate(Vector3.up * Time.deltaTime*100.0f);
        }
    }
}
