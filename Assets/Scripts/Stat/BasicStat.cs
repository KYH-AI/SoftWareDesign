using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Baisc Stat", menuName = "Scriptable Object/Stat")]
public class BasicStat : ScriptableObject
{
    public int Hp;
    public float MoveSpeed;
    public int Armor;
    public int DefaultAttackDamage;
}