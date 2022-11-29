using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    public GameObject spawnerBasic1;
    public GameObject spawnerBasic2;
    public GameObject spawnerElite;

    bool restart = false;

    private void Start()
    {
        spawnerBasic1.SetActive(true);
        spawnerBasic2.SetActive(true);
        spawnerElite.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        if (Managers.StageManager.isBossAlive == false)
        {
            spawnerBasic1.SetActive(false);
            spawnerBasic2.SetActive(false);
            spawnerElite.SetActive(false);
            restart = true;
            Debug.Log("isBossAlive : " + Managers.StageManager.isBossAlive);
            Debug.Log("restart : " + restart);
        }

        if (Managers.StageManager.isBossAlive == true && restart == true)
        {
            spawnerBasic1.SetActive(true);
            spawnerBasic2.SetActive(true);
            spawnerElite.SetActive(true);
        }
    }
}
