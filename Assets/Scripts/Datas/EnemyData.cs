using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName="My Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float health;
    public float speed;
    public float attackRange;
    public float damage;
}
