using UnityEngine;

[CreateAssetMenu (fileName = "Entity", menuName = "ScriptableObjects/Entity", order = 1)]
public class BaseEntity : ScriptableObject {

    public int life;
    public float speed;
    public float fireRate;
}
