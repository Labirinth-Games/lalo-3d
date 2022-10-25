using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOPlayerSettings : ScriptableObject
{
    [Header("Settings")]
    public float speed = 3f;
    public float fastSpeed = 7f;
    public float damage;
    public float defense;

    [Space()]
    public string displayName = "You name";

}
