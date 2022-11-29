using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    static string nextScene;
    public Image progressBar;
    float time;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    
    void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }
    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        //float timer = 0f;
        time = 0;
        while (!op.isDone)
        {
            time += Time.unscaledDeltaTime;
            progressBar.fillAmount = time/5f;
            if (progressBar.fillAmount >=1f)
            {
                op.allowSceneActivation = true;
                yield break; 
            }
            yield return null;
        }
    }

  
}
