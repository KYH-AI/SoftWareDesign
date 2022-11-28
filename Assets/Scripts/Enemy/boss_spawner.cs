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
    public BossSpawnEffect bossSpawn;
    private void Start()
    {
        bossSpawn = GetComponent<BossSpawnEffect>();
    }
    // Update is called once per frame
    void Update()
    {
        switch (Managers.StageManager.stage)
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
        //if (Managers.StageManager.IsStageCleared())
        if(Input.GetKeyDown(KeyCode.C))
        {
            
            Managers.UI.bossSlider.gameObject.SetActive(true);
            Managers.UI.InitBossSlider();
            Spawn();
            Managers.CameraManager.SetFollow(ob.transform);
            Managers.CameraManager.SetPriority(11);
            bossSpawn.PlayFromTimeline();
            Managers.StageManager.SetStageKillCount();
        }
    }
    void SetLocation()
    {
        spawnX = Managers.StageManager.Player.transform.position.x;
        spawny = Managers.StageManager.Player.transform.position.y + 7f;
    }
    void Spawn()    
    {
        ob = Instantiate(Boss[StageNum], new Vector2(spawnX, spawny), Quaternion.identity);
        ob.GetComponent<Enemy>().EnemyInit(Managers.StageManager.Player);
    }
}
