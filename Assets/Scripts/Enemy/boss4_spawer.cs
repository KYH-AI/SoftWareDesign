using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss4_spawer : MonoBehaviour
{
    [SerializeField] GameObject Boss4;

    private float spawnX;
    private float spawny;
    GameObject ob;
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        SetLocation();
        if (Input.GetKeyDown(KeyCode.C))
        {
            Spawn();
        }
    }
    void SetLocation()
    {
        spawnX = StageManager.GetInstance().Player.transform.position.x;
        spawny = StageManager.GetInstance().Player.transform.position.y + 7f;
    }
    void Spawn()
    {
        ob = Instantiate(Boss4, new Vector2(spawnX, spawny), Quaternion.identity);
        ob.GetComponent<Enemy>().EnemyInit(StageManager.GetInstance().Player);
    }
}
