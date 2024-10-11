using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType",menuName = "CreateEnemy/EnemyType",order = 1)]
public class EnemyTypes : ScriptableObject
{
    public string EnemyName;
    public float MoveSpeed;
    public float TurnSpeed;
    public float ShootRange;
    public float ShootRate;
    public float HitDamage;
    public float Health;
    public Color EnemyColor;
}
