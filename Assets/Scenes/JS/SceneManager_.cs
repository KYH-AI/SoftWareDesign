using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_ : MonoBehaviour
{
    public static string SceneName;
    private static SceneManager_ instance;
    public static SceneManager_ Instance
    {
        get
        {
            return instance;
        }
    }
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

    float minTime = 0;
    public IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(SceneName);
        minTime = 0;
        asyncOper.allowSceneActivation = false;
        while (!asyncOper.isDone && minTime <= 2.0)
        {
            Debug.Log(minTime);
            minTime += Time.deltaTime;
        }
        asyncOper.allowSceneActivation = true;
        yield return null;
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync());
    }
    
}
