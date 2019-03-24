using UnityEngine;

public class Enemy : BaseEntity {

    [HideInInspector] public EnemyGenerator generator;
    [HideInInspector] public int spaceIndex;

    public override void Hit(Projectile projectile) {
        base.Hit (projectile);

        if (currentLife <= 0) {
            Destroy (transform.parent.gameObject);
            generator.onEnemyKilled.OnNext (spaceIndex);
        }
    }
}
