using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_spawner : MonoBehaviour
{
    //public GameObject Boss4;
    public GameObject Boss;
    //public GameObject Boss2;
    //public GameObject Boss1;
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
        /* switch (StageManager.GetInstance().stage)
         {
             case Define.Stage.ONE:
                 ob = Instantiate(Boss1, new Vector2(spawnX, spawny), Quaternion.identity);
                 break;
             case Define.Stage.TWO:
                 ob = Instantiate(Boss2, new Vector2(spawnX, spawny), Quaternion.identity);
                 break;
             case Define.Stage.THREE:
                 ob = Instantiate(Boss3, new Vector2(spawnX, spawny), Quaternion.identity);
                 break;
             case Define.Stage.FOUR:
                 ob = Instantiate(Boss4, new Vector2(spawnX, spawny), Quaternion.identity);
                 break;
         }*/
        ob = Instantiate(Boss, new Vector2(spawnX, spawny), Quaternion.identity);
        ob.GetComponent<Enemy>().EnemyInit(StageManager.GetInstance().Player);
    }
}
