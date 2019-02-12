using UnityEngine;

[CreateAssetMenu (fileName = "Entity", menuName = "ScriptableObjects/Entity", order = 1)]
public class EntityModel : ScriptableObject {

    public float life;
    public float speed;
    public float fireRate;

    public float shield;
    public float shieldRegen; // per second
}
