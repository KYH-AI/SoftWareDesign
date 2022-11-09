using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int projectileDamage;
    private float projectileSpeed;
    private Vector2 dir;

    public int ProjectileDamage { get { return projectileDamage; } }
    public float ProjectileSpeed { get { return projectileSpeed; } }
    public Vector2 Dir { get { return dir; } }

    public void ProjectileInit(Vector2 dir, int projectileDamage = 0, float projectileSpeed = 0f)
    {
        this.projectileDamage = projectileDamage;
        this.projectileSpeed = projectileSpeed;
        this.dir = dir;
    }
}
