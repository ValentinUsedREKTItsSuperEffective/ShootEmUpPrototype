using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {

    protected float currentShield;
    
    public abstract void Hit(Projectile projectile);
}
