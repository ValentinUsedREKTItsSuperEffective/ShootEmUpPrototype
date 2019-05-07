using UnityEngine;
using UniRx;

[CreateAssetMenu (fileName = "Entity", menuName = "ScriptableObjects/Entity", order = 1)]
public class EntityModel : ScriptableObject {
    
    public float life;
    public ReactiveProperty<float> currentLife;
    public float speed;

    public int damage;
    public float fireRate;
    public float fireSpeed;
    public float fireDispersion; // circle ray

    public int shield;
    public int shieldRegen; // per second
    public float shieldReloadCooldown;
    public float shieldReloadCapacity; // percentage
}
