using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {

    public EntityModel model;

    protected float currentLife;

    void Awake() {
        if (model == null) { 
            Debug.Log (name + "'s model is not defined !!");
            return;
        }

        currentLife = model.life;
    }

    public virtual void Hit(int damage) {
        currentLife -= damage;
    }
}
