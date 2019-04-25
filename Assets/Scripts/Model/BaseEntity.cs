using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {

    public EntityModel model;

    protected float currentLife;
    protected float currentShield;

    void Awake() {
        currentLife = model.life;
    }

    public virtual void Hit(int damage) {
        if (currentShield > 0) {
            currentShield -= damage;

            if (currentShield < 0) {
                currentLife += currentShield;
                currentShield = 0;
            }
        } else {
            currentLife -= damage;
        }
    }
}
