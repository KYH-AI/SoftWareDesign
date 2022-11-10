using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Player player;
    private static StageManager instance;
    
    
    public Define.Stage stage = Define.Stage.ONE;


    private void Awake()
    {
        
        SetInstance();
    }
    private void SetInstance()
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
    public static StageManager GetInstance()
    {
        return instance;
    }

    
}
