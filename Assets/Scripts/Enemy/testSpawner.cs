using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSpawner : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    private float spawnX;
    private float spawny;
    GameObject ob;

    // Update is called once per frame
    void Update()
    {
        SetLocation();
        //if (Managers.StageManager.IsStageCleared())
        if (Input.GetKeyDown(KeyCode.C))
        {

            Managers.UI.bossSlider.gameObject.SetActive(true);
            Spawn();
        }
    }
    void SetLocation()
    {
        spawnX = Managers.StageManager.Player.transform.position.x;
        spawny = Managers.StageManager.Player.transform.position.y + 7f;
        this.transform.position = new Vector2(spawnX, spawny + 3f);
    }
    void Spawn()
    {
        ob = Instantiate(Boss, new Vector2(spawnX, spawny), Quaternion.identity);
        ob.GetComponent<Enemy>().EnemyInit(Managers.StageManager.Player);
    }
}
