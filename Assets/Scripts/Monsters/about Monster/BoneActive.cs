using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneActive : Enemy
{
    public GameObject bone;
    public GameObject blue1;
    public GameObject blue2;
    public GameObject blue3;
    void Start()
    {
        bone.GetComponent<Enemy>().EnemyInit(StageManager.GetInstance().Player);
        bone.SetActive(true);

        blue1.GetComponent<Enemy>().EnemyInit(StageManager.GetInstance().Player);
        blue2.GetComponent<Enemy>().EnemyInit(StageManager.GetInstance().Player);
        blue3.GetComponent<Enemy>().EnemyInit(StageManager.GetInstance().Player);
        blue1.SetActive(false);
        blue2.SetActive(false);
        blue3.SetActive(false);
    }

    void Update()
    {
        if (bone.activeSelf == false)
        {
            blue1.transform.position = bone.transform.position + Vector3.left;
            blue1.SetActive(true);

            blue2.transform.position = bone.transform.position + Vector3.right;
            blue2.SetActive(true);

            blue3.transform.position = bone.transform.position;
            blue3.SetActive(true);
        }

        if (bone.activeSelf == false && blue1.activeSelf == false && blue2.activeSelf == false && blue3.activeSelf == false)
            gameObject.SetActive(false);
    }
}
