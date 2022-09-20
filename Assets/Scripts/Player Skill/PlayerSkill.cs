using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    protected int damage;
    public int Damage { get { return damage; } set { damage = value; } }

    protected float duration;
    public float Duration { get { return duration; } set { duration = value; } }

    protected int projectileCount;
    public int ProjectileCount { get { return projectileCount; } set { projectileCount = value; } }


    protected GameObject playerObject;
    protected PlayerController playerController;

    protected int enemyLayer = 1 << 11;
    protected int wallLayer = 1 << 12;

    public void Init(GameObject skillMaster)
    {
        playerObject = skillMaster;
        damage = 10;
        duration = 2f;
        projectileCount = 1;
        playerController = playerObject.GetComponent<PlayerController>();   
    }
}
