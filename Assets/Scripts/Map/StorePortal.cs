using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorePortal : MonoBehaviour
{
    public GameObject player;
    float distance;

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < 1)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(SceneChange());
            }
        }
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1f);
        switch (StageManager.Instance.stage)
        {
            case StageManager.Stage.two:
                SceneManager.LoadScene("Stage2");
                break;
            case StageManager.Stage.three:
                SceneManager.LoadScene("Stage3");
                break;
            case StageManager.Stage.four:
                SceneManager.LoadScene("Stage4");
                break;
        }
    }
}
