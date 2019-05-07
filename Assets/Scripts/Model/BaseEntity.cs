using UnityEngine;
using UniRx;

public abstract class BaseEntity : MonoBehaviour {

    public EntityModel model;

    protected virtual void Awake() {
        if (model == null) {
            Debug.Log (name + "'s model is not defined !!");
            return;
        }

        model.currentLife = new ReactiveProperty<float> ();
        model.currentLife.Value = model.life;
    }

    public virtual void Hit(int damage) {
        if (model == null) {
            Debug.Log (name + "'s model is not defined !!");
            return;
        }

        model.currentLife.Value -= damage;
    }
}
