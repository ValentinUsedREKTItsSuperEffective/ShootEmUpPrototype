using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {

    public EntityModel model;

    protected float currentLife;
    protected float currentShield;
    
    public virtual void Hit(Projectile projectile) {
        if (currentShield > 0) {
            currentShield -= projectile.model.damage;

            if (currentShield < 0) {
                currentLife += currentShield;
                currentShield = 0;
            }
        } else {
            currentLife -= projectile.model.damage;
        }
    }
}
