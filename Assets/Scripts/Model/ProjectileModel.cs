﻿using UnityEngine;

[CreateAssetMenu (fileName = "Projectile", menuName = "ScriptableObjects/Projectile", order = 3)]
public class ProjectileModel : ScriptableObject {

    public int damage;
    public float speed; // unit by sec
}
