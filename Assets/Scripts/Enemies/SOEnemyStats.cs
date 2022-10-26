using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOEnemyStats : ScriptableObject
{
    public float damage = 1f;
    public float maxHealth = 10f;
    public float health = 10f;
    public float defense = 2f;
}
