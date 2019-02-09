using UnityEngine;

[CreateAssetMenu (fileName = "Projectile", menuName = "Assets/Projectile", order = 1)]
public class ProjectileModel : ScriptableObject {

    public int damage;
    public float speed; // unit by sec
}
