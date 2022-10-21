using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{


    private static StageManager instance;
    public static StageManager Instance { get{return instance;} }
    public enum Stage{
        one, 
        two,
        three,
        four,
        five
    }
    public Stage stage = Stage.one;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
