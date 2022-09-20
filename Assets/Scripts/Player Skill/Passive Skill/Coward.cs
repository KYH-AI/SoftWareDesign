using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coward : PassiveSkill
{
    private float buffSpeed;
    public float BuffSpeed { get { return buffSpeed; } set { buffSpeed = value; } }

    private float skillDuration;
    public float SkillDuration { get { return skillDuration; } set { skillDuration = value; } }
    private WaitForSeconds skillTime;

    public override void OnActive()
    {
        
    }

    private float SettingSpeed(float updateSpeed)
    {
        
        return 0.1f;
    }

    private void CowardSkillActive()
    {
       // playerController.MoveSpeed *= buffSpeed;
    }
}
