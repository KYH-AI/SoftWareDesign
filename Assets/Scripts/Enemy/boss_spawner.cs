using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_spawner : MonoBehaviour
{
    [SerializeField] GameObject[] Boss;
    int StageNum;
    private float spawnX;
    private float spawny;
    GameObject ob;
    

    // Update is called once per frame
    void Update()
    {
        switch (StageManager.GetInstance().stage)
        {
            case Define.Stage.STAGE1:
                StageNum = 0;
                break;
            case Define.Stage.STAGE2:
                StageNum = 1;
                break;
            case Define.Stage.STAGE3:
                StageNum = 2;
                break;
            case Define.Stage.STAGE4:
                StageNum = 3;
                break;

        }
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
        ob = Instantiate(Boss[StageNum], new Vector2(spawnX, spawny), Quaternion.identity);
        ob.GetComponent<Enemy>().EnemyInit(StageManager.GetInstance().Player);
    }
}
