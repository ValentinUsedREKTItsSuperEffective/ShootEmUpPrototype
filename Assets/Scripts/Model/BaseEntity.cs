using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {

    public EntityModel model;

    protected float currentLife;
    protected float currentShield;
    
    public abstract void Hit(Projectile projectile);
}
