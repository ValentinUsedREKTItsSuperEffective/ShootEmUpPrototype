using UnityEngine;

public abstract class Enemy : BaseEntity {

    [HideInInspector] public EnemyGenerator generator;
    [HideInInspector] public int spaceIndex;

    public override void Hit(Projectile projectile) {
        base.Hit (projectile);

        if (currentLife <= 0) {
            Destroy (gameObject);
            generator.onEnemyKilled.OnNext (spaceIndex);
        }
    }

    public abstract void Generate();
}
