using UnityEngine;

public abstract class Enemy : BaseEntity {

    [HideInInspector] public WaveController generator;
    [HideInInspector] public int spaceIndex;

    public virtual void InitializeEnemy(WaveController generator, int index, Transform parent){
        this.generator = generator;
        spaceIndex = index;
        transform.parent = parent;
    }

    public override void Hit(Projectile projectile) {
        base.Hit (projectile);

        if (currentLife <= 0) {
            Destroy (gameObject);
            generator.onEnemyKilled.OnNext (spaceIndex);
        }
    }

    public abstract void Generate();
}
