using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "My Game/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float baseDamage;
    public float damage;

    [SerializeField] private float baseAttackSpeed;
    public float attackSpeed;

    [SerializeField] private float baseRange;
    public float range;


    public void SetStartStats()
    {
        damage = baseDamage;
        attackSpeed = baseAttackSpeed;
        range = baseRange;
    }
}
