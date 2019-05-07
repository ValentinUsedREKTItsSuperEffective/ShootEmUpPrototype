using UnityEngine;

public class Planet : BaseEntity {

    public float threshold;

    public override void Hit(int damage) {
        base.Hit (damage);

        if(model.currentLife.Value < model.life * threshold){
            model.currentLife.Value = 0;
        }
    }
}
